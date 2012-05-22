using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public partial class Location
    {
        static public bool Create(T_Location location)
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
                    bdd.DeleteObject(bdd.T_Location.Where(x => x.Id == id).FirstOrDefault());
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }


        static public bool Update(T_Location location)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    var n_location = new T_Location { Id = location.Id };
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

        static public T_Location Get(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                T_Location location;
                try
                {
                    location = bdd.T_Location.Include("T_Event").
                        Where(x => x.Id == id).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
                return location;
            }
        }

        public static T_Location GetLocationByCoord(double latitude, double longitude)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                T_Location location;
                try
                {
                    location = bdd.T_Location.
                        Where(x => x.Latitude == latitude && x.Longitude == longitude).FirstOrDefault();
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