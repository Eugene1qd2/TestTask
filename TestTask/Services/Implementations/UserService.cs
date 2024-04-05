using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser()
        {
            var result = await (from od in _context.Orders
                                group od by od.User into g
                                select g)
                                .OrderByDescending(gr => gr.Count())
                                .FirstOrDefaultAsync();

            return result.Key;
        }

        public async Task<List<User>> GetUsers()
        {
            var result=await _context.Users
                .Where(us=>us.Status==Enums.UserStatus.Inactive)
                .ToListAsync();
            return result;
        }
    }
}
