using Microsoft.AspNetCore.Mvc;
using TaskAPI.DTOs;
using TaskAPI.Models;
namespace TaskAPI.Data
{
    public interface IRegistrationRepository
    {
        Task<ActionResult<IEnumerable<User>>> GetAllAsync();
        List<Username> Getusername();
        Task<User> GetByIdAsync(int id);
        Task<User> PostAsync(User user);
        Task putnewpassword(newpasswordDTO dto);
        bool SaveChanges();
        User userdata(UserLogin user);
        User GetuserById(int id);
    }
}
