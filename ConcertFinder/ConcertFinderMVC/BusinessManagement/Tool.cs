using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;

namespace ConcertFinderMVC.BusinessManagement
{
    public class Tool
    {
        public static string Truncate(string description)
        {
            return description.ToArray().Take(100).ToString();
        }

        public static bool IsAdmin(string pseudo)
        {
            return BusinessManagement.User.GetUserByPseudo(pseudo).Role.Equals(Models.UserModel.GetRoleType((int)Models.eRole.Admin));
        }
        public static bool IsModerator(string pseudo)
        {
            return BusinessManagement.User.GetUserByPseudo(pseudo).Role.Equals(Models.UserModel.GetRoleType((int)Models.eRole.Moderateur));
        }
    }
}