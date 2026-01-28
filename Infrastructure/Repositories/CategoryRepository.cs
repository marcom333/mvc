using Application.Entities;
using Application.Interface.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository{
    private readonly DapperContext _context;
    public CategoryRepository(DapperContext context){
        _context = context;
    }
    
    public async Task<Category?> GetCategory(int id)
    {
        throw new NotImplementedException();
    }
}