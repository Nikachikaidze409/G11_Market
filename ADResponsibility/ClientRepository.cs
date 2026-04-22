using ClientRepository;
using Enum;
using Market.Extensions.AdminFunctons;
using Market.Extensions.Repository.Sql;
using MarketDTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;


namespace IRepository
{
    internal class ClientRepository : BaseRepository<SqlConnection,
    SqlTransaction, SqlParameter, SqlDataReader>, IClientRepository, IAsyncDisposable
    {
        public ClientRepository(string connectionString) : base(connectionString)
        {
        }

        public  Task<Receipt> GetReceipt(int categoryId) => QueryFirstOrDefaultAsync<Receipt>("select p.ProductID,p.ProductName, c.Description as CategoryDescription from Products p inner join Categories c on p.CategoryID = c.CategoryID where p.CategoryID = @CategoryID"
            , new { CategoryID = categoryId });

        public Task ClearCart(int clientId) => ExecuteAsync("delete from CartItems where ClientId = @ClientId", new { ClientId = clientId });

        public async Task RemoveFromCart(int clientId, int productId) => await ExecuteAsync("delete from CartItems where ClientId = @ClientId AND ProductId = @ProductId", new { ClientId = clientId, ProductId = productId });

        public Task AddToCart(int clientId, int productId, int quantity) => ExecuteAsync("INSERT INTO CartItems (ClientId, ProductId, Quantity) VALUES (@ClientId, @ProductId, @Quantity)", new { ClientId = clientId, ProductId = productId, Quantity = quantity });

        public Task CancelOrder(int orderId) => ExecuteAsync("update Orders set FinalStatus = @Status Where OrderId = @OrderId", new { status = OrderStatus.Cancelled, OrderId = orderId });

        public Task PlaceOrder(Orders order) => ExecuteAsync("insert into Orders (OrderId, ClientId, EmployeeId, OrderDate, RequiredDate, ShippedDate, ShipVie, Freight, ShipAddress) " +
                                                                        "values (@OrderId, @ClientId, @EmployeeId, @OrderDate, @RequiredDate, @ShippedDate, @ShipVie, @Freight, @ShipAddress)", new{
                                                                                                                                                                                              // davamtavreb amas
                                                                                                                                                                                               
        });





    }
}
