using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Souvenir.DataLayer;
using Souvenir.ViewModels;
using Souvenir.ViewModels.MainPageLists;


namespace Souvenir.Web.Controllers
{
    public class MainPageController : Controller
    {
 
        private readonly UnitOfWork db;
        public MainPageController()
        {
            db = new UnitOfWork();
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult MapInfo()
        {
            var info = db.Province.GetSouvenirsInfoForMap();
            var model = new List<MapInfoViewModel>();
            foreach (var item in info)
            {
                var modelItem = new MapInfoViewModel
                {
                    CategoryCount = item.CategoryCount,
                    ProvinceNo = item.ProvinceNo,
                    SouvenirCount = item.SouvenirCount
                };
                model.Add(modelItem);
            }
            

            return PartialView("_MapInfo" , model);
        }

        public PartialViewResult MostVisited()
        {
            List<Souvenirs> mostVisited = db.Souvenirs.MostVisited().ToList();
            var model = new List<MostVisitedViewModel>();
            foreach (var item in mostVisited)
            {
                var modelItem = new MostVisitedViewModel
                {
                    CategoryName =item.Category.CategoryName , 
                    ImageSrc = item.MainImage , 
                    SouvenirId = item.SouvenirId , 
                    SouvenirName = item.Name 
                };
                model.Add(modelItem);
            }

            return PartialView("_MostVisited" ,model);
        }

        public PartialViewResult NewestProducts()
        {
            List<Souvenirs> newProducts = db.Souvenirs.NewestSouvenirs().ToList();
            var model = new List<NewestProductsViewModel>();
            foreach (var item in newProducts)
            {
                var modelItem = new NewestProductsViewModel
                {
                    ProductID = item.SouvenirId ,
                    ProductImageSrc  = item.MainImage , 
                    ProductName  = item.Name , 
                    ProductShortDescription = item.ShortDescription
                };
                model.Add(modelItem);
            }

            return PartialView("_NewestProducts", model);
        }

        public PartialViewResult MainPageCategories()
        {
            List<SouvenirsCategory> categories = db.SouvenirsCategory.GetAllCategories().ToList();
            List<CategoryListsViewModel> model = new List<CategoryListsViewModel>();

            foreach (var item in categories)
            {
                var modelItem = new CategoryListsViewModel
                {
                    CategoryId = item.CategoryId , 
                    ImageSrc = item.CategoryImage , 
                    Name = item.CategoryName
                };
                model.Add(modelItem);
            }

            return PartialView("_MainPageCategories", model);    
        } 

        [Route("AboutUs")]
        public ActionResult About(bool IsAjax = false)
        {

            if (IsAjax)
            {
                return PartialView("AjaxAbout");
            }

            return View();
        }
        [Route("ContactUs")]
        public ActionResult ContactWays(bool IsAjax = false)
        {
            if (IsAjax)
            {
                return PartialView("AjaxContactWays");
            }
            
            return View();
        }

    }
}