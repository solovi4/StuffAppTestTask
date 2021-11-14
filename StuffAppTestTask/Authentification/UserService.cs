using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StuffAppTestTask.DB;

namespace StuffAppTestTask.Authentification
{
    public class UserService : IUserService
    {
        private readonly MyContext _context;
        private User _user;

        public UserService(MyContext context)
        {
            _context = context;
        }
        
        public async Task<User> Authenticate(string username, string password)
        {
            return await _context.Users.Where(u => u.Login == username && u.Password == password).SingleAsync();
        }

        public User GetCurrentUser()
        {
            return _user;
        }
    }
}