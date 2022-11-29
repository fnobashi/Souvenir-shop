using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Souvenir.DataLayer.Dto;

namespace Souvenir.DataLayer
{
    public interface IProvinceRepository
    {
        IEnumerable<Province> GetProvinces();
        Task<IEnumerable<Province>> GetProvincesAsync();
        Province GetProvinceById(int id);   
        void InsertProvince(Province province);
        void UpdateProvince(Province province); 
        void DeleteProvince(Province province);
        void DeleteProvince(int id);
        List<MapInfoDto> GetSouvenirsInfoForMap();
    }
}
