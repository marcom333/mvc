using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Application.Interface.Service;
namespace Infrastructure.Data;
public class DapperContext:IDapperService
{
    private readonly IConfiguration _configuration;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IDbConnection GetConnection()
    {
        return new SqlConnection(_configuration["ConnectionString:DefaultConnection"]);
    }
}