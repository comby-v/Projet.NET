using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public class T_Location
    {
        static public bool Create(LOCATION location)
        {

            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    bdd.AddToLOCATIONs (location);
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
                    bdd.DeleteObject(bdd.LOCATIONs.Where(x => x.LOCATION_ID == id).FirstOrDefault());
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }


        static public bool Update(LOCATION location)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    var n_location = new LOCATION { LOCATION_ID = location.LOCATION_ID };
                    bdd.LOCATIONs.Attach(n_location);
                    bdd.ApplyCurrentValues("LOCATION", location);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        static public LOCATION Get(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                LOCATION location;
                try
                {
                    location = bdd.LOCATIONs.Include("EVENT").
                        Where(x => x.LOCATION_ID == id).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
                return location;
            }
        }

        public static LOCATION GetLocationByCoord(double latitude, double longitude)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                LOCATION location;
                try
                {
                    location = bdd.LOCATIONs.
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