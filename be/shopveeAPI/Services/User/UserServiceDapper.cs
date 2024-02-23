using Dapper;
using shopveeAPI.Dapper;

namespace shopveeAPI.Services.User;

public class UserServiceDapper : BaseApplicationService, IUserServiceDapper
{
    public UserServiceDapper(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    
    public async Task<List<Models.User>> GetAllAsync(Guid? id)
    {
        var parameters = new DynamicParameters();
        string query;
        if (id == null)
        {
            query = "select * from user";
        }
        else
        {
            parameters.Add("Id", id);
            query = "select * from user where id = @Id";
        }
        var result = await DbConnection.QueryAsync<Models.User>(query, parameters);
        return result;
    }
}