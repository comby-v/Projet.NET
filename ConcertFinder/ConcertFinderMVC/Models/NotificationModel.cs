using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.Models
{
    public enum eStatus
    {
        Creation = 0,
        Edition,
        Status
    }

    public class NotificationModel
    {
        public static string GetStatus(int status)
        {
            var enumType = (eStatus)status;
            switch (enumType)
            {
                case eStatus.Creation:
                    return "Creation";
                case eStatus.Edition:
                    return "Edition";
                case eStatus.Status:
                    return "Statut";
                default:
                    return string.Empty;
            }
        }
    }

    public class NotificationItem
    {
        public long Id { get; set; }
        public String Titre { get; set; }
        public DateTime Date { get; set; }
        public String Message { get; set; }
    }
}