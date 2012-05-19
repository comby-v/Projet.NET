using System;
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
                USER_ROLE = "User"
                
            };
 
            string[] split = form.Tags.Split(new Char[] { ' ', ',', '.', ';'});
            System.Data.Objects.DataClasses.EntityCollection<ConcertFinderMVC.DataAccess.TAG> listTag = new System.Data.Objects.DataClasses.EntityCollection<ConcertFinderMVC.DataAccess.TAG> ();
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
                       DataAccess.T_Tag.Create (tag);
                       listTag.Add(tag);
                    }
                } 
            }

            user.TAGs = listTag;
            return DataAccess.T_User.create(user);
        }

        public static Boolean validate_user(string pseudo, string password)
        {
            return DataAccess.T_User.validate_user(pseudo, password);
        }
    }
}