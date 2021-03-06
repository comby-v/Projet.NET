﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace ConcertFinderMVC.BusinessManagement
{
    public class Tool
    {
        public static string Truncate(string description)
        {
            if (description.Length > 400)
            {
                return (new String(description.ToArray().Take(400).ToArray())) + " ...";
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


        public static bool CreatedByMe(string pseudo, long id)
        {
            DataAccess.T_Event ev = new DataAccess.T_Event();

            ev = DataAccess.Event.Get(id, true);

            return (ev.T_User.Pseudo == pseudo);
        }
    }
}