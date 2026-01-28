using Application.Entities;

namespace Application.Interface.Repositories;

public interface ICategoryRepository{
    public Task<Category?> GetCategory(int id);
}