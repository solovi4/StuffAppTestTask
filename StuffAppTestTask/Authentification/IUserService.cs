using System.Threading.Tasks;
using StuffAppTestTask.DB;

namespace StuffAppTestTask.Authentification
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        User GetCurrentUser();
    }
}