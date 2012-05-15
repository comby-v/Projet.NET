using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public class T_Tag
    {
        public static bool Create(TAG tag)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    bdd.AddToTAGs(tag);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static TAG Get(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    TAG tag = bdd.TAGs.Where(x => x.TAG_ID == id).FirstOrDefault();
                    return tag;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        static public bool Update(TAG tag)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    var n_tag = new TAG { TAG_ID = tag.TAG_ID };
                    bdd.TAGs.Attach(n_tag);
                    bdd.ApplyCurrentValues("TAG", tag);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Delete(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    bdd.DeleteObject(bdd.TAGs.Where(x => x.TAG_ID == id).FirstOrDefault());
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}