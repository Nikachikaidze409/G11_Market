using Market.Domain.Enums;

namespace Market.Domain.Entities;

public class User
{
    public string AuthId { get; set; }
    public string PasswordHash { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public EmployeeType Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}
