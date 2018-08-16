using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.DAL
{
    public interface IUserSqlDAO
    {
        bool CreateUser(User newUser);

        List<string> GetUsernames();

        User GetUser(string username);

        bool CheckUsernameAvailability(string username, List<string> allUsernames);

        List<List<Book>> GetReadingList(string username);
    }
}
