using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Entities;
using shopveeAPI.Services.User;
using shopveeAPI.Services.User.Dto.Request;
using shopveeAPI.UnitOfWork;

namespace shopveeAPI.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UserRequest> _validator;

    public UserController(IUnitOfWork unitOfWork, IValidator<UserRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult> GetUser()
    {
        var users = await _unitOfWork.userServices.GetUser();
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

        var user = await _unitOfWork.userServices.AddUserAsync(request);
        
        return user == null ? Ok(user) : BadRequest("error");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var result = await _unitOfWork.userServices.DeleteUserAsync(id);
        return Ok(result);
    }
}