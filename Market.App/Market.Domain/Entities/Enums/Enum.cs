namespace Market.Domain.Enums
{
    public enum Gender
    {
        unknown,
        Female,
        Male,
        Other = 3  
    }
    [Flags]
    public enum Access
    {
        none,
        Read,
        Write,
        FullAccess = Read | Write
    }
    [Flags]
    public enum Permission
    {
        None = 0,
        ReadUsers = 1 << 0,  
        WriteUsers = 1 << 1, 
        DeleteUsers = 1 << 2, 
        ReadProducts = 1 << 3,  
        WriteProducts = 1 << 4,  
        DeleteProducts = 1 << 5,  
        ReadOrders = 1 << 6,  
        WriteOrders = 1 << 7,  
        DeleteOrders = 1 << 8,  
        ViewReports = 1 << 9,  
        ExportReports = 1 << 10, 
        ManageRoles = 1 << 11, 
        ManageSettings = 1 << 12, 
        ViewAuditLog = 1 << 13,
        User = ReadProducts | ReadOrders,
        Manager = ReadProducts | WriteProducts | ReadOrders | WriteOrders | ViewReports,
        Admin = ReadUsers | WriteUsers | DeleteUsers | ReadProducts | WriteProducts | DeleteProducts | ReadOrders | WriteOrders | DeleteOrders | ViewReports | ExportReports | ManageRoles | ManageSettings | ViewAuditLog,
        All = ~0
    }
    public enum OrderStatus
    {
        Pending,
        Approved,
        Processing,
        Shipped,
        Delivered,
        Cancelled,
        Rejected
    }
    public enum EmployeeType
    {      
        Admin,
        Manager,
        Operator,
        CustomerSupport,
        WarehouseStaff,
        Accountant,
        ProductManager,
        CategoryManager,
        ContentManager,
        MarketingSpecialist,
        Developer,
        DevOps,
    }
    [Flags]
    public enum CustomerTier
    {
        Regular,
        VIP,
        Premium,
        Banned,
        FrequentBuyer,
        HighReturnRate
    }
}
