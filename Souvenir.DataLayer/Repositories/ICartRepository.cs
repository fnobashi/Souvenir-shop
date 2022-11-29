using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Souvenir.DataLayer.Dto;

namespace Souvenir.DataLayer
{
    public interface ICartRepository
    {
        bool IsUserHaveCart(object userId);
        Cart GetCartById(long id);
        Task<Cart> GetCartByIdAsync(long id);
        long GetCartIdByUserId(string userId);
        void UpdateCart(Cart cart);
        CartResult CreateCart(Cart cart);
        CartResult CreateCart(Cart cart, CartItem item);
        CartResult AddToCart(CartItem cart);
        IEnumerable<CartItem> GetCartItemsOfUser(long orderId);
        CartResult ItemCountPlus(long orderId, long itemId, int souvenirId ,  int amount = 1);
        CartResult ItemCountMinus (long orderId, long itemId, int souvenirId ,  int amount = 1);    
        PaymentResultDto GetPaymentResult(long orderID);
        
        Task<IEnumerable<PaidOrderDto>> GetListOfPaidOrdersAsync();
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int id);
        Task<IEnumerable<Cart>> GetCartsByUserIdAsync(string userId);
        long GetTotalPaymentByDate(DateTime? startDate, DateTime? endDate);
        int CountOrdersByStatus(Status status);


    }

    public enum CartResult
    {
        Success , 
        Duplicate , 
        Faild ,
        NotEnough
    }


}
