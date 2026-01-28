using System.Data;
using Application.Entities;
using Application.Interface.Repositories;
using Dapper;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DapperContext _context;
    public CategoryRepository(DapperContext context){
        _context = context;
    }

    /*  Regresa la lista de categorias */
    public async Task<List<Category>> GetCategories() {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string query = 
            @"SELECT 
                CategoryId as CategoryId,
                Name, 
                Description
            FROM 
                dbo.Category";
        return (await con.QueryAsync<Category>(query)).ToList();
    }

    /*  Regresa la categoria por id */
    public async Task<Category?> GetCategory(int id) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql =
            @"SELECT 
                c.CategoryId,
                c.Name, 
                c.Description
            FROM dbo.Category c
            WHERE CategoryId = @productId";
        return (await con.QueryAsync<Category>(sql, 
            new {productId=id}
        )).First();
    }

    /* Borra registro de la base de datos */
    public async Task DeleteCategory(Category p) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql = @"DELETE FROM dbo.Category WHERE CategoryId = @CategoryId";
        int rows = await con.ExecuteAsync(sql, p);
        Console.WriteLine(rows);
        if(rows < 1)
            throw new NotImplementedException("No se borro nada");
    }

    public async Task<Category> CreateCategory(Category p) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql = 
            @"INSERT INTO dbo.Category (
                Name,
                Description
            )
            VALUES (
                @Name, 
                @Description
            );
            SELECT CAST(SCOPE_IDENTITY() AS int);";
        int id = await con.ExecuteScalarAsync<int>(sql, p);
        p.CategoryId = id;
        return p;
    }

    public async Task UpdateCategory(Category p) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql = 
            @"UPDATE dbo.Category SET
                Name = @Name, 
                Description = @Description
            WHERE CategoryId = @CategoryId";
        int count = await con.ExecuteAsync(sql, p);
        Console.WriteLine(count);
    }
}