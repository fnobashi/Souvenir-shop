using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Souvenir.DataLayer;
using System.Threading.Tasks;
using Souvenir.ViewModels.Admin.Souvenirs;
using Application.Utilites;

namespace Souvenir.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SouvenirsController : Controller
    {

        private readonly UnitOfWork db;
        public SouvenirsController()
        {
            db = new UnitOfWork();
        }

        public async Task<ActionResult> Index(bool IsAjax = false)
        {
            var souvenirs = await db.Souvenirs.GetAllSouevnirsAsync();

            var model = new List<IndexViewModel>();
            foreach (var item in souvenirs)
            {
                var modelItem = new IndexViewModel
                {
                    Id = item.SouvenirId,
                    Name = item.Name,
                    Image = item.MainImage,
                    Category = item.Category.CategoryName,
                    Province = item.Province.ProvinceName,
                    Price = item.Price.ToToman(),
                    Inventory = item.Inventory
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

        [HttpGet]
        public async Task<ActionResult> AddSouvenir()
        {

            ViewBag.CategoryId = new SelectList(await db.SouvenirsCategory.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
            ViewBag.ProvinceId = new SelectList(await db.Province.GetProvincesAsync(), "ProvinceId", "ProvinceName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSouvenir(InsertViewModel model, HttpPostedFileBase mainImage, List<HttpPostedFileBase> otherImages)
        {
            if (ModelState.IsValid)
            {
                var mainImagePath = Server.MapPath(@"\SouvenirMainImages");
                var otherImagesPath = Server.MapPath(@"\SouvenirOtherImages");

                try
                {

                    if (mainImage == null)
                    {
                        ModelState.AddModelError("mainImage", "لطفا عکس اصلی محصول را انتخاب نمایید");
                        ViewBag.CategoryId = new SelectList(await db.SouvenirsCategory.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
                        ViewBag.ProvinceId = new SelectList(await db.Province.GetProvincesAsync(), "ProvinceId", "ProvinceName");
                        return View(model);
                    }

                    var mainImageFileName = await Application.Utilites.File.Save(mainImage, mainImagePath, true, ".png", ".jpg", ".jpeg");

                    var souvenir = new Souvenirs
                    {
                        Name = model.Name,
                        CategoryId = model.CategoryId,
                        ProvinceId = model.ProvinceId,
                        ShortDescription = model.ShortDescription,
                        Description = model.Description,
                        Price = model.Price,
                        CreateDate = DateTime.Now,
                        IsDeleted = false,
                        MainImage = mainImageFileName,
                        Visit = 0,
                        Inventory = model.Inventory,
                        OtherImages = new List<SouvenirImages>()
                    };



                    if (otherImages[0] != null)
                    {
                        foreach (var image in otherImages)
                        {
                            var imageName = await Application.Utilites.File.Save(image, otherImagesPath, true, ".png", ".jpg", ".jpeg");
                            var imageSrc = new SouvenirImages
                            {
                                ImageName = imageName
                            };
                            souvenir.OtherImages.Add(imageSrc);
                        }
                    }

                    var res = await db.Souvenirs.InsertSouvenirAsync(souvenir);

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
                    ViewBag.CategoryId = new SelectList(await db.SouvenirsCategory.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
                    ViewBag.ProvinceId = new SelectList(await db.Province.GetProvincesAsync(), "ProvinceId", "ProvinceName");
                    ModelState.AddModelError("", "فرمت فایل های انتخاب شده صحیح نمیباشد");
                    return View(model);
                }

            }
            ViewBag.CategoryId = new SelectList(await db.SouvenirsCategory.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
            ViewBag.ProvinceId = new SelectList(await db.Province.GetProvincesAsync(), "ProvinceId", "ProvinceName");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateSouvenir(int? id)
        {
            if (id != null)
            {
             
                var souvenir = await db.Souvenirs.GetSouvenirByIdAsync(id);
                var model = new UpdateViewModel
                {
                    Id = souvenir.SouvenirId,
                    CategoryId = souvenir.CategoryId,
                    ProvinceId = souvenir.ProvinceId,
                    ShortDescription = souvenir.ShortDescription,
                    Description = souvenir.Description,
                    Name = souvenir.Name,
                    Inventory = souvenir.Inventory,
                    Price = souvenir.Price
                };
                ViewBag.CategoryId = new SelectList(await db.SouvenirsCategory.GetAllCategoriesAsync(), "CategoryId", "CategoryName" , souvenir.CategoryId);
                ViewBag.ProvinceId = new SelectList(await db.Province.GetProvincesAsync(), "ProvinceId", "ProvinceName" , souvenir.ProvinceId);

                return View(model);
            }
            ViewBag.Error = "محصول مورد نظر یافت نشد";
            return View("Error");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateSouvenir(UpdateViewModel model, HttpPostedFileBase mainImage, List<HttpPostedFileBase> otherImages)
        {
            if (ModelState.IsValid)
            {
                var mainImagePath = Server.MapPath(@"\SouvenirMainImages");
                var otherImagesPath = Server.MapPath(@"\SouvenirOtherImages");

                try
                {

                    var souvenir = db.Souvenirs.GetSouvenirById(model.Id);

                    souvenir.Name = model.Name;
                    souvenir.ProvinceId = model.ProvinceId;
                    souvenir.CategoryId = model.CategoryId;
                    souvenir.ShortDescription = model.ShortDescription;
                    souvenir.Description = model.Description;
                    souvenir.Price = model.Price;
                    souvenir.Inventory = model.Inventory;

                    if (mainImage != null)
                    {
                        var mainImageFileName = await Application.Utilites.File.Save(mainImage, mainImagePath, true, ".png", ".jpg", ".jpeg");
                        souvenir.MainImage = mainImageFileName;
                    }


                    if (otherImages[0] != null)
                    {
                        if (souvenir.OtherImages == null)
                        {
                            souvenir.OtherImages = new List<SouvenirImages>();
                        }
                        foreach (var image in otherImages)
                        {
                            var imageName = await Application.Utilites.File.Save(image, otherImagesPath, true, ".png", ".jpg", ".jpeg");
                            var imageSrc = new SouvenirImages
                            {
                                ImageName = imageName
                            };
                            souvenir.OtherImages.Add(imageSrc);
                        }
                    }

                    var res = await db.Souvenirs.UpdateSouvenirAsync(souvenir);

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
                    ViewBag.CategoryId = new SelectList(await db.SouvenirsCategory.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
                    ViewBag.ProvinceId = new SelectList(await db.Province.GetProvincesAsync(), "ProvinceId", "ProvinceName");
                    ModelState.AddModelError("", "فرمت فایل های انتخاب شده صحیح نمیباشد");
                    return View(model);
                }

            }
            ViewBag.CategoryId = new SelectList(await db.SouvenirsCategory.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
            ViewBag.ProvinceId = new SelectList(await db.Province.GetProvincesAsync(), "ProvinceId", "ProvinceName");
            return View(model);
        }



        [HttpGet]
        public async Task<ActionResult> DeleteSouvenir(int? id)
        {

            if (id != null)
            {
                var souvenir = await db.Souvenirs.GetSouvenirByIdAsync(id.Value);

                var model = new DeleteViewModel
                {
                    Id = souvenir.SouvenirId , 
                    Category = souvenir.Category.CategoryName  , 
                    Provice  = souvenir.Province.ProvinceName  , 
                    ImageSrc = souvenir.MainImage , 
                    Name = souvenir.Name    
                };

                return View(model);

            }
            ViewBag.Error = "آیتم مورد نظر یافت نشد";
            return View("Error");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(DeleteViewModel model, bool IsAjax)
        {

            var souvenir = await db.Souvenirs.GetSouvenirByIdAsync(model.Id);
            souvenir.IsDeleted = true;

            var delResult = await db.Souvenirs.UpdateSouvenirAsync(souvenir);
            if (delResult == Result.Success)
            { 
                return RedirectToAction("Index", new { IsAjax = IsAjax });

            }

            ViewBag.Error = "عملیات حذف با شکست مواحه شد";
            return View("Error");
        }


        [HttpGet]
        public async Task<ActionResult> Detail(int? id)
        {
            if (id != null)
            {
                var souvenir = await db.Souvenirs.GetSouvenirByIdAsync(id.Value);

                var model = new DetailViewModel
                {
                    Name =  souvenir.Name  , 
                    Category = souvenir.Category.CategoryName , 
                    Image = souvenir.MainImage , 
                    Inventory = souvenir.Inventory  , 
                    Price  = souvenir.Price.ToToman(), 
                    Province = souvenir.Province.ProvinceName , 
                    ShortDescription  = souvenir.ShortDescription
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