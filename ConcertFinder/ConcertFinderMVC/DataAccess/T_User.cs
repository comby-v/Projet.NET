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
            using (ConcertFinderEntities concert = new ConcertFinderEntities())
            {

                try
                {
                    concert.AddToUSERs(user);
                    concert.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return (true);
            }
        }

        public static Boolean delete(long idUser)
        {
            using (ConcertFinderEntities concert = new ConcertFinderEntities())
            {
                try
                {
                    USER user = concert.USERs.Where(u => u.USER_ID == idUser).FirstOrDefault();

                    user.USER_ROLE = "deactivated";

                    concert.AddToUSERs(user);
                    concert.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        public static Boolean update(long idUser)
        {
            using (ConcertFinderEntities concert = new ConcertFinderEntities ())
            {
                try
                {
                    USER user = concert.USERs.Where(u => u.USER_ID == idUser).FirstOrDefault();


                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
    }
}