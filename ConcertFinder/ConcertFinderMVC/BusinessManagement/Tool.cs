﻿using System;
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
            if (description.Length > 200)
            {
                return (new String(description.ToArray().Take(200).ToArray())) + " ...";
            }
            else
            {
                return (description);
            }
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