using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ConcertFinderMVC.DataAccess
{
    public class T_Notification
    {
        public static bool Create(NOTIFICATION notification, USER user, EVENT myevent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    EVENT a_event = bdd.EVENTs.Where(x => x.EVENT_ID == myevent.EVENT_ID).FirstOrDefault();
                    USER a_user = bdd.USERs.Where(x => x.USER_ID == user.USER_ID).FirstOrDefault();

                    bdd.Attach(a_event);
                    bdd.Attach(a_user);

                    notification.EVENTs.Add(a_event);
                    notification.USERs.Add(a_user);

                    bdd.NOTIFICATIONs.AddObject(notification);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                    return false;
                }
            }
            return true;
        }
    }
}