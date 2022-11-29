using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Souvenir.DataLayer
{
    public class SouvenirsCategoryRepository:ISouvenirsCategoryRepository
    {
        private readonly ApplicationDbContext db;
        public SouvenirsCategoryRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public IEnumerable<SouvenirsCategory> GetAllCategories()
        {
            return db.SouvenirsCategories.Where(c => c.IsDeleted == false);
        }
        public async Task<IEnumerable<SouvenirsCategory>> GetAllCategoriesAsync()
        {
            return await Task.Run(() =>
            {
                return db.SouvenirsCategories.Where(c => c.IsDeleted == false);
            });
        }

        public SouvenirsCategory GetCategoryById(int id)
        {
            return db.SouvenirsCategories.Find(id);
        }

        public async Task<SouvenirsCategory> GetCategoryByIdAsync(object id)
        {
            return await Task.Run(() =>
            {
                return db.SouvenirsCategories.Find(id);
            });
        }

        public void InsertCategory(SouvenirsCategory category)
        {
            db.SouvenirsCategories.Add(category);
        }

        public async Task<Result> InsertCategoryAsync(SouvenirsCategory category)
        {
            return await Task.Run(() =>
            {
                try
                {
                    db.SouvenirsCategories.Add(category);
                    db.SaveChanges();
                    return Result.Success;
                }
                catch
                {

                    return Result.Failiure;
                }
            });
        }

        public void UpdateCategory(SouvenirsCategory category)
        {
            db.Entry(category).State = EntityState.Modified;
        }

        public async Task<Result> UpdateCategotyAsync(SouvenirsCategory category)
        {
            return await Task.Run(() =>
            {
                try
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return Result.Success;
                }
                catch
                {

                    return Result.Failiure;
                }
            });
        }

        public void DeleteCategory(SouvenirsCategory category)
        {
            db.Entry(category).State = EntityState.Deleted;

        }

        public void DeleteCategory(int id)
        {
            SouvenirsCategory category = GetCategoryById(id); 
            DeleteCategory(category);
        }

        public long Count()
        {
            return db.SouvenirsCategories.Count();
        }

     
    }
}
