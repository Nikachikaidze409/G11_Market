using System.Data;
using System.Diagnostics.Contracts;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
namespace Market.Extensions.Repository.Sql;

public abstract class BaseRepository<TConnection, TTransaction, TParameter, TDataReader> : IDisposable, IAsyncDisposable
    where TConnection : DbConnection, new()
    where TTransaction : DbTransaction
    where TParameter : DbParameter
    where TDataReader : DbDataReader
{
    private readonly string _connectionString;
    private TConnection? _connection;
    private bool _disposed;

    IDisposable? _disposableResource = new MemoryStream();
    IAsyncDisposable? _asyncDisposableResource = new MemoryStream();

    public BaseRepository(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public virtual TConnection GetConnection()
    {
        var connection = new TConnection();
        connection.ConnectionString = _connectionString;
        return connection;
    }

    public TConnection CreateConnection()
    {
        ThrowIfDisposed();
        return _connection ??= GetConnection();
    }
    public void OpenConnection()
    {
        var connection = CreateConnection();
        if (_connection?.State != ConnectionState.Open)
            connection?.Open();
    }
    public void CloseConnection()
    {
        if (_connection?.State != ConnectionState.Closed)
            _connection?.Close();
    }

    public Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null)
    {
        using var connection = GetConnection();
        return connection.QueryFirstOrDefaultAsync<T>(sql, param);
    }
    public Task ExecuteAsync(string sql, object? param = null, CommandType commandType = CommandType.Text)
    {
        using var connection = GetConnection();
        return connection.ExecuteAsync(sql, param, commandType: commandType);
    }


    public void ExecuteInTransaction(Action<IDbConnection, IDbTransaction> action)
    {
        using var connection = GetConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();
        try
        {
            action(connection, transaction);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }


    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(BaseRepository<TConnection, TTransaction, TParameter, TDataReader>));
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposableResource?.Dispose();
            _disposableResource = null;

            if (_asyncDisposableResource is IDisposable disposable)
            {
                disposable.Dispose();
                _asyncDisposableResource = null;
            }
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_asyncDisposableResource is not null)
        {
            await _asyncDisposableResource.DisposeAsync().ConfigureAwait(false);
        }

        if (_disposableResource is IAsyncDisposable disposable)
        {
            await disposable.DisposeAsync().ConfigureAwait(false);
        }
        else
        {
            _disposableResource?.Dispose();
        }

        _asyncDisposableResource = null;
        _disposableResource = null;
    }

}

