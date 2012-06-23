using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;
using ConcertFinderMVC.Models;
using System.Text.RegularExpressions;

namespace ConcertFinderMVC.BusinessManagement
{
    public class User
    {
        public static Boolean create(RegisterModel form)
        {
            string role = "";

            if (form.Pseudo.Equals("superadmin"))
            {
                role = UserModel.GetRoleType((int)eRole.Admin);
            }
            else
            {
                role = UserModel.GetRoleType((int)eRole.User);
            }
            SimpleAES encryptor = new SimpleAES();
            DataAccess.T_User user = new DataAccess.T_User()
            {
                Name = form.Name,
                Firstname = form.Firstname,
                Pseudo = form.Pseudo,
                Mail = form.Email,
                Ville = form.City,
                Password = encryptor.EncryptToString(form.Password),
                Role = role,
                Deleted = false,
                T_Event = null,
                T_Notification = null,
                T_Tag = null
            };

            List<DataAccess.T_Tag> listTag = new List<DataAccess.T_Tag>();
            if (form.Tags != null && form.Tags != "")
            {
                string[] split = form.Tags.Split(new Char[] { ' ', ',', '.', ';' });
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
                            if (DataAccess.Tag.Get(str) == null)
                            {
                                DataAccess.Tag.Create(tag);
                            }

                            tag = DataAccess.Tag.Get(str);
                            listTag.Add(tag);
                        }
                    }
                }
            }
            return DataAccess.User.Create(user, listTag);
        }

        public static Boolean ValidateUser(string pseudo, string password)
        {
            return DataAccess.User.ValidateUser(pseudo, password);
        }

        public static DataAccess.T_User GetUserByPseudo(String pseudo)
        {
            return DataAccess.User.GetUserByPseudo(pseudo);
        }

        public static List<Models.UserItem> GetListUser()
        {
            List <T_User> listUser = DataAccess.User.GetListUser();
            List<Models.UserItem> listUserItem = new List<UserItem>();

            foreach (T_User user in listUser)
            {
                Models.UserItem userIt = new UserItem()
                {
                    Id = user.Id,
                    Name = user.Name,
                    FirstName = user.Firstname,
                    Login = user.Pseudo,
                    Mail = user.Mail,
                    City = user.Ville,
                    Role = user.Role,
                    Deleted = user.Deleted
                };

                listUserItem.Add(userIt);
            }
            return listUserItem;
        }

        public static List<Models.UserItem> GetNextUsers(long last_id)
        {
            List<T_User> listUser = DataAccess.User.GetNextUsers(last_id);
            List<Models.UserItem> listUserItem = new List<UserItem>();

            foreach (T_User user in listUser)
            {
                Models.UserItem userIt = new UserItem()
                {
                    Id = user.Id,
                    Name = user.Name,
                    FirstName = user.Firstname,
                    Login = user.Pseudo,
                    Mail = user.Mail,
                    City = user.Ville,
                    Role = user.Role
                };

                listUserItem.Add(userIt);
            }
            return listUserItem;
        }

        static public bool BlockUser(long Id)
        {
            return (DataAccess.User.BlockUser(Id));
        }

        static public bool ChangeParameter(string pseudo, ParameterModel form)
        {
            SimpleAES encryptor = new SimpleAES();
            
            T_User user = DataAccess.User.GetUserByPseudo(pseudo);

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

            if (form.Tag != null)
            {
                List<DataAccess.T_Tag> listTag = new List<DataAccess.T_Tag>();    
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
                            if (DataAccess.Tag.Get(str) == null)
                            {
                                DataAccess.Tag.Create(tag);
                            }

                            tag = DataAccess.Tag.Get(str);
                            bool find = false;
                            if (user.T_Tag.Count > 0)
                            {
                                foreach (T_Tag usertag in user.T_Tag)
                                {
                                    if ((usertag.Name.Equals(tag.Name)))
                                    {
                                        find = true;
                                        break;
                                    }
                                }
                                if (!find)
                                {
                                    listTag.Add(tag);
                                        
                                }
                                find = false;
                            }
                            else
                            {
                                listTag.Add(tag);
                            }
                        }
                    }
                }
                if (listTag.Count > 0)
                {
                    return (DataAccess.User.Update(user, listTag));
                }
            }

            return (DataAccess.User.Update(user));
        }

        static public bool ForgotPassword(string email)
        {
            SimpleAES encryptor = new SimpleAES();

            T_User user = DataAccess.User.GetUserByEmail(email);
            if (user != null)
            {
                /* Send an email */
                string password = encryptor.DecryptString(user.Password);
                return true;
            }
            return false;
        }

        static public bool ChangeRole(long id, string role)
        {
            if (DataAccess.User.ChangeRole(id, role))
            {
                T_Notification notif = new T_Notification() { Titre = NotificationModel.GetStatus((int)eStatus.Status), Date = DateTime.Now, Message = "Votre statut est désormais " + role, Check = false };
                return DataAccess.Notification.Create(notif, id);
            }
            return false;
        }

        static public UserItem UserToUserItem(string pseudo)
        {
            T_User user = DataAccess.User.GetUserByPseudo(pseudo);
            Models.UserItem itemUser = new UserItem()
            {
                Name = user.Name,
                FirstName = user.Firstname,
                Role = user.Role,
                City = user.Ville,
                Id = user.Id,
                Deleted = user.Deleted,
                Mail = user.Mail
            };

            return itemUser;

        }
    }
}