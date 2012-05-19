﻿using System;
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
                    throw;
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

        public static Boolean update(USER upUser)
        {
            using (ConcertFinderEntities concert = new ConcertFinderEntities ())
            {
                try
                {
                    var user = new EVENT { EVENT_ID = upUser.USER_ID };
                    concert.EVENTs.Attach(user);
                    concert.ApplyCurrentValues("USER", upUser);
                    concert.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static USER get(long idUser)
        {
            using (ConcertFinderEntities concert = new ConcertFinderEntities())
            {
                try
                {
                    return (concert.USERs.Where(u => u.USER_ID == idUser).FirstOrDefault());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static USER getUserbylogin(String login)
        {
            using (ConcertFinderEntities concert = new ConcertFinderEntities())
            {
                try
                {
                    return (concert.USERs.Where(u => u.USER_LOGIN == login).FirstOrDefault());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static USER getUserbyemail(String email)
        {
            using (ConcertFinderEntities concert = new ConcertFinderEntities())
            {
                try
                {
                    return (concert.USERs.Where(u => u.USER_MAIL == email).FirstOrDefault());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
       public static List<TAG> getTaglistfromUser(long idUser)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    USER user = bdd.USERs.Where(x => x.USER_ID == idUser).FirstOrDefault();
                    List<TAG> tags = user.TAGs.ToList();
                    return tags;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

       public static Boolean validate_user(string pseudo, string password)
       {
           using (ConcertFinderEntities bdd = new ConcertFinderEntities())
           {
               try
               {
                  USER user = bdd.USERs.Where(x => x.USER_LOGIN == pseudo && x.USER_PASSWORD == password).FirstOrDefault();
                   if (user != null)
                   {
                       return true;
                   }
                   return false;
               }
               catch (Exception)
               {
                   return false;
               }
           }
       }
    }
}