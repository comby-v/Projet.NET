using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public partial class Tag
    {
        public static bool Create(Tag tag)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    bdd.AddToT_Tag(tag);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static Tag Get(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    Tag tag = bdd.T_Tag.Where(x => x.TAG_ID == id).FirstOrDefault();
                    return tag;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static Tag Get(string tagName)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    Tag tag = bdd.T_Tag.Where(t => t.TAG_CONTENT == tagName).FirstOrDefault();
                    return tag;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static Tag GetTagbycontent(String content)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    Tag tag = bdd.T_Tag.Where(x => x.TAG_CONTENT == content).FirstOrDefault();
                    return tag;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        static public bool Update(Tag tag)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    var n_tag = new Tag { TAG_ID = tag.TAG_ID };
                    bdd.T_Tag.Attach(n_tag);
                    bdd.ApplyCurrentValues("T_Tag", tag);
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
                    bdd.DeleteObject(bdd.T_Tag.Where(x => x.TAG_ID == id).FirstOrDefault());
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

       public static List<User> getUserlistfromTag(long idTag)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    Tag tag = bdd.T_Tag.Where(x => x.TAG_ID == idTag).FirstOrDefault();
                    List<User> users = tag.T_User.ToList();
                    return users;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

       public static List<Event> getEventlistfromTag(long idTag)
       {
           using (ConcertFinderEntities bdd = new ConcertFinderEntities())
           {
               try
               {
                   Tag tag = bdd.T_Tag.Where(x => x.TAG_ID == idTag).FirstOrDefault();
                   List<Event> events = tag.T_Event.ToList();
                   return events;
               }
               catch (Exception)
               {
                   return null;
               }
           }
       }
    }
}