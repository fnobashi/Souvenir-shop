using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Souvenir.DataLayer;
using Souvenir.ViewModels.Categories;
using Souvenir.ViewModels.Souvenirs;   


namespace Souvenir.Web.Controllers
{
    public class ListsController : Controller
    {
        private readonly UnitOfWork db;
        public ListsController()
        {
            db = new UnitOfWork();
        }

        [Route("Categories")]
        public ActionResult CategoryLists()
        {
            List<SouvenirsCategory> categories = db.SouvenirsCategory.GetAllCategories().ToList();
            List<CategoryListsVIewModel> model = new List<CategoryListsVIewModel>();
            foreach (var item in categories)
            {
                var listItem = new CategoryListsVIewModel
                {
                    Id = item.CategoryId,
                    ImageSrc = item.CategoryImage,
                    Name = item.CategoryName
                };
                model.Add(listItem);
            }

            return View(model);
        }


        [Route("Categories/{id}")]
        public ActionResult SouvenirListsByCategory(int? id)
        {

            List<Souvenirs> souvenirs = db.Souvenirs.GetSouvenirsByCategoryId(id.Value).ToList();
            
            if (souvenirs.Count > 0)
            {
                List<SouvenirListsByCategoryViewModel> model = new List<SouvenirListsByCategoryViewModel>();
                foreach (var item in souvenirs)
                {
                    var listItem = new SouvenirListsByCategoryViewModel
                    {
                        Id = item.SouvenirId,
                        ImageSrc = item.MainImage,
                        Name = item.Name,
                        ShortDescription = item.ShortDescription
                    };
                    model.Add(listItem);
                }
                
                return View(model);
            }

            ViewBag.Title = "دسته بندی خالی";
            ViewBag.Info = "در حال حاظر دسته بندی مورد نظر خالی میباشد به زودی آیتم های جدید به آن اضافه خواهد شد.";
            return View("NofFoundError");
        }

        [Route("Provinces/{id}")]
        public ActionResult SouvenirListsByProvince(int? id)
        {    
            Province province;
            try
            {
                province = db.Province.GetProvinceById(id.Value);
                List<Souvenirs> souvenirs = db.Souvenirs.GetSouvenirsByProvince(id.Value).ToList();
                List<SouvenirsListByProvinceViewModel> model = new List<SouvenirsListByProvinceViewModel>();
                foreach (var item in souvenirs)
                {
                    var listItem = new SouvenirsListByProvinceViewModel
                    {
                        Id = item.SouvenirId,
                        ImageSrc = item.MainImage,
                        Name = item.Name
                    };
                    model.Add(listItem);
                }
                ViewBag.ProvinceName = province.ProvinceName;
                return View(model);
            }
            catch
            {
                ViewBag.Title = "ارور 404";
                ViewBag.Info = "صفحه مورد نظر یافت نشد";
                return View("NofFoundError");
            }
        }
    }
}