using System;
using System.Collections.Generic;
using System.Text;

namespace Enum
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
        None,
        ReadUsers,
        WriteUsers,
        DeleteUsers,
        ManageProducts,
        UserManager = ReadUsers | WriteUsers | DeleteUsers,
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
