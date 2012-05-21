using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;

namespace ConcertFinderMVC.BusinessManagement
{
    public class T_Location
    {
        static public bool Create(LOCATION location)
        {
            return DataAccess.T_Location.Create(location);
        }

        public static LOCATION GetLocationByCoord(double latitude, double longitude)
        {
            return DataAccess.T_Location.GetLocationByCoord(latitude, longitude);
        }
    }
}