using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.DAL
{
    public interface IForumPostSqlDAO
    {
        List<ForumPost> GetAllFoumPosts();
    }
}