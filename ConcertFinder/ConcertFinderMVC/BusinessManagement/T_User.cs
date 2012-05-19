using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;
using ConcertFinderMVC.Models;

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
            return DataAccess.T_User.create(user);
        }
    }
}