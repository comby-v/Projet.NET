using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public partial class User
    {
        public static Boolean create(T_User user, List<T_Tag> tags)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    foreach (T_Tag tag in tags)
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
                    T_User user = bdd.T_User.Where(u => u.Id == idUser).FirstOrDefault();

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

        public static Boolean update(T_User upUser)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities ())
            {
                try
                {
                    var user = new T_User { Id = upUser.Id };
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

        public static T_User get(long idUser)
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

        public static T_User getUserbylogin(String pseudo)
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

        public static T_User getUserbyemail(String email)
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
       public static List<T_Tag> getTaglistfromUser(long idUser)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_User user = bdd.T_User.Where(x => x.Id == idUser).FirstOrDefault();
                    List<T_Tag> tags = user.T_Tag.ToList();
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
                   T_User user = bdd.T_User.Where(x => x.Pseudo == pseudo && x.Password == password).FirstOrDefault();
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

       public static T_User GetUserByPseudo(String pseudo)
       {
           using (ConcertFinderEntities bdd = new ConcertFinderEntities())
           {
               try
               {
                   T_User user = bdd.T_User.Include("T_Tag").Where(x => x.Pseudo == pseudo).FirstOrDefault();
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

       public static List<T_User> GetListUser()
       {
           using (ConcertFinderEntities bdd = new ConcertFinderEntities())
           {
               try
               {
                  return bdd.T_User.ToList();
               }
               catch (Exception)
               {
                   return null;
               }
           }
       }

       static public bool BlockUser(long Id)
       {
           T_User user = new T_User();
           try
           {
               user = get(Id);
               user.Deleted = true;
               return (update(user));
           }
           catch (Exception)
           {
               return false;
           }
       }
    }
}