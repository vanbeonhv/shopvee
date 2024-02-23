using System.Data;
using Dapper;
using Npgsql;


namespace shopveeAPI.Dapper;

public class ApplicationDbConnection : IApplicationDbConnection
{
    private readonly IDbConnection _connection;

    public ApplicationDbConnection(IConfiguration configuration)
    {
        _connection = new NpgsqlConnection(configuration.GetConnectionString("CONNECTION_STRING")
            .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD")));
    }

    public IDbConnection GetConnection => this._connection;
    
    public void Dispose()
    {
        _connection.Dispose();
    }


    public Task<int> ExecuteAsync(string sql, object param = null,
        CommandType commandType = CommandType.Text,
        IDbTransaction transaction = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<T>> QueryAsync<T>(string sql, object param = null,
        CommandType commandType = CommandType.Text,
        IDbTransaction transaction = null, CancellationToken cancellationToken = default)
    {
        return (await _connection.QueryAsync<T>(sql, param, transaction, commandType: commandType)).AsList();
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null,
        CommandType commandType = CommandType.Text,
        IDbTransaction transaction = null, CancellationToken cancellationToken = default)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object param,
        CommandType commandType,
        IDbTransaction transaction, CancellationToken cancellationToken = default)
    {
        return await _connection.QuerySingleAsync<T>(sql, param, transaction, commandType: commandType);
    }
}