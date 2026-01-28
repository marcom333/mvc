using Application.Entities;
namespace Application.Interface.Service;

public interface ICategoryService{
    public Task<Category?> GetCategory(int id);
}