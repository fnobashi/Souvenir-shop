using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Souvenir.DataLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            

        }

        public virtual DbSet<Souvenirs> Souvenirs { get; set; }
        public virtual DbSet<SouvenirsCategory> SouvenirsCategories { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<SouvenirImages> SouvenirImages { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }

   



        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
            CreateProvinces();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        static void CreateProvinces()
        {
            Dictionary<int, string> Provinces = new Dictionary<int, string>
            {
                { 1 ,"آذربایجان غربی" },
                { 2 ,"آذربایجان شرقی" },
                { 3,"اردبیل" },
                { 4,"اصفهان" },
                { 5,"البرز" },
                { 6,"ایلام" },
                { 7,"بوشهر" },
                { 8,"تهران"},
                { 9,"چهارمحال و بختیاری"},
                { 10,"خراسان جنوبی"},
                { 11,"خراسان رضوی"},
                { 12,"خراسان شمالی"},
                { 13,"خوزستان"},
                { 14,"زنجان"},
                { 15,"سمنان"},
                { 16,"سیستان و بلوچستان"},
                { 17,"فارس"},
                { 18,"قزوین"},
                { 19,"قم"},
                { 20,"کردستان"},
                { 21,"کرمان"},
                { 22,"کرمانشاه"},
                { 23,"کهگیلویه و بویراحمد"},
                { 24,"گلستان"},
                { 25,"گیلان"},
                { 26,"لرستان"},
                { 27,"مازندران"},
                { 28,"مرکزی"},
                { 29,"هرمزگان"},
                { 30,"همدان"},
                { 31, "یزد" },
            };

            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                if (db.Provinces.ToList().Count == 0)
                {
                    foreach (KeyValuePair<int, string> province in Provinces)
                    {
                        var ProvinceName = new Province()
                        {

                            ProvinceName = province.Value,
                            ProvinceNo = province.Key
                        };

                        db.Provinces.Add(ProvinceName);
                    }
                    db.SaveChanges();
                }

            }
        }


    }
}