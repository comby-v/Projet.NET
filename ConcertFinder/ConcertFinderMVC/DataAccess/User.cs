using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.BusinessManagement;
using System.Text.RegularExpressions;

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

        public static Boolean Update(string pseudo, Models.ParameterModel form)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {

                    SimpleAES encryptor = new SimpleAES();
            
                    T_User user = bdd.T_User.Include("T_Tag").Include("T_Event").Where(x => x.Pseudo == pseudo).FirstOrDefault();

                    if (user.Ville != form.MyCity && form.MyCity != null)
                    {
                        user.Ville = form.MyCity;
                    }

                    if ((form.NewPassword != null) && (form.OldPassword != null) && (form.ConfirmPassword != null))
                    {
                        if (User.ValidateUser(pseudo, encryptor.EncryptToString(form.OldPassword)) && encryptor.EncryptToString(form.NewPassword) != user.Password)
                        {
                            user.Password = encryptor.EncryptToString(form.NewPassword);
                        }
                    }

                    if (user.Mail != form.Email && (form.Email != null))
                    {
                        user.Mail = form.Email;
                    }

                    List<DataAccess.T_Tag> listTag = new List<DataAccess.T_Tag>();
                    if (form.Tag != null)
                    {
                        
                        string[] split = form.Tag.Split(new Char[] { ' ', ',', '.', ';' });
                        foreach (string str in split)
                        {
                            if (str.Length > 2)
                            {
                                Regex r = new Regex("[a-z1-9*]");
                                Match m = r.Match(str);
                                if (m.Success)
                                {
                                    str.ToLower();
                                    DataAccess.T_Tag tag = new DataAccess.T_Tag()
                                    {
                                        Name = str
                                    };
                                    if (bdd.T_Tag.Where(t => t.Name == tag.Name).FirstOrDefault() == null)
                                    {
                                        DataAccess.Tag.Create(tag);
                                    }

                                    tag = bdd.T_Tag.Where(t => t.Name == tag.Name).FirstOrDefault();


                                    listTag.Add(tag);

                                }
                            }
                        }
                    }

                    
                    user.T_Tag.Clear();
                    foreach (T_Tag tag in listTag)
                    {
                        bdd.Attach(tag);
                        user.T_Tag.Add(tag);
                    }

                    var uuser = new T_User { Id = user.Id };
                   
                    bdd.ApplyCurrentValues("T_User", user);
                    bdd.SaveChanges();
                }
                catch (System.Data.UpdateException ex)
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
                  return bdd.T_User.Where(x => x.Deleted == false).OrderByDescending(x => x.Id).Take(16).ToList();
               }
               catch (Exception)
               {
                   return null;
               }
           }
       }

       public static List<T_User> GetNextUsers(long last_id)
       {
           using (ConcertFinderEntities bdd = new ConcertFinderEntities())
           {
               try
               {
                   return bdd.T_User.OrderByDescending(x => x.Id).Where(x => x.Deleted == false && x.Id < last_id).Take(16).ToList();
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