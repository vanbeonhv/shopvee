namespace shopveeAPI.Services.User;
using DbContext;
using Entities;



public class UserServices : IUserServices
{
    private readonly ShopveeDbContext _dbContext;

    public UserServices(ShopveeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetUser()
    {
        var entities = new List<User>();
        try
        {
            entities = _dbContext.User.ToList();
            return entities;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}