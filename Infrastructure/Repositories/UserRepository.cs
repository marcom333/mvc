using System.Data;
using Application.Entities;
using Application.Interface.Repositories;
using Dapper;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _dapper;
    public UserRepository(DapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<User> GetUser(int id)
    {
        User? user;
        string sql = @"SELECT UserId, Name, Primer_Apellido, Segundo_Apellido FROM dbo.[User] WHERE UserId = @ID";

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();

            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    user = await conn.QueryFirstOrDefaultAsync<User>(sql, new { ID = id }, tx);
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return user != null? user: new User();
        }
    }

    public async Task<List<User>> GetUsers()
    {
        List<User>? users;
        string sql = @"SELECT UserId, Name, Primer_Apellido, Segundo_Apellido FROM dbo.[User]";

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();

            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    users = (await conn.QueryAsync<User>(sql, null, tx)).ToList();
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return users;
        }
    }

    public async Task<bool> CreateUser(User user)
    {
        string sql = @"INSERT INTO dbo.[User] (Name, Primer_Apellido, Segundo_Apellido) 
            Values (@Name, @Primer_Apellido, @Segundo_Apellido) SELECT CAST(SCOPE_IDENTITY() AS INT)";

        int id = 0;

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();
            
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    id = await conn.ExecuteScalarAsync<int>(sql, user, tx);
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return id != 0;
        }        
    }
    
    public async Task<bool> UpdateUser(User user)
    {
        string sql = @"
            UPDATE dbo.[User] SET Name = @Name, Primer_Apellido = @Primer_Apellido, Segundo_Apellido = @Segundo_Apellido
            WHERE UserId = @UserId";

        int filas = 0;

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();
            
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    filas = await conn.ExecuteAsync(sql, user, tx);
                    if(filas == 0) {                          
                        throw new InvalidOperationException("No se actualizó el usuario. Error UpdateUser.");    
                    }
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return filas > 0;
        }        
    }
    
    public async Task<bool> DeleteUser(int id)
    {
        string sql = @"
            DELETE FROM dbo.[User] WHERE UserId = @ID";

        bool eliminado = false;

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();
            
            using (var tx = conn.BeginTransaction())
            {
                try
                {                    
                    eliminado = await conn.ExecuteAsync(sql, new { ID = id }, tx) > 0;
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return eliminado;
        }        
    }
}