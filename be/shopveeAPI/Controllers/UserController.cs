using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Models;
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
    public async Task<ActionResult> GetAllUser()
    {
        var users = await _unitOfWork._userGenericService.GetAll();
        return Ok(users);
    }

    [HttpPost()]
    public async Task<ActionResult> AddUser([FromBody] UserRequest request)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }

        //Add mapping method later
        var entiry = new User()
        {
            Email = request.Email,
            Password = request.Password
        };
        var res = await _unitOfWork._userGenericService.Add(entiry);

        return res == 0 ? BadRequest("error") : Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var result = await _unitOfWork._userGenericService.Delete(id);
        return Ok(result);
    }
}