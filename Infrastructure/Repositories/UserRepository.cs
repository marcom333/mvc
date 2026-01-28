

using System.Data;
using Application.Entities;
using Application.Interface.Repositories;
using Dapper;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository{
    

    private readonly DapperContext _context;

    public UserRepository(DapperContext context) {
        _context = context;
    }

    public async Task<List<User>> GetUsers() {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string query = 
            @"SELECT 
                u.UserId,
                u.Name, 
                u.Email, 
                u.Password,

                p.ProductId,
                p.Name,
                p.Description,
                p.Price,

                c.CategoyId as CategoryId,
                c.Name, 
                c.Description
            FROM 
                Users u
            LEFT JOIN Product p ON
                p.UserId = u.UserId
            LEFT JOIN Category c ON
                c.CategoyId = p.CategoryId
        ";
        Dictionary<int, User> dict = [];
        IEnumerable<User> users = await con.QueryAsync<User, Product, Category, User>(
            query,
            (user, product, category) => {
                User? current = dict.GetValueOrDefault(user.UserId); 
                if (current == null) {
                    current = user;
                    dict.Add(user.UserId, user);
                }
                if(product != null) {
                    product.Category = category;
                }
                return user;
            },
            splitOn: "ProductId,CategoryId"
        );


        return users.ToList();
    }

    /*
        CREATE TABLE dbo.Users(
            UserId INT IDENTITY PRIMARY KEY,
            Name NVARCHAR(100) NOT NULL,
            Email NVARCHAR(256) NOT NULL,
            Password NVARCHAR(256) NOT NULL
        );    
    */

    public async Task<User?> GetUser(int id) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql =
            @"SELECT 
                u.UserId,
                u.Name, 
                u.Email, 
                u.Password,

                p.ProductId,
                p.Name,
                p.Description,
                p.Price,

                c.CategoyId as CategoryId,
                c.Name, 
                c.Description
            FROM dbo.Users u
            LEFT JOIN Product p ON
                p.UserId = u.UserId
            LEFT JOIN Category c ON
                c.CategoyId = p.CategoryId
            WHERE u.UserId = @id";
        User? outputUser = null;
        User users = con.Query<User, Product, Category, User>(
            sql,
            (user, product, category) =>{
                if (outputUser == null)
                    outputUser = user;
                if (product != null){
                    product.Category = category; 
                    outputUser.Products.Add(product);
                }
                return outputUser;
            },
            new { id },
            splitOn: "ProductId,CategoryId"
        ).First();
        return users;
    }
    public async Task DeleteUser(User p) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql = @"DELETE FROM dbo.Users WHERE UserId = @UserId";
        int rows = await con.ExecuteAsync(sql, p);
        Console.WriteLine(rows);
        if(rows < 1)
            throw new NotImplementedException("No se borro nada");
    }
    public async Task<User> CreateUser(User p) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql = 
            @"INSERT INTO dbo.Users (
                Name, 
                Password,
                Email
            )
            VALUES (
                @Name, 
                @Password,
                @Email
            );
            SELECT CAST(SCOPE_IDENTITY() AS int);";
        int id = await con.ExecuteScalarAsync<int>(sql, p);
        p.UserId = id;
        return p;
    }
    public async Task UpdateUser(User p) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql = 
            @"UPDATE dbo.Users SET
                Name = @Name, 
                Password = @Password
            WHERE UserId = @UserId";
        int count = await con.ExecuteAsync(sql, p);
        Console.WriteLine(count);
    }

}