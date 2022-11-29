using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Souvenir.DataLayer;
namespace Souvenir.DataLayer
{
    public interface ISouvenirsCategoryRepository
    {
        IEnumerable<SouvenirsCategory> GetAllCategories();
        Task<IEnumerable<SouvenirsCategory>> GetAllCategoriesAsync();
        Task<SouvenirsCategory> GetCategoryByIdAsync(object id);
        SouvenirsCategory GetCategoryById(int id); 
        void InsertCategory (SouvenirsCategory category);
        Task<Result> InsertCategoryAsync(SouvenirsCategory category);
        void UpdateCategory (SouvenirsCategory category);
        Task<Result> UpdateCategotyAsync(SouvenirsCategory category);
        void DeleteCategory (SouvenirsCategory category);
        void DeleteCategory(int id);
        long Count();

    }

}
