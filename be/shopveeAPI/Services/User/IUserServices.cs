namespace shopveeAPI.Services.User;
using Entities;
public interface IUserServices
{
    Task<List<User>> GetUser();
}