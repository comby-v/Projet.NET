using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public partial class Location
    {
        static public bool Create(Location location)
        {

            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    bdd.AddToT_Location (location);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        static public bool Delete(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    bdd.DeleteObject(bdd.T_Location.Where(x => x.LOCATION_ID == id).FirstOrDefault());
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }


        static public bool Update(Location location)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    var n_location = new Location { LOCATION_ID = location.LOCATION_ID };
                    bdd.T_Location.Attach(n_location);
                    bdd.ApplyCurrentValues("T_Location", location);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        static public Location Get(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                Location location;
                try
                {
                    location = bdd.T_Location.Include("T_Event").
                        Where(x => x.LOCATION_ID == id).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
                return location;
            }
        }

        public static Location GetLocationByCoord(double latitude, double longitude)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                Location location;
                try
                {
                    location = bdd.T_Location.
                        Where(x => x.LOCATION_LATTITUDE == latitude && x.LOCATION_LONGITUDE == longitude).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
                return location;
            }
        }
    }
}