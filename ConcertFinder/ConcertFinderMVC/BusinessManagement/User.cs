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
            DataAccess.User user = new DataAccess.User()
            {
                Name = form.Name,
                Firstname = form.Firstname,
                Pseudo = form.Pseudo,
                Mail = form.Email,
                Ville = form.City,
                Password = form.Password,
                Role = "User",
                T_Event = null,
                T_Notification = null,
                T_Tag = null                
            };
            string[] split = form.Tags.Split(new Char[] { ' ', ',', '.', ';'});
            List<DataAccess.Tag> listTag = new List<DataAccess.Tag>();
            foreach (string str in split)
            {
                if (str.Length > 2)
                {
                    Regex r = new Regex("[a-z1-9*]");
                    Match m = r.Match(str);
                    if (m.Success)
                    {
                        str.ToLower();
                        DataAccess.Tag tag = new DataAccess.Tag ()
                        {
                            TAG_CONTENT = str
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
            return DataAccess.User.create(user, listTag);
        }

        public static Boolean validate_user(string pseudo, string password)
        {
            return DataAccess.User.validate_user(pseudo, password);
        }

        public static DataAccess.User GetUserByPseudo(String pseudo)
        {
            return DataAccess.User.GetUserByPseudo(pseudo);
        }
    }
}