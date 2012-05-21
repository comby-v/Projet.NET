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
        public static bool Create(DataAccess.User user, DataAccess.Event myevent)
        {
            DataAccess.Notification notif = new DataAccess.Notification() { Date = DateTime.Now, Titre = NotificationModel.GetStatus((int)NotificationModel.eStatus.Creation), Message = "Vous venez de créé l'événement " + myevent.EVENT_TITRE };
            return DataAccess.Notification.Create(notif, user, myevent);
        }

        public static List<DataAccess.Notification> Get(string pseudo)
        {
            DataAccess.User user = User.GetUserByPseudo(pseudo);
            return DataAccess.Notification.Get(user);
        }
    }
}