using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using Souvenir.DataLayer.Dto;

namespace Souvenir.DataLayer
{
    public class CartRepository : ICartRepository
    {

        private ApplicationDbContext db;

        public CartRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public Cart GetCartById(long id)
        {
            return db.Cart.Find(id);
        }

        public async Task<Cart> GetCartByIdAsync(long id)
        {
            return await Task.Run(() =>
            {
                return db.Cart.Find(id);
            });
        }


        public CartResult CreateCart(Cart cart)
        {
            try
            {
                db.Cart.Add(cart);
                db.SaveChanges();
                return CartResult.Success;
            }
            catch
            {
                return CartResult.Faild;
            }

        }

        public void UpdateCart(Cart cart)
        {
            db.Entry(cart).State = EntityState.Modified;
        }


        public CartResult CreateCart(Cart cart, CartItem item)
        {
            try
            {
                db.Cart.Add(cart);
                db.CartItems.Add(item);
                db.SaveChanges();
                return CartResult.Success;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return CartResult.Duplicate;
            }
            catch
            {
                return CartResult.Faild;
            }
        }


        public CartResult AddToCart(CartItem item)
        {
            var IsItemExist = db.CartItems.Any(it => it.SouvenirId == item.SouvenirId && it.OrderID == item.OrderID);
            if (!IsItemExist)
            {
                try
                {
                    db.CartItems.Add(item);
                    var souvenir = db.Souvenirs.Find(item.SouvenirId);
                    souvenir.Inventory = souvenir.Inventory - 1;
                    db.SaveChanges();
                    return CartResult.Success;
                }
                catch (Exception)
                {
                    return CartResult.Faild;
                }
            }
            else
            {
                return CartResult.Duplicate;
            }
        }

        public bool IsUserHaveCart(object userId)
        {

            var IsExsit = db.Cart.Any(u => u.UserId == (string)userId && u.IsFinally == false);

            return IsExsit;
        }

        public IEnumerable<CartItem> GetCartItemsOfUser(long orderId)
        {
            var result = db.CartItems.Where(item => item.OrderID == orderId).Include(item => item.Souvenir);
            return result;

        }

        public CartResult ItemCountPlus(long orderId, long itemId, int souvenirId, int amount = 1)
        {
            try
            {
                var souvenir = db.Souvenirs.Find(souvenirId);
                if (souvenir.Inventory == 0)
                {
                    return CartResult.NotEnough;
                }
                souvenir.Inventory = souvenir.Inventory - 1;
                var item = db.CartItems.Single(c => c.OrderID == orderId && c.ItemID == itemId);
                item.Count = item.Count + amount;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return CartResult.Success;
            }
            catch (Exception)
            {
                return CartResult.Faild;
            }
        }

        public CartResult ItemCountMinus(long orderId, long itemId, int souvenirId, int amount = 1)
        {
            try
            {

                var souvenir = db.Souvenirs.Find(souvenirId);
                souvenir.Inventory = souvenir.Inventory + 1;
                var item = db.CartItems.Single(c => c.OrderID == orderId && c.ItemID == itemId);
                if (item.Count != 1)
                {
                    item.Count = item.Count - amount;
                    db.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    db.Entry(item).State = EntityState.Deleted;
                }

                db.SaveChanges();
                return CartResult.Success;
            }
            catch (Exception)
            {
                return CartResult.Faild;
            }
        }

        public PaymentResultDto GetPaymentResult(long orderID)
        {
            var userCart = db.Cart.Find(orderID);
            var item = userCart.Items.Where(i => i.OrderID == orderID);
            var itemCount = 0;
            long totalPrice = 0;
            foreach (var i in item)
            {
                itemCount += i.Count;
                totalPrice += i.Price * i.Count;
            }
            return new PaymentResultDto
            {
                UserId = userCart.UserId,
                SouvenirCount = itemCount,
                Price = totalPrice
            };
        }




        public long GetCartIdByUserId(string userId)
        {
            try
            {
                return db.Cart.First(c => c.UserId == userId && c.IsFinally == false).OrderID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<PaidOrderDto>> GetListOfPaidOrdersAsync()
        {
            return await Task.Run(() =>
            {

                var list = db.Cart.Where(c => c.IsFinally == true).Include(c => c.ApplicationUser).Include(c => c.Items);
                var result = new List<PaidOrderDto>();
                foreach (var item in list)
                {
                    var resItem = new PaidOrderDto
                    {
                        Id = item.OrderID,
                        CustomerAddress = item.ApplicationUser.Address,
                        FullName = item.ApplicationUser.Name,
                        ItemsCount = item.Items.Count,
                        TotalPayment = item.TotalPrice,
                        OrderDate = item.DateTime,
                        Status = item.Status == 0 ? Status.Registered : item.Status == 1 ? Status.InProgress : item.Status == 2 ? Status.Sent : Status.Cancelled
                    };
                    result.Add(resItem);

                }

                return result;
            });

        }


        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(int id)
        {
            return await Task.Run(() =>
            {
                return db.CartItems.Where(i => i.OrderID == id).Include(s => s.Souvenir).Include(s => s.Cart);
            });
        }


        public async Task<IEnumerable<Cart>> GetCartsByUserIdAsync(string userId)
        {
            return await Task.Run(() =>
            {
                var carts = db.Cart.Where(c => c.UserId == userId);
                return carts;

            });
        }

        public long GetTotalPaymentByDate(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                return db.Cart.Where(c => c.DateTime >= startDate && c.DateTime <= endDate).Sum(s => s.TotalPrice);

            }
            catch (InvalidOperationException)
            {

                return 0;    
            }            
        }


        public int CountOrdersByStatus(Status status)
        {
            return db.Cart.Where(c => c.Status == (int)status && c.IsFinally == true).Count();
        }



    }
}
