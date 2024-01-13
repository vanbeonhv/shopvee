using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Entities;
using shopveeAPI.Services.User;
using shopveeAPI.Services.User.Dto.Request;

namespace shopveeAPI.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserServices _services;
    private readonly IValidator<UserRequest> _validator;

    public UserController(IUserServices userServices, IValidator<UserRequest> validator)
    {
        _services = userServices;
        _validator = validator;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult> GetUser()
    {
        var users = await _services.GetUser();
        return Ok(users);
    }

    [HttpPost()]
    public async Task<ActionResult> AddUser([FromBody] UserRequest request)
    {
        ValidationResult validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }
        var user = await _services.AddUserAsync(request);
        return Ok(user);
    }
}