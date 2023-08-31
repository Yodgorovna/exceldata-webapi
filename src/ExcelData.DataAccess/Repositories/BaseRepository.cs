using Dapper;
using ExcelData.DataAccess.Handlers;
using Npgsql;

namespace ExcelData.DataAccess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository()
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection(
            "Host = localhost;" +
            "Port = 5432;" +
            "Database = Excel-db;" +
            "User Id = postgres;" +
            "Password = 0409;");
    }
}
