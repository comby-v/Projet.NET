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
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    return (bdd.T_User.Where(u => u.Pseudo == pseudo).FirstOrDefault().Role.Equals("Administrateur"));
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}