using System.Data;
using Application.Entities;
using Application.Interface.Repositories;
using Dapper;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DapperContext _dapper;
    public CategoryRepository(DapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<Category> GetCategory(int id)
    {
        Category? category;
        string sql = @"SELECT CategoryId, Name, Description FROM dbo.Category WHERE CategoryId = @ID";

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();

            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    category = await conn.QueryFirstOrDefaultAsync<Category>(sql, new { ID = id }, tx);
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return category != null? category: new Category();
        }
    }

    public async Task<List<Category>> GetCategories()
    {
        List<Category>? categories;
        string sql = @"SELECT CategoryId, Name, Description FROM dbo.Category";

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();

            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    categories = (await conn.QueryAsync<Category>(sql, null, tx)).ToList();
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return categories;
        }
    }

    public async Task<bool> CreateCategory(Category category)
    {
        string sql = @"INSERT INTO dbo.Category (Name, Description) 
            Values (@Name, @Description) SELECT CAST(SCOPE_IDENTITY() AS INT)";

        int id = 0;

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();
            
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    id = await conn.ExecuteScalarAsync<int>(sql, category, tx);
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
    
    public async Task<bool> UpdateCategory(Category category)
    {
        string sql = @"
            UPDATE dbo.Category SET Name = @Name, Description = @Description
            WHERE CategoryId = @CategoryId";

        int filas = 0;

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();
            
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    filas = await conn.ExecuteAsync(sql, category, tx);
                    if(filas == 0) {                          
                        throw new InvalidOperationException("No se actualizó la categoria. Error UpdateCategory.");    
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
    
    public async Task<bool> DeleteCategory(int id)
    {
        string sql = @"
            DELETE FROM dbo.Category WHERE CategoryId = @ID";

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