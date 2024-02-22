namespace shopveeAPI.Services.User;

public interface IUserServiceDapper
{
    Task<List<Models.User>> GetAllAsync();
}