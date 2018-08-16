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
        bool CreateUser(RegisterUser newUser);

        // User UserLogin(string username, string password);

        User GetUser(string username);
    }
}
