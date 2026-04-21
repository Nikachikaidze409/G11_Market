using Market.Domain.Enums;

namespace Market.DTO;

public class UserDto
{
    public string AuthId { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public EmployeeType Role { get; set; }
    public Permission Permissions { get; set; }

}