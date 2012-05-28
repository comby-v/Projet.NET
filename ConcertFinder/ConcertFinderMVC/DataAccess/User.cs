using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public partial class User
    {
        public static Boolean Create(T_User user, List<T_Tag> tags)
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

        public static Boolean Delete(long idUser)
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

        public static Boolean Update(T_User upUser)
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

        public static T_User Get(long idUser)
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

        public static T_User GetUserByEmail(String email)
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
       public static List<T_Tag> GetTagListFromUser(long idUser)
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

       public static Boolean ValidateUser(string pseudo, string password)
       {
           using (ConcertFinderEntities bdd = new ConcertFinderEntities())
           {
               try
               {
                   T_User user = bdd.T_User.Where(x => x.Pseudo == pseudo && x.Password == password && x.Deleted == false) .FirstOrDefault();
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
                   T_User user = bdd.T_User.Include("T_Tag").Include("T_Event").Where(x => x.Pseudo == pseudo).FirstOrDefault();
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
               user = Get(Id);
               user.Deleted = true;
               return (Update(user));
           }
           catch (Exception)
           {
               return false;
           }
       }

       static public bool ChangePassword(string pseudo, string new_password)
       {
           try
           {
               T_User user = GetUserByPseudo(pseudo);
               user.Password = new_password;
               return (Update(user));
           }
           catch (Exception)
           {
               return false;
           }
       }

       static public bool ChangeRole(long id, string role)
       {
           try
           {
               T_User user = Get(id);
               user.Role = role;
               return (Update(user));
           }
           catch (Exception)
           {
               return false;
           }
       }
    }
}