﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;
using ConcertFinderMVC.Models;
using System.Text.RegularExpressions;

namespace ConcertFinderMVC.BusinessManagement
{
    public class T_User
    {
        public static Boolean create(RegisterModel form)
        {
            USER user = new USER()
            {
                USER_NAME = form.Name,
                USER_FIRSTNAME = form.Firstname,
                USER_LOGIN = form.Pseudo,
                USER_MAIL = form.Email,
                USER_VILLE = form.City,
                USER_PASSWORD = form.Password,
                USER_ROLE = "User",
                EVENTs = null,
                NOTIFICATIONs = null,
                TAGs = null                
            };
            DataAccess.T_User.create(user);
            string[] split = form.Tags.Split(new Char[] { ' ', ',', '.', ';'});
            List<TAG> listTag = new List<TAG>();
            foreach (string str in split)
            {
                if (str.Length > 2)
                {
                    Regex r = new Regex("[a-z1-9*]");
                    Match m = r.Match(str);
                    if (m.Success)
                    {
                        str.ToLower();
                        ConcertFinderMVC.DataAccess.TAG tag = new ConcertFinderMVC.DataAccess.TAG ()
                        {
                            TAG_CONTENT = str
                        };
                        if (DataAccess.T_Tag.Get(str) == null)
                        {
                            DataAccess.T_Tag.Create(tag);
                        }
                        using (ConcertFinderEntities concert = new ConcertFinderEntities())
                        {
                            tag = DataAccess.T_Tag.Get(str);
                            concert.Attach(tag);
                           
                            user = GetUserByPseudo(form.Pseudo);
                            concert.Attach(user);
                            user.TAGs.Add(tag);
                            concert.Detach(user);
                            DataAccess.T_User.update(user);
                        }
                            listTag.Add(tag);
                    }
                } 
            }

            return DataAccess.T_User.update(user);
        }

        public static Boolean validate_user(string pseudo, string password)
        {
            return DataAccess.T_User.validate_user(pseudo, password);
        }

        public static USER GetUserByPseudo(String pseudo)
        {
            return DataAccess.T_User.GetUserByPseudo(pseudo);
        }
    }
}