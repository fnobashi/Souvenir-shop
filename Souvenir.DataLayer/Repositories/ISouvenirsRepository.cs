using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.DataLayer
{
    public interface ISouvenirsRepository
    {
        IEnumerable<Souvenirs> GetSouvenirs();
        Task<IEnumerable<Souvenirs>> GetAllSouevnirsAsync();
        Souvenirs GetSouvenirById(int id);
        Task<Souvenirs> GetSouvenirByIdAsync(object id);
        bool InsertSouvenir(Souvenirs souvenir);
        Task<Result> InsertSouvenirAsync(Souvenirs souvenir);
        bool UpdateSouvenir(Souvenirs souvenir);
        Task<Result> UpdateSouvenirAsync(Souvenirs souvenir);
        bool DeleteSouvenir(Souvenirs souvenir);
        bool DeleteSouvenir(int id);
        long Count();
        IEnumerable<Souvenirs> MostVisited(int take = 4);
        IEnumerable<Souvenirs> NewestSouvenirs(int take = 10);
        IEnumerable<Souvenirs> GetSouvenirsByCategoryId(int id);
        IEnumerable<Souvenirs> GetSouvenirsByProvince(int id);
        Task<IEnumerable<Souvenirs>> FindAsync(string Key);
        Task<IEnumerable<Comments>> GetCommnetsAsync(int postId);

    }
}
