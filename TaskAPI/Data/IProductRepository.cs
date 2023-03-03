using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models;

namespace TaskAPI.Data
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        bool SaveChanges();
        Task<ActionResult<IEnumerable<Product>>> GetAllAsync();
        Task UpdateAsync(Product product);

        bool IsProductExists(int id);

        Task<Product> GetByIdAsync(int id);

        Task DeleteAsync(int id);
    }
}
