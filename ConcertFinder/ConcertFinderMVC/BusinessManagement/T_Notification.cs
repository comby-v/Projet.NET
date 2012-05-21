using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;
using ConcertFinderMVC.Models;

namespace ConcertFinderMVC.BusinessManagement
{
    public class T_Notification
    {
        public static bool Create(USER user, EVENT myevent)
        {
            NOTIFICATION notif = new NOTIFICATION() { NOTIF_DATE = DateTime.Now, NOTIF_TITRE = NotificationModel.GetStatus((int)NotificationModel.eStatus.Creation), NOTIF_MESSAGE = "Vous venez de créé l'événement " + myevent.EVENT_TITRE };
            return DataAccess.T_Notification.Create(notif, user, myevent);
        }
    }
}