using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace shopveeAPI.Services.User.Dto.Request;

public class UserRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}