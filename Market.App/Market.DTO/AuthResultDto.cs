namespace Market.DTO;

public class AuthResultDto
{
    public bool Success { get; set; }
    public string AuthId { get; set; } 
    public string Token { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public bool RequiresPassword { get; set; }
}