using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Souvenir.DataLayer
{
    public class SouvenirsRepository : ISouvenirsRepository
    {
        private readonly ApplicationDbContext db;
        public SouvenirsRepository(ApplicationDbContext context)
        {
            db = context;
        }



        public IEnumerable<Souvenirs> GetSouvenirs()
        {
            return db.Souvenirs.Include(s => s.Category).Include(s => s.Province).Where(s => s.IsDeleted == false);
        }

        public async Task<IEnumerable<Souvenirs>> GetAllSouevnirsAsync()
        {
            return await Task.Run(() =>
            {
                return db.Souvenirs.Include(s => s.Province).Include(s => s.Category).Where(s => s.IsDeleted == false && s.Category.IsDeleted == false);
            });
        }

        public Souvenirs GetSouvenirById(int id)
        {
            return db.Souvenirs.Find(id);
        }

        public async Task<Souvenirs> GetSouvenirByIdAsync(object id)
        {
            return await Task.Run(() =>
            {
                return db.Souvenirs.Find(id);
            });
        }

        public bool InsertSouvenir(Souvenirs souvenir)
        {
            try
            {
                db.Souvenirs.Add(souvenir);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<Result> InsertSouvenirAsync(Souvenirs souvenir)
        {
            return await Task.Run(() =>
            {
                try
                {
                    db.Souvenirs.Add(souvenir);
                    db.SaveChanges();
                    return Result.Success;
                }
                catch
                {

                    return Result.Failiure;
                }
            });
        }

        public bool UpdateSouvenir(Souvenirs souvenir)
        {
            try
            {
                db.Entry(souvenir).State = EntityState.Modified;
                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<Result> UpdateSouvenirAsync(Souvenirs souvenir)
        {
           return await Task.Run(() =>
            {
                try
                {
                    db.Entry(souvenir).State = EntityState.Modified;
                    db.SaveChanges();
                    return Result.Success;
                }
                catch
                {
                    return Result.Failiure;
                }
            });

        }

        public bool DeleteSouvenir(Souvenirs souvenir)
        {
            try
            {
                db.Entry(souvenir).State = EntityState.Deleted;
                return true;
            }
            catch
            {

                return false;
            }
        }

        public bool DeleteSouvenir(int id)
        {
            try
            {

                Souvenirs souvenir = GetSouvenirById(id);
                DeleteSouvenir(souvenir);
                return true;
            }
            catch
            {

                return false;
            }
        }


        public long Count()
        {
            return db.Souvenirs.Count(s=> s.IsDeleted == false && s.Category.IsDeleted == false);
        }

        public IEnumerable<Souvenirs> MostVisited(int take = 4)
        {
            return db.Souvenirs.Where(s => s.IsDeleted == false && s.Category.IsDeleted == false).OrderByDescending(s => s.Visit).Take(take);
        }

        public IEnumerable<Souvenirs> NewestSouvenirs(int take = 10)
        {
            return db.Souvenirs.Where(s => s.IsDeleted == false && s.Category.IsDeleted == false).OrderByDescending(s => s.CreateDate).Take(take);
        }


        public IEnumerable<Souvenirs> GetSouvenirsByCategoryId(int id)
        {
            return db.Souvenirs.Where(s => s.CategoryId == id && s.IsDeleted == false);
        }

        public IEnumerable<Souvenirs> GetSouvenirsByProvince(int id)
        {
            return db.Souvenirs.Where(s => s.Province.ProvinceNo == id && s.IsDeleted == false);

        }

        public async Task<IEnumerable<Souvenirs>> FindAsync(string Key)
        {
            return await Task.Run(() => {

                return db.Souvenirs.Where(s => s.Name.Contains(Key) || s.Province.ProvinceName.Contains(Key) || s.Category.CategoryName.Contains(Key) || s.Description.Contains(Key) || s.ShortDescription.Contains(Key));
            });
        }

        public async Task<IEnumerable<Comments>> GetCommnetsAsync(int postId)
        {
            return await Task.Run(async () => {

                var souvenir = await GetSouvenirByIdAsync(postId);

                return souvenir.Comments;
            });
        }
    }
}
