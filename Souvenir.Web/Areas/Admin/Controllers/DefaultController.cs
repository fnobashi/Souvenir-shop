using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Souvenir.DataLayer;
using Souvenir.ViewModels.Admin;

namespace Souvenir.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DefaultController : Controller
    {
        private readonly UnitOfWork db;
        public DefaultController()
        {
            db = new UnitOfWork();
        }


        public ActionResult Index()
        {
            var endDate = DateTime.Now;
            var startDate = DateTime.Now.AddDays(-30);

            var model = new IndexViewModel {
                UsersCount = db.Users.UsersCount(),
                ProductsCount = db.Souvenirs.Count(),
                ThisMonthSale = db.Cart.GetTotalPaymentByDate(startDate, endDate),
                InProgressOrders = db.Cart.CountOrdersByStatus((DataLayer.Dto.Status)Status.InProgress) , 
                NewOrders = db.Cart.CountOrdersByStatus((DataLayer.Dto.Status)Status.Registered)
            };
            return View(model);
        }
    }
}