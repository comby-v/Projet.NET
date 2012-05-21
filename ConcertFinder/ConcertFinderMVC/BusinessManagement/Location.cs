using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;

namespace ConcertFinderMVC.BusinessManagement
{
    public class Location
    {
        static public bool Create(DataAccess.Location location)
        {
            return DataAccess.Location.Create(location);
        }

        public static DataAccess.Location GetLocationByCoord(double latitude, double longitude)
        {
            return DataAccess.Location.GetLocationByCoord(latitude, longitude);
        }
    }
}