using Microsoft.Data.SqlClient;
using Market.Extensions.Repository.Sql;
public sealed class DBSql : BaseRepository<SqlConnection,
    SqlTransaction, SqlParameter, SqlDataReader>
{
    public DBSql(string connectionString) : base(connectionString)
    {
    }
}