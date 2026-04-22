using Enum;
using MarketDTO;
using MarketDTO.EmployeeDTO;
using ProductDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientRepository
{
    public interface IClientRepository
    {
        
        Client GetProfile(int clientId);
        Task UpdateProfile(Client client);
        
        IEnumerable<Orders> GetMyOrders(int clientId);
        Orders GetOrderDetails(int orderId);
        Task PlaceOrder(Orders order);
        Task CancelOrder(int orderId);
        
        Task AddToCart(int clientId, int productId, int quantity);
        Task RemoveFromCart(int clientId, int productId);
        IEnumerable<CartItem> GetCart(int clientId);
        Task ClearCart(int clientId);

        Task<Receipt> GetReceipt(int orderId);
    }
}

