namespace shopveeAPI.Dapper;

public class BaseApplicationService
{
    public BaseApplicationService(IServiceProvider serviceProvider)
    {
        DbConnection = serviceProvider.GetRequiredService<IApplicationDbConnection>();
    }
    
    public IApplicationDbConnection DbConnection { get; }
}