using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Souvenir.DataLayer;
using Souvenir.ViewModels.Souvenirs;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Souvenir.Web.Controllers
{
    public class SouvenirController : Controller
    {

        private readonly UnitOfWork db;
        public SouvenirController()
        {
            db = new UnitOfWork();
        }



        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [Route("Souvenirs/{id}")]
        public ActionResult Index(int? id, CartResult? result)
        {
            Souvenirs souvenir = db.Souvenirs.GetSouvenirById(id.Value);
            if (souvenir != null)
            {

                var userId = User.Identity.GetUserId();
                var souvenirIamges = new List<SouvenirOtherImagesDto>();

                foreach (var image in souvenir.OtherImages)
                {
                    souvenirIamges.Add(new SouvenirOtherImagesDto
                    {
                        ImageName = image.ImageName
                    });
                }

                var model = new ShowSouvenirViewModel
                {
                    SouvenirId = souvenir.SouvenirId,
                    Name = souvenir.Name,
                    ShortDescription = souvenir.ShortDescription,
                    Description = souvenir.Description,
                    MainImage = souvenir.MainImage,
                    Price = souvenir.Price,
                    OtherImages = souvenirIamges , 
                    Inventory = souvenir.Inventory , 
                };


                
                ViewBag.Comments = souvenir.Comments;


                if (User.Identity.IsAuthenticated)
                {
                    model.UserId = User.Identity.GetUserId();


                    if (db.Cart.IsUserHaveCart(userId))
                    {
                        model.OrderID = db.Cart.GetCartIdByUserId(userId);

                    }
                    else
                    {
                        Cart cart = new Cart
                        {
                            IsFinally = false,
                            DateTime = DateTime.Now,
                            UserId = userId

                        };
                        db.Cart.CreateCart(cart);
                        db.Save();
                        model.OrderID = db.Cart.GetCartIdByUserId(userId);

                    }


                }

                souvenir.Visit++;
                db.Souvenirs.UpdateSouvenirAsync(souvenir);

                if (result != null)
                {
                    switch (result)
                    {
                        case CartResult.Success:
                            model.IsAddToCartResult = true;
                            ViewBag.Result = "محصول با موفقیت به سبد خرید اضافه شد";
                            return View(model);
                        case CartResult.Duplicate:
                            model.IsAddToCartResult = true;
                            ViewBag.Result = "محصول در سبد خرید موجود می باشد";
                            return View(model);
                        case CartResult.NotEnough:
                            model.IsAddToCartResult = true;
                            ViewBag.Result = "محصول موجود نمیباشد";
                            return View(model);
                        case CartResult.Faild:
                        default:
                            model.IsAddToCartResult = true;
                            ViewBag.Result = "عملیات افزودن با شکست مواجه شد لطفا دقایقی دیگر دوباره امتحان بفرمایید";
                            return View(model);
                    }
                }


                return View(model);
            }

            ViewBag.Title = "ارور 404";
            ViewBag.Info = "صفحه مورد نظر یافت نشد";
            return View("NofFoundError");
        }

        public async Task<ActionResult> LoadComments(int souvenirId)
        {
            var comments = await db.Souvenirs.GetCommnetsAsync(souvenirId);


            return PartialView(comments);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComment(int SouvenirId, string commentBody, int? parentId)
        {

            var comment = new Comments
            {
                Comment = commentBody,
                SouvenirID = SouvenirId,
                Date = DateTime.Now , 
                UserId = User.Identity.GetUserId()
            };

            var result =  await db.Comments.AddCommentAsync(comment , parentId);

            switch (result)
            {
                case Result.Success:
                    return RedirectToAction("LoadComments", new { souvenirId = SouvenirId});
                case Result.Failiure:
                    ViewBag.Error = "خطا در افزودن نظر";
                    throw new Exception("نشد ک بشه");
                    
            }

            return RedirectToAction("Index", new { id = SouvenirId });

        }


    }
}