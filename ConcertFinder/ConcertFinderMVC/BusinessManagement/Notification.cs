using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;
using ConcertFinderMVC.Models;

namespace ConcertFinderMVC.BusinessManagement
{
    public class Notification
    {
        public static bool Create(DataAccess.T_User user, DataAccess.T_Event myevent)
        {
            DataAccess.T_Notification notif = new DataAccess.T_Notification() { Date = DateTime.Now, Titre = NotificationModel.GetStatus((int)NotificationModel.eStatus.Creation), Message = "Vous venez de créé l'événement " + myevent.Titre };
            return DataAccess.Notification.Create(notif, user, myevent);
        }

        public static List<DataAccess.T_Notification> Get(string pseudo)
        {
            DataAccess.T_User user = User.GetUserByPseudo(pseudo);
            return DataAccess.Notification.Get(user);
        }
    }
}