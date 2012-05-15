using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public class T_User
    {
        public static Boolean create(USER user)
        {
            using (ConcertFinderEntities1 concert = new ConcertFinderEntities1())
            {
                long id = user.USER_ID;

                concert.AddToUSERs(user);
                concert.SaveChanges();

                USER us = concert.USERs.Where(u => u.USER_ID == user.USER_ID).FirstOrDefault();

                return (us.USER_ID == id);
            }
        }
    }
}