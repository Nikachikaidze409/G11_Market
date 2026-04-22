using ProductDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminFunctons
{
    public interface IManagerRepository
    {
        
        Product GetProductById(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void RemoveProduct(int productId);
        IEnumerable<Product> GetLowStockProducts(int threshold);
        void RestockProduct(int productId, int quantity);
        IEnumerable<Order> GetPendingOrders();
        void ApproveOrder(int orderId);
        void RejectOrder(int orderId, string reason);

        Receipt GenerateReceipt(int orderId);

        void Save();
    }

}
