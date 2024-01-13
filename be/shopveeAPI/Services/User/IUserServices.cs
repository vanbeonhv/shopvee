using shopveeAPI.Services.User.Dto.Request;

namespace shopveeAPI.Services.User;
using Entities;
public interface IUserServices
{
    Task<List<User>> GetUser();
    Task<User> AddUserAsync(UserRequest request);
}