using System.Data;

namespace shopveeAPI.Dapper;

public interface IApplicationDbConnection
{
    IDbConnection GetConnection { get; }

    Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text, IDbTransaction transaction = null, CancellationToken cancellationToken = default);

    Task<List<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text, IDbTransaction transaction = null, CancellationToken cancellationToken = default);

    Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default);

    Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
}