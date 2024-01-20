namespace Models;
public class User
{
    public Guid Id {get; set;}
    public string Email { get; set; } = null!;
    public string Password {get; set;} = null!;
    public int Age {get; set;}
    public string? Address {get; set;}
    public string? RefreshToken {get; set;}
    public string? AccessToken {get; set;}
    public string? Roles {get; set;}
    public DateTime? DateOfBirth {get; set;}
    public string? Phone {get; set;}
}
