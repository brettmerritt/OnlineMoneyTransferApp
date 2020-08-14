using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IUserDAO
    {
        User GetUser(string username);
        User AddUser(string username, string password);
        List<User> GetUsers();
        double GetBalance(User user);
        //List<User> ViewUsers();
    }
}
