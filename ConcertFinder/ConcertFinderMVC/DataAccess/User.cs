using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public partial class User
    {
        public static Boolean create(User user, List<Tag> tags)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    foreach (Tag tag in tags)
                    {
                        bdd.Attach(tag);
                        user.T_Tag.Add(tag);
                    }
                    bdd.AddToT_User(user);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                   
                }
                return (true);
            }
        }

        public static Boolean delete(long idUser)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    User user = bdd.T_User.Where(u => u.Id == idUser).FirstOrDefault();

                    user.Role = "deactivated";

                    bdd.AddToT_User(user);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        public static Boolean update(User upUser)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities ())
            {
                try
                {
                    var user = new User { Id = upUser.Id };
                    bdd.T_User.Attach(user);
                    bdd.ApplyCurrentValues("T_User", upUser);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return true;
            }
        }

        public static User get(long idUser)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    return (bdd.T_User.Where(u => u.Id == idUser).FirstOrDefault());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static User getUserbylogin(String pseudo)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    return (bdd.T_User.Where(u => u.Pseudo == pseudo).FirstOrDefault());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static User getUserbyemail(String email)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    return (bdd.T_User.Where(u => u.Mail == email).FirstOrDefault());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
       public static List<Tag> getTaglistfromUser(long idUser)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    User user = bdd.T_User.Where(x => x.Id == idUser).FirstOrDefault();
                    List<Tag> tags = user.T_Tag.ToList();
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
                   User user = bdd.T_User.Where(x => x.Pseudo == pseudo && x.Password == password).FirstOrDefault();
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

       public static User GetUserByPseudo(String pseudo)
       {
           using (ConcertFinderEntities bdd = new ConcertFinderEntities())
           {
               try
               {
                   User user = bdd.T_User.Include("T_Tag").Where(x => x.Pseudo == pseudo).FirstOrDefault();
                   if (user != null)
                   {
                       return user;
                   }
                   return null;
               }
               catch (Exception)
               {
                   throw;
                 
               }
           }
       }
    }
}