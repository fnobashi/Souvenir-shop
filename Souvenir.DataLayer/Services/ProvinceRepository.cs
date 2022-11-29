using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Souvenir.DataLayer.Dto;


namespace Souvenir.DataLayer
{
    public class ProvinceRepository : IProvinceRepository
    {

        private ApplicationDbContext db;

        public ProvinceRepository(ApplicationDbContext context)
        {
            db = context;
        }
        
        public IEnumerable<Province> GetProvinces()
        {
            return db.Provinces;
        }
        public async Task<IEnumerable<Province>> GetProvincesAsync()
        {
            return await Task.Run(() => {
                return db.Provinces;
            });
        }

        public Province GetProvinceById(int id)
        {
            return db.Provinces.First(p => p.ProvinceNo == id);
        }

        public void InsertProvince(Province province)
        {
            db.Provinces.Add(province);
        }

        public void UpdateProvince(Province province)
        {
            db.Entry(province).State = EntityState.Modified;
        }

        public void DeleteProvince(Province province)
        {
            db.Entry(province).State = EntityState.Deleted;

        }

        public void DeleteProvince(int id)
        {
            Province province = GetProvinceById(id);
            DeleteProvince(province);
        }

        public List<MapInfoDto> GetSouvenirsInfoForMap()
        {
            var Provinces = db.Provinces.ToList();
            List<MapInfoDto> MapInfoList = new List<MapInfoDto>();
            foreach (var item in Provinces)
            {

                MapInfoDto mapInfo = new MapInfoDto()
                {
                    ProvinceNo = item.ProvinceNo,
                    SouvenirCount = item.Souvenir.Where(s => s.IsDeleted == false).Count(),
                    CategoryCount = db.Souvenirs.Where(s => s.Province.ProvinceNo == item.ProvinceNo && s.Category.IsDeleted == false).Select(s => s.Category).Distinct().Count()
                };

                MapInfoList.Add(mapInfo);

            }
            return MapInfoList;
        }

       
    }
}
