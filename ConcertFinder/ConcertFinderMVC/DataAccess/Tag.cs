using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public partial class Tag
    {
        public static bool Create(T_Tag tag)
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

        public static T_Tag Get(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_Tag tag = bdd.T_Tag.Where(x => x.Id == id).FirstOrDefault();
                    return tag;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static T_Tag Get(string tagName)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_Tag tag = bdd.T_Tag.Where(t => t.Name == tagName).FirstOrDefault();
                    return tag;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static T_Tag GetTagbycontent(String content)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_Tag tag = bdd.T_Tag.Where(x => x.Name == content).FirstOrDefault();
                    return tag;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        static public bool Update(T_Tag tag)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    var n_tag = new T_Tag { Id = tag.Id };
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
                    bdd.DeleteObject(bdd.T_Tag.Where(x => x.Id == id).FirstOrDefault());
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

       public static List<T_User> getUserlistfromTag(long idTag)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_Tag tag = bdd.T_Tag.Where(x => x.Id == idTag).FirstOrDefault();
                    List<T_User> users = tag.T_User.ToList();
                    return users;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

       public static List<T_Event> getEventlistfromTag(long idTag)
       {
           using (ConcertFinderEntities bdd = new ConcertFinderEntities())
           {
               try
               {
                   T_Tag tag = bdd.T_Tag.Where(x => x.Id == idTag).FirstOrDefault();
                   List<T_Event> events = tag.T_Event.ToList();
                   return events;
               }
               catch (Exception)
               {
                   return null;
               }
           }
       }


       // Contains (Titre(mot), Salle) Start (Ville, Tag)
       public static List<T_Event> Search(String q)
       {
           using (ConcertFinderEntities bdd = new ConcertFinderEntities())
           {
               try
               {
                   List<T_Event> events = bdd.T_Event.Include("T_Tag").Include("T_Location")
                       .Where(x => x.Valide == true && x.DateDebut > DateTime.Now).ToList()
                       .Where(x => x.Titre.Split(new char[] { ' ', '+' }).Intersect(q.Split(new char[] { ' ', '+' })).Count() > 0 ||
                           x.T_Location.Name.Split(' ').Select(z => z.ToLowerInvariant()).Intersect(q.Split(' ').Select(z => z.ToLowerInvariant())).Count() > 0 ||
                           x.T_Location.Ville.Split(new char[] { ' ', '-' }).Intersect(q.Split(new char[] { ' ', '-' })).Count() > 0 ||
                           x.T_Tag.FirstOrDefault(y => y.Name.Equals(q, StringComparison.InvariantCultureIgnoreCase)) != null)
                           .OrderBy(x => x.DateDebut).ToList();
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