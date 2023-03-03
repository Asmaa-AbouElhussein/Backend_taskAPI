using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;

namespace TaskAPI.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly TaskDataContext _context;

        public ProductRepository(TaskDataContext context)
        {
            _context = context;
        }
        //public async Task<User> AddAsync(User user)
        //{
        //    if (_context.Users.Where(e => e.email == user.email).FirstOrDefault() == null)
        //    {

        //        user.password = user.password.HashPassword();
        //        _context.Users.Add(user);
        //        await _context.SaveChangesAsync();
        //        return user;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
           
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task UpdateAsync(Product newProduct)
        {
            _context.Entry(newProduct).State = EntityState.Modified;

        }

        public bool IsProductExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);

        }
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(n => n.id == id);
            _context.Products.Remove(result);
           
        }
    }
}
