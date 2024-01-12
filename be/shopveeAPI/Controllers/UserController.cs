using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Services.User;

namespace shopveeAPI.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserServices _userServices;

    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult> GetUser()
    {
        var users = await _userServices.GetUser();
        return Ok(users);
    }
}