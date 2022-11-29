using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Souvenir.DataLayer;
using System.Threading.Tasks;
using Souvenir.ViewModels.Souvenirs;



namespace Souvenir.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly UnitOfWork db;

        public SearchController()
        {
            db = new UnitOfWork();  
        }

        [Route("Search")]
        public async Task<ActionResult> Index(string key)
        {

            ViewBag.Key = key;
            var query = await db.Souvenirs.FindAsync(key);

            var model = new List<SearchViewModel>();

            foreach (var item in query)
            {
                var modelItem = new SearchViewModel
                {
                    Id = item.SouvenirId,
                    ShortDescription = item.ShortDescription,
                    ImageSrc = item.MainImage,
                    Title = item.Name
                };
                model.Add(modelItem);
            }
            return View(model);
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