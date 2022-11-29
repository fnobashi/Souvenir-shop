using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.DataLayer
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork()
        {
            _db = new ApplicationDbContext();
        }

        private ICartRepository _cartRepository;
        private ICommentsRepository _commentsRepository;
        private IProvinceRepository _provinceRepository;
        private ISouvenirsCategoryRepository _souvenirsCategoryRepository;
        private ISouvenirsRepository _souvenirsRepository;
        private ISouvenirImageRepository _souvenirImageRepository;
        private IUserRepository _usersRepository;

     

        public CartRepository Cart
        {
            get
            {
                if (_cartRepository == null)
                {
                    _cartRepository = new CartRepository(_db);
                }
                return (CartRepository)_cartRepository;
            }

        }

        public ICommentsRepository Comments
        {
            get
            {
                if (_commentsRepository == null)
                {
                    _commentsRepository = new CommentsRepository(_db);
                }
                return _commentsRepository;
            }
        }

        public IProvinceRepository Province
        {
            get
            {
                if (_provinceRepository == null)
                {
                    _provinceRepository = new ProvinceRepository(_db);
                }
                return _provinceRepository;
            }
        }


        public ISouvenirsCategoryRepository SouvenirsCategory
        {
            get
            {
                if (_souvenirsCategoryRepository == null)
                {
                    _souvenirsCategoryRepository = new SouvenirsCategoryRepository(_db);
                }
                return _souvenirsCategoryRepository;
            }
        }

        public ISouvenirsRepository Souvenirs
        {
            get
            {
                if (_souvenirsRepository == null)
                {
                    _souvenirsRepository = new SouvenirsRepository(_db);
                }
                return _souvenirsRepository;
            }
        }



        public ISouvenirImageRepository SouvenirOtherImages
        {
            get
            {
                if (_souvenirImageRepository == null)
                {
                    _souvenirImageRepository = new SouvenirImageRepository(_db);
                }
                return _souvenirImageRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_usersRepository == null)
                {
                    _usersRepository = new UserRepository(_db);
                }
                return _usersRepository;
            }

        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
