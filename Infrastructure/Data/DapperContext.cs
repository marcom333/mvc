

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class DapperContext {
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration) {
        _configuration = configuration;
    }

    public IDbConnection GetConnection() {
        return new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
    }

}