﻿using System;
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
            DataAccess.T_Notification notif = new DataAccess.T_Notification() { Date = DateTime.Now, Titre = NotificationModel.GetStatus((int)eStatus.Creation), Message = "Vous venez de créer l'événement " + myevent.Titre, Check = false };
            return DataAccess.Notification.Create(notif, user, myevent);
        }

        public static bool Update(DataAccess.T_User user, DataAccess.T_Event myevent)
        {
            DataAccess.T_Notification notif = new DataAccess.T_Notification() { Date = DateTime.Now, Titre = NotificationModel.GetStatus((int)eStatus.Edition), Message = "Vous venez d'éditer l'événement " + myevent.Titre, Check = false };
            return DataAccess.Notification.Create(notif, user, myevent);
        }

        public static List<DataAccess.T_Notification> Get(string pseudo)
        {
            return DataAccess.Notification.Get(pseudo);
        }

        public static List<DataAccess.T_Notification> GetNext(string pseudo, int last_id)
        {
            return DataAccess.Notification.GetNext(pseudo, last_id);
        }

        public static bool Deny(ForbidForm form)
        {
            T_Notification notif = new T_Notification()
            {
                Date = DateTime.Now,
                Titre = form.Title,
                Message = form.reason,
                Check = false
            };
            return (DataAccess.Notification.Deny(notif, form.IdEvent));
        }

        public static int Checks(String user)
        {
            return (DataAccess.Notification.Checks(user));
        }
    }
}