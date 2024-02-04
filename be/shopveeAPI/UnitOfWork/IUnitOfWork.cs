using shopveeAPI.Services.Auth;
using shopveeAPI.Services.User;

namespace shopveeAPI.UnitOfWork;

public interface IUnitOfWork
{
    public IUserGenericService _userGenericService { get; set; }
    public IAuthService _authService { get; set; }

    int SaveChange();
}