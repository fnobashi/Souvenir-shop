using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.DataLayer
{
    public class SouvenirImageRepository : ISouvenirImageRepository
    {
        private readonly ApplicationDbContext db;


        public SouvenirImageRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public List<SouvenirImages> GetAllImages()
        {
            return db.SouvenirImages.ToList();
        }

        public IEnumerable<SouvenirImages> GetImageBySouvenirID(int souvenirID)
        {
            return db.SouvenirImages.Where(img => img.SouvenirId == souvenirID);
        }

        public SouvenirImages GetImage(long ImgId)
        {
            return db.SouvenirImages.Find(ImgId);
        }

        public bool SaveImageFileName(SouvenirImages img)
        {
            try
            {
                db.SouvenirImages.Add(img);
                return true;
            }
            catch
            {
                return false;

            }
        }
        public bool DeleteImageFileName(SouvenirImages img)
        {
            try
            {
                db.Entry(img).State = System.Data.Entity.EntityState.Deleted;
                return true;
            }
            catch
            {

                return false;
            }
        }
        public bool UpdateImageFileName(SouvenirImages img)
        {
            try
            {
                db.Entry(img).State = System.Data.Entity.EntityState.Modified;
                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
