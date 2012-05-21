using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.Models
{
    public class NotificationModel
    {
        public enum eStatus
        {
            Creation = 0,
            Edition
        }

        public static string GetStatus(int status)
        {
            var enumType = (eStatus)status;
            switch (enumType)
            {
                case eStatus.Creation:
                    return "Creation";
                case eStatus.Edition:
                    return "Edition";
                default:
                    return string.Empty;
            }
        }
    }
}