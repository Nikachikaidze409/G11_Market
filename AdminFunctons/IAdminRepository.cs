using Enum;
using MarketDTO.EmployeeDTO;
using ProductDTO;
namespace Market.Extensions.AdminFunctons;

public interface IAdminRepository // me davamtavreb amas
{
    
    IEnumerable<Employee> GetAllEmployees();
    Employee GetEmployeeById(int employeeId);
    void HireEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    void FireEmployee(int employeeId);
    IEnumerable<Product> GetAllProducts();
    Product GetProductById(int productId);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void RemoveProduct(int productId);
    bool IsProductInStock(int productId);
    IEnumerable<Order> GetAllOrders();
    IEnumerable<Order> GetOrdersByStatus(OrderStatus status);
    void UpdateOrderStatus(int orderId, OrderStatus status);
    void CancelOrder(int orderId);
    Receipt GenerateReceipt(int orderId);
    IEnumerable<Order> GetDailyReport(DateTime date);
    decimal GetTotalRevenue(DateTime from, DateTime to);
    void Save();
}

