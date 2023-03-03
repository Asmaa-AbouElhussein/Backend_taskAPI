using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using TaskAPI.Models;
using TaskAPI.Data;
using TaskAPI.DTOs;

namespace TaskAPI.Data
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly TaskDataContext _context;

        public RegistrationRepository(TaskDataContext context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);

        }

        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public List<Username> Getusername()
        {
            var data = _context.Users.Where(d => d.username != "Admin22").Select(d => new Username() { username = d.username }).ToList(); ;
            return data;
        }
        public async Task<User> PostAsync(User user)
        {
            if (_context.Users.Where(e => e.email == user.email).FirstOrDefault() == null)
            {

                user.password = user.password.HashPassword();
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            else
            {
                return null;
            }

        }
        public async Task putnewpassword(newpasswordDTO dto)
        {
            var obj = _context.Users.Where(o => o.email == dto.email).Select(o => new { o.id, o.username }).FirstOrDefault();
            dto.password = dto.password.HashPassword();
            User objreg = new User { email = dto.email, password = dto.password, id = obj.id, username = obj.username};
            _context.Entry(objreg).State = EntityState.Modified;

         
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public User userdata(UserLogin user)
        {
            var usdata = _context.Users.Where(r => r.username == user.username
           && r.password == user.password.HashPassword()).FirstOrDefault();
            return usdata;
        }


        public User GetuserById(int id)
        {
            return  _context.Users.Find(id);

        }
    }
}
