using Dapper;
using shopveeAPI.Dapper;

namespace shopveeAPI.Services.User;

public class UserServiceDapper : BaseApplicationService, IUserServiceDapper
{
    public UserServiceDapper(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    
    public async Task<List<Models.User>> GetAllAsync()
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", Guid.NewGuid());
        var result = await DbConnection.QueryAsync<Models.User>("SELECT * FROM User", parameters);
        return result;
    }
}