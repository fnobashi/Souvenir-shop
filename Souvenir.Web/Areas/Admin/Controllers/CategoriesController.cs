using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Souvenir.DataLayer;
using Souvenir.ViewModels.Admin.Categories;
using Application.Utilites;

namespace Souvenir.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly UnitOfWork db;
        public CategoriesController()
        {
            db = new UnitOfWork();
        }

        public async Task<ActionResult> Index(bool IsAjax = false)
        {
            var categories = await db.SouvenirsCategory.GetAllCategoriesAsync();
            var model = new List<IndexViewModel>();

            foreach (var item in categories)
            {
                var modelItem = new IndexViewModel
                {
                    Id = item.CategoryId,
                    Name = item.CategoryName,
                    ImageSrc = item.CategoryImage,
                };

                model.Add(modelItem);
            }

            if (IsAjax == false)
            {
             
                return View(model);
            }
            else
            {
                return View("AjaxIndex", model);
            }
            
        }


        public ActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCategory(InsertViewModel model, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                var path = Server.MapPath(@"\CategoryImages");
                if (file == null)
                {
                    ModelState.AddModelError("file", "لطفا عکس دسته بندی را انتخاب کنید"); ;
                    return View(model);
                }

                try
                {
                    var filename = await Application.Utilites.File.Save(file, path, true, ".png", ".jpg", ".jpeg");
                    var category = new SouvenirsCategory
                    {
                        CategoryName = model.Name,
                        CategoryImage = filename,
                        IsDeleted = false
                    };

                    var res = await db.SouvenirsCategory.InsertCategoryAsync(category);

                    switch (res)
                    {
                        case Result.Success:
                            return RedirectToAction("index");
                        case Result.Failiure:
                            ViewBag.Error = "متاسفانه مشکلی در ذخیره داده پیش آمده لطفا مجددا تلاش فرمایید.";
                            return View("Error");
                    }
                }
                catch (ValidFileExtensionException)
                {
                    ModelState.AddModelError("", "فرمت فایل  انتخاب شده صحیح نمیباشد");
                    return View(model);
                   
                }

            }
            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> UpdateCategory(int id)
        {
            var category = await db.SouvenirsCategory.GetCategoryByIdAsync(id);

            var model = new UpdateViewModel
            {
                CategoryName = category.CategoryName,
                Id = category.CategoryId,
                ImageSrc = category.CategoryImage
            };

            return View(model);
        }

        public async Task<ActionResult> UpdateCategory(UpdateViewModel model, HttpPostedFileBase image)
        {

            if (ModelState.IsValid)
            {
                var path = Server.MapPath(@"\CategoryImages");

                try
                {

                    var category = new SouvenirsCategory
                    {
                        CategoryId = model.Id,
                        CategoryName = model.CategoryName,
                        IsDeleted = false,
                        CategoryImage = model.ImageSrc

                    };

                    if (image != null)
                    {

                        Application.Utilites.File.DeleteFile(Server.MapPath(@"\CategoryImages\"), category.CategoryImage);
                        var filename = await Application.Utilites.File.Save(image, path, true, ".png", ".jpg", ".jpeg");
                        category.CategoryImage = filename;

                    }


                    var res = await db.SouvenirsCategory.UpdateCategotyAsync(category);

                    switch (res)
                    {
                        case Result.Success:
                            return RedirectToAction("index");
                        case Result.Failiure:
                            ViewBag.Error = "متاسفانه مشکلی در ویرایش داده پیش آمده لطفا مجددا تلاش فرمایید.";
                            return View("Error");
                    }
                }
                catch (ValidFileExtensionException)
                {
                    ModelState.AddModelError("", "فرمت فایل انتخاب شده صحیح نمیباشد");
                    return View(model);
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteCategory(int? id)
        {

            if (id != null)
            {
                var category = await db.SouvenirsCategory.GetCategoryByIdAsync(id.Value);

                var model = new DeleteViewModel
                {
                    ID = category.CategoryId,
                    CategoryName = category.CategoryName,
                    ImageSrc = category.CategoryImage
                };

                return View(model);

            }
            ViewBag.Error = "آیتم مورد نظر یافت نشد";
            return View("Error");
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(DeleteViewModel model , bool IsAjax)
        {

            var category = await db.SouvenirsCategory.GetCategoryByIdAsync(model.ID);
            category.IsDeleted = true;

            var delResult = await db.SouvenirsCategory.UpdateCategotyAsync(category);
            if (delResult == Result.Success)
            {
                return RedirectToAction("Index" , new {IsAjax = IsAjax});

            }

            ViewBag.Error = "عملیات حذف با شکست مواحه شد";
            return View("Error");
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int? id)
        {
            if (id != null)
            {
                var category = await db.SouvenirsCategory.GetCategoryByIdAsync(id.Value);

                var model = new DetailViewModel
                {
                    Name = category.CategoryName,
                    Image = category.CategoryImage
                };

                return View(model);
            }
            ViewBag.Error = "دسته بندی مورد نظر یافت نشد";
            return View("Error");

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