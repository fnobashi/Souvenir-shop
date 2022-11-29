using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Souvenir.DataLayer;
using Souvenir.ViewModels.Admin.Orders;
using System.Threading.Tasks;

namespace Souvenir.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {

        private readonly UnitOfWork db;
        public OrdersController()
        {
            db = new UnitOfWork();
        }

        [HttpGet]
        public async Task<ActionResult> Index(bool IsAjax = false)
        {
            var carts = await db.Cart.GetListOfPaidOrdersAsync();

            var model = new List<IndexViewModel>();

            foreach (var item in carts)
            {
                var modelItem = new IndexViewModel
                {
                    Id = item.Id,
                    UserAddress = item.CustomerAddress,
                    ItemsCount = item.ItemsCount,
                    UserFullName = item.FullName,
                    TotalPayment = item.TotalPayment, 
                    OrderDate = item.OrderDate , 
                    Status = (Status)item.Status
                };

                model.Add(modelItem);
            }
            if (IsAjax)
            {
                return PartialView("AjaxIndex", model);
            }

            return View(model);
        }


      
        public async Task<ActionResult> Detail(int id , Status status )
        {
            var cartItems = await db.Cart.GetCartItemsAsync(id);
            var model = new List<DetailsViewModel>();
            ViewBag.OrderId = id;
            ViewBag.Status = status;
            foreach (var item in cartItems)
            {
                var modelItem = new DetailsViewModel
                {
                    ItemId = item.ItemID,
                    OrderId = item.OrderID,
                    ProductName = item.Souvenir.Name,
                    ProductAmount = item.Count,
                    ProductPrice = item.Price
                };
                model.Add(modelItem);
            }

            return View(model);
        }

        public async Task<ActionResult> ConfirmOrder(long OrderId ,  bool IsAjax)
        {
            var order = await db.Cart.GetCartByIdAsync(OrderId);

            order.Status = (int)Status.InProgress;
            db.Cart.UpdateCart(order);
            db.Save();

            return RedirectToAction("index" , new { IsAjax = IsAjax});
        }


        public async Task<ActionResult> SendConfirm(long OrderId , bool IsAjax)
        {
            var order = await db.Cart.GetCartByIdAsync(OrderId);

            order.Status = (int)Status.Sent;
            db.Cart.UpdateCart(order);
            db.Save();

            return RedirectToAction("index", new { IsAjax = IsAjax });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}