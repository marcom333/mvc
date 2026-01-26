using Application.Entities;

namespace Application.Interface.Repositories;

public interface IProductRepository
{
    public List<Product> GetProducts();
}