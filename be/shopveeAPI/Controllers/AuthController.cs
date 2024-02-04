using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Services.User.Dto.Request;
using shopveeAPI.UnitOfWork;

namespace shopveeAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UserRequest> _validator;

        public AuthController(IUnitOfWork unitOfWork, IValidator<UserRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UserRequest userRequest)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(userRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            var authResponse = await _unitOfWork._authService.Login(userRequest);
            return authResponse.ResponseCode > 0 ? Ok(authResponse) : BadRequest(authResponse);
        }
    }
}