using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Souvenir.DataLayer;
using Souvenir.ViewModels.Cart;
using Souvenir.ViewModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Souvenir.Web.Controllers
{
	public class CartController : Controller
	{
		private readonly UnitOfWork db;
		public CartController()
		{
			db = new UnitOfWork();
		}

		[Route("User/Cart")]
		public ActionResult Index(CartResult? itemResult , int? souvenirId, bool IsAjax = false)
		{
			if (User.Identity.IsAuthenticated)
			{
				List<CartItemsViewModel> items = new List<CartItemsViewModel>();
				var userId = User.Identity.GetUserId();
				var orderId = db.Cart.GetCartIdByUserId(userId);
				dynamic cartItems;
				dynamic paymentResult;
				dynamic newOrderId = 0;

				if (orderId != 0)
				{

					cartItems = db.Cart.GetCartItemsOfUser(orderId);
					paymentResult = db.Cart.GetPaymentResult(orderId);
				}
                else
                {
					Cart cart = new Cart
					{
						IsFinally = false,
						DateTime = DateTime.Now,
						UserId = userId , 
						Items = new List<CartItem>()
					};
					
					db.Cart.CreateCart(cart);
					newOrderId = cart.OrderID;
					db.Save();
					cartItems = db.Cart.GetCartItemsOfUser(newOrderId);
					paymentResult = db.Cart.GetPaymentResult(newOrderId);
				}



				foreach (var item in cartItems)
				{
					var cartItem = new CartItemsViewModel
					{
						ItemID = item.ItemID,
						SouvneirId = item.SouvenirId,
						Title = item.Souvenir.Name,
						ImageName = item.Souvenir.MainImage,
						Price = item.Price,
						Count = item.Count
					};
					items.Add(cartItem);
				}

				var model = new CartIndexViewModel
				{
					Items = items,
					TotalCount = paymentResult.SouvenirCount,
					TotalPrice = paymentResult.Price,
					OrderId = orderId != 0 ? orderId: newOrderId
				};

                if (itemResult == CartResult.NotEnough)
                {
					ViewBag.ErrorMessage = "موجودی کافی نمی باشد";
					ViewBag.SouvenirId = souvenirId;
				}
			
                if (IsAjax)
                {
					return PartialView("AjaxIndex", model);
                }
              

				return View(model);
			}
			// If User isn,t Logged In Send It To Login Page... 
			return Redirect("/Login");
		}

		public ActionResult GetCartInformation(AddToCartViewModel model)
		{
			return PartialView("_addToCartPartial", model);
		}

		[Route("Souevnir/AddToCart")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddToCart(AddToCartViewModel model)
		{

			var souvenir = await db.Souvenirs.GetSouvenirByIdAsync(model.SouvenirId);

            if (souvenir.Inventory > 0 )
            {

				var cartItem = new CartItem
				{
					Count = 1,
					OrderID = model.OrderId,
					SouvenirId = model.SouvenirId,
					Price = model.Price
				};

				var result = db.Cart.AddToCart(cartItem);
				return RedirectToAction("Index", "Souvenir", new { id = model.SouvenirId, result = result });
			}

			return RedirectToAction("Index", "Souvneir", new { id = model.SouvenirId, result = CartResult.NotEnough });
			
		}

		[Route("User/Cart/AddItem")]
		public ActionResult AddOneToItemAmount(long orderId , long itemId , int souvenirId)
		{
			var result = db.Cart.ItemCountPlus(orderId , itemId , souvenirId);
            if (result == CartResult.NotEnough)
            {
				return RedirectToAction("Index", new { itemResult = result , IsAjax = true  , souvenirId = souvenirId});
            }
			return RedirectToAction("Index" , new {IsAjax = true});
		}


		[Route("User/Cart/DeleteItem")]
		public ActionResult MinusOneFormItemAmount(long orderId, long itemId , int souvenirId)
		{

			db.Cart.ItemCountMinus(orderId , itemId , souvenirId);
			return RedirectToAction("Index"  , new {IsAjax = true});

		}
	 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Payment(long OrderId)
		{

			var order = db.Cart.GetCartById(OrderId);

			var paymentReuslt = db.Cart.GetPaymentResult(OrderId);


			System.Net.ServicePointManager.Expect100Continue = false;
			ZarinPal.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();
			string Authority;

            //int Status = zp.PaymentRequest("YOUR-ZARINPAL-MERCHANT-CODE", (int)paymentReuslt.Price, "تست درگاه زرین پال در تاپ لرن", "fnobashi@gmail.com", "09355939546", "https://localhost:44369/Cart/VerifyPayment/?orderId=" + order.OrderID, out Authority);
            int Status = zp.PaymentRequest("YOUR-ZARINPAL-MERCHANT-CODE", (int)paymentReuslt.Price, "تست درگاه زرین پال در تاپ لرن", "fnobashi@gmail.com", "09355939546", "https://soghatitest.ir/Cart/VerifyPayment/?orderId=" + order.OrderID, out Authority);
		
            if (Status == 100)
			{
				// Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
				Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
			}
			else
			{
				ViewBag.Error = "Error : " + Status;
			}
			return View();

		}

		
		public ActionResult VerifyPayment (long orderId)
		{
			var order = db.Cart.GetCartById(orderId);
			var paymentReuslt = db.Cart.GetPaymentResult(orderId);

			if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
			{
				if (Request.QueryString["Status"].ToString().Equals("OK"))
				{
					long RefID;
					System.Net.ServicePointManager.Expect100Continue = false;
					ZarinPal.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();

					int Status = zp.PaymentVerification("YOUR-ZARINPAL-MERCHANT-CODE", Request.QueryString["Authority"].ToString(), (int)paymentReuslt.Price , out RefID);
					
					if (Status == 100)
					{
						order.IsFinally = true;
						order.TotalPrice = paymentReuslt.Price;
						db.Cart.UpdateCart(order);
						db.Save();
						ViewBag.IsSuccess = true;
						ViewBag.RefId = RefID;
					}
					else
					{
						ViewBag.Status = Status;
					}
				}
				else
				{
					Response.Write("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
				}
			}
			else
			{
				Response.Write("Invalid Input");
			}
			return View();
		}

	}
}