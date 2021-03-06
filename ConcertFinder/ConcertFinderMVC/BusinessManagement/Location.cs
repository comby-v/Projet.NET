﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;

namespace ConcertFinderMVC.BusinessManagement
{
    public class Location
    {
        static public bool Create(DataAccess.T_Location location)
        {
            return DataAccess.Location.Create(location);
        }

        public static DataAccess.T_Location GetLocationByCoord(double latitude, double longitude)
        {
            return DataAccess.Location.GetLocationByCoord(latitude, longitude);
        }

        public static bool Update(DataAccess.T_Location location)
        {
            return DataAccess.Location.Update(location);
        }

        public static T_Location GetLocationById(long id)
        {
            return (DataAccess.Location.GetLocationById(id));
        }

    }
}