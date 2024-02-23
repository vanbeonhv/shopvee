using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Models;
using shopveeAPI.Services.User;
using shopveeAPI.Services.User.Dto.Request;
using shopveeAPI.UnitOfWork;
using shopveeAPI.Utils;

namespace shopveeAPI.Controllers;

[Route("api/user")]
[ApiController]
// [Authorize]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UserRequest> _validator;
    private readonly IUserServiceDapper _userServiceDapper;

    public UserController(IUnitOfWork unitOfWork, IValidator<UserRequest> validator, IConfiguration configuration, IUserServiceDapper userServiceDapper)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _configuration = configuration;
        _userServiceDapper = userServiceDapper;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult> GetAllUser()
    {
        var users = await _unitOfWork._userGenericService.GetAllAsync();
        return Ok(users);
    }
    
    [HttpGet("get-dapper")]
    public async Task<ActionResult> GetAllUserDapper(Guid? id = null)
    {
        var users = await _userServiceDapper.GetAllAsync(id);
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

        var expired = _configuration["JWT:RefreshTokenValidityInDays"] ?? String.Empty;
        var refreshToken = Helper.GenerateRefreshToken();

        //Add mapping method later
        var entity = new User()
        {
            Email = request.Email,
            Password = request.Password,
            RefreshToken = refreshToken,
            RefreshTokenExpired = DateTime.Now.AddDays(int.Parse(expired))
        };
        var res = await _unitOfWork._userGenericService.Add(entity);

        return res == 0 ? BadRequest("error") : Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var result = await _unitOfWork._userGenericService.Delete(id);
        return Ok(result);
    }
}