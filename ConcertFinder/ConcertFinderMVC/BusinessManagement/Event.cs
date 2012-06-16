using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.Models;
using ConcertFinderMVC.DataAccess;
using System.Text.RegularExpressions;
using System.IO;

namespace ConcertFinderMVC.BusinessManagement
{
    public class Event
    {
        static public bool Create(FormEventModels myevent, DataAccess.T_User user)
        {
            DataAccess.T_Event ev = new DataAccess.T_Event ()
            {
                Titre = myevent.Title,
                Type = EventModel.GetEventType(myevent.Type),
                Description = myevent.Description,
                DateDebut = myevent.StartDate,
                DateFin= myevent.EndDate,
                Email = myevent.Email,
                WebSite = myevent.Website,
                Tel = myevent.Phone,
                Valide = false
            };
            SaveImage(myevent, ev);

            List<DataAccess.T_Tag> list_tag = new List<DataAccess.T_Tag>();
            List<String> tags = myevent.Tags.Split(new Char[] { ' ', ',', '.', ';'}).ToList();
            foreach (String tag in tags)
            {
                Regex regx = new Regex("[a-z1-9*]");
                if (tag.Length > 2 && regx.Match(tag).Success)
                {
                    tag.ToLower();
                    DataAccess.T_Tag bdd_tag = BusinessManagement.Tag.Get(tag);
                    if (bdd_tag != null)
                    {
                        list_tag.Add(bdd_tag);
                    }
                    else
                    {
                        BusinessManagement.Tag.Create(tag);
                        bdd_tag = BusinessManagement.Tag.Get(tag);
                        list_tag.Add(bdd_tag);
                    }
                }
            }

            DataAccess.T_Location location = Location.GetLocationByCoord(myevent.Latitude, myevent.Longitude);
            if (location != null)
            {
                return DataAccess.Event.Create(ev, user, location, list_tag);
            }
            else
            {
                if (BusinessManagement.Location.Create(new DataAccess.T_Location()
                                                        {
                                                            Pays = myevent.Country,
                                                            Ville = myevent.City,
                                                            CP = myevent.CodePostal,
                                                            Rue = myevent.Address,
                                                            Name = myevent.RoomName,
                                                            Latitude = myevent.Latitude,
                                                            Longitude = myevent.Longitude
                                                        }))
                {
                    DataAccess.T_Location n_location = Location.GetLocationByCoord(myevent.Latitude, myevent.Longitude);
                    return DataAccess.Event.Create(ev, user, n_location, list_tag);
                }
                else
                {
                    return false;
                }
            }
        }

        private static void SaveImage(FormEventModels myevent, T_Event evnt)
        {
            var destinationFolder = HttpContext.Current.Server.MapPath("~/Download");
            var postedFile = myevent.FileImage;
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(postedFile.FileName);
                var path = Path.Combine(destinationFolder, fileName);
                postedFile.SaveAs(path);
                /*var hashpath = Path.Combine(destinationFolder, Serializer.NameFile(fileName));
                ResizeImage(path, hashpath, 180, 570, false);
                SuppressOriginalImage(path);
                evnt.Image = hashpath;*/
                evnt.Image = path;
            }
        }

        private static void SuppressOriginalImage(String filename)
        {
            try
            {
                File.Delete(filename);
            }
            catch (Exception)
            {
            }
        }

        private static void ResizeImage(string OriginalFile, string NewFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);
            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }
            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }
            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);
            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();
            // Save resized picture
            NewImage.Save(NewFile);
        }

        static public bool Delete(long id)
        {
            return DataAccess.Event.Delete (id);
        }


        static public bool Update(FormEventModels myevent, DataAccess.T_Location location, DataAccess.T_User user, long id)
        {
            DataAccess.T_Event ev = Get(myevent.Id);
            ev.Type = EventModel.GetEventType(myevent.Type);
            ev.Description = myevent.Description;
            ev.DateDebut = myevent.StartDate;
            ev.DateFin = myevent.EndDate;
            ev.Email = myevent.Email;
            ev.WebSite = myevent.Website;
            ev.Tel = myevent.Phone;
            SaveImage(myevent, ev);

            List<DataAccess.T_Tag> list_tag = new List<DataAccess.T_Tag>();
            List<String> tags = myevent.Tags.Split(new Char[] { ' ', ',', '.', ';' }).ToList();

            foreach (String tag in tags)
            {
                Regex regx = new Regex("[a-z1-9*]");
                if (tag.Length > 2 && regx.Match(tag).Success)
                {
                    tag.ToLower();
                    DataAccess.T_Tag bdd_tag = BusinessManagement.Tag.Get(tag);
                    if (bdd_tag != null)
                    {
                        list_tag.Add(bdd_tag);
                    }
                    else
                    {
                        BusinessManagement.Tag.Create(tag);
                        bdd_tag = BusinessManagement.Tag.Get(tag);
                        list_tag.Add(bdd_tag);
                    }
                }
            }

            return DataAccess.Event.Update(ev, location, list_tag);
        }

        static public DataAccess.T_Event Get(long id, bool creation = false)
        {
            return DataAccess.Event.Get(id, creation);
        }

        static public DataAccess.T_Event Get(string title, bool creation = false)
        {
            return DataAccess.Event.Get(title, creation);
        }

        static public List<EventItem> GetListLastAddEvent(int nbr, string type = "")
        {
            List<EventItem> list_eventItem = new List<EventItem>();
            List<DataAccess.T_Event> list_event = DataAccess.Event.GetListLastAddEvent(nbr, type);
            foreach (DataAccess.T_Event myevent in list_event)
            {
                EventItem myeventitem = new EventItem()
                {
                    Id = myevent.Id,
                    Titre = myevent.Titre,
                    Description = myevent.Description,
                    Type = myevent.Type,
                    StartDate = myevent.DateDebut,
                    EndDate = myevent.DateFin.GetValueOrDefault(),
                    Salle = myevent.T_Location.Name,
                    Image = myevent.Image,
                    Email = myevent.Email,
                    Tel = myevent.Tel,
                    Website = myevent.WebSite,
                    CP = myevent.T_Location.CP,
                    Ville = myevent.T_Location.Ville,
                    Rue = myevent.T_Location.Rue
                };
                list_eventItem.Add(myeventitem);
            }
            return list_eventItem;
        }

        public static List<T_Event> GetListTEvent(int nb_event)
        {
            return DataAccess.Event.GetListEvent(nb_event);
        }

        public static List<T_Tag> getTagListFromEvent(long idevent)
        {
            return DataAccess.Event.getTaglistfromEvent(idevent);
        }

        static public List<EventItem> GetListEvent(int nbr, string type = "")
        {
            List<EventItem> list_eventItem = new List<EventItem>();
            List<DataAccess.T_Event> list_event = DataAccess.Event.GetListEvent(nbr, type);
            foreach (DataAccess.T_Event myevent in list_event)
            {
                EventItem myeventitem = new EventItem()
                {
                    Id = myevent.Id,
                    Titre = myevent.Titre,
                    Description = myevent.Description,
                    Type = myevent.Type,
                    StartDate = myevent.DateDebut,
                    EndDate = myevent.DateFin.GetValueOrDefault(),
                    Salle = myevent.T_Location.Name,
                    Email = myevent.Email,
                    Tel = myevent.Tel,
                    Website = myevent.WebSite,
                    CP = myevent.T_Location.CP,
                    Ville = myevent.T_Location.Ville,
                    Rue = myevent.T_Location.Rue
                };
                ServerPathImage(myevent, myeventitem);
                list_eventItem.Add(myeventitem);
            }
            return list_eventItem;
        }

        private static void ServerPathImage(T_Event myevent, EventItem myeventitem)
        {
            if (myevent.Image != null)
            {
                var tab = myevent.Image.Split('\\');
                var path = "Download/" + tab[tab.Length - 1];
                myeventitem.Image = path;
            }
        }

        static public List<EventItem> GetListNonValid ()
        {
            List<EventItem> list = new List<EventItem>();
            List<T_Event> listEvent = DataAccess.Event.GetListNonValid();

            foreach (T_Event myevent in listEvent)
            {
                EventItem myeventitem = new EventItem()
                {
                    Id = myevent.Id,
                    Titre = myevent.Titre,
                    Description = myevent.Description,
                    Type = myevent.Type,
                    StartDate = myevent.DateDebut,
                    EndDate = myevent.DateFin.GetValueOrDefault(),
                    Salle = myevent.T_Location.Name,
                    Image = myevent.Image,
                    Email = myevent.Email,
                    Tel = myevent.Tel,
                    Website = myevent.WebSite,
                    CP = myevent.T_Location.CP,
                    Ville = myevent.T_Location.Ville,
                    Rue = myevent.T_Location.Rue,
                    Valide = myevent.Valide.Value
                };
                list.Add(myeventitem);    
            }
            return list;


        }

        static public List<EventItem> GetListValid()
        {
            List<EventItem> list = new List<EventItem>();
            List<T_Event> listEvent = DataAccess.Event.GetListValid();

            foreach (T_Event myevent in listEvent)
            {
                EventItem myeventitem = new EventItem()
                {
                    Id = myevent.Id,
                    Titre = myevent.Titre,
                    Description = myevent.Description,
                    Type = myevent.Type,
                    StartDate = myevent.DateDebut,
                    EndDate = myevent.DateFin.GetValueOrDefault(),
                    Salle = myevent.T_Location.Name,
                    Image = myevent.Image,
                    Email = myevent.Email,
                    Tel = myevent.Tel,
                    Website = myevent.WebSite,
                    CP = myevent.T_Location.CP,
                    Ville = myevent.T_Location.Ville,
                    Rue = myevent.T_Location.Rue,
                    Valide = myevent.Valide.Value
                };
                list.Add(myeventitem);
            }
            return list;
        }

        static public List<EventItem> MyEvent(string pseudo)
        {
            List<T_Event> list = DataAccess.Event.MyEvent(pseudo);
            List<EventItem> listEventItem = new List<EventItem>();
            foreach (T_Event myevent in list)
            {
                EventItem eventItem = new EventItem()
                {
                    Id = myevent.Id,
                    Titre = myevent.Titre,
                    Description = myevent.Description,
                    Type = myevent.Type,
                    StartDate = myevent.DateDebut,
                    EndDate = myevent.DateFin.GetValueOrDefault(),
                    Salle = myevent.T_Location.Name,
                    Email = myevent.Email,
                    Tel = myevent.Tel,
                    Website = myevent.WebSite,
                    CP = myevent.T_Location.CP,
                    Ville = myevent.T_Location.Ville,
                    Rue = myevent.T_Location.Rue
                };
                ServerPathImage(myevent, eventItem);
                listEventItem.Add(eventItem);
            }
            return listEventItem;
        }

        static public bool ValidEvent(long idEvent)
        {
            return DataAccess.Event.ValidEvent (idEvent);
        }

        public static List<EventItem> GetNextEvents(int last_id, string type)
        {
            List<T_Event> list = DataAccess.Event.GetNextEvents(last_id, type);
            List<EventItem> listEventItem = new List<EventItem>();
            foreach (T_Event myevent in list)
            {
                EventItem eventItem = new EventItem()
                {
                    Id = myevent.Id,
                    Titre = myevent.Titre,
                    Description = BusinessManagement.Tool.Truncate(myevent.Description),
                    Type = myevent.Type,
                    StartDate = myevent.DateDebut,
                    EndDate = myevent.DateFin.GetValueOrDefault(),
                    Salle = myevent.T_Location.Name,
                    Email = myevent.Email,
                    Tel = myevent.Tel,
                    Website = myevent.WebSite,
                    CP = myevent.T_Location.CP.Substring(0, 2),
                    Ville = myevent.T_Location.Ville,
                    Rue = myevent.T_Location.Rue
                };
                ServerPathImage(myevent, eventItem);
                listEventItem.Add(eventItem);
            }
            return listEventItem;
        }


        public static List<EventItem> GetEventForAdmin(T_Event myevent, int nb)
        {
            List<EventItem> listRes = new List<EventItem> ();
            List<T_Event> listEvent = new List<T_Event>();

            listEvent = DataAccess.Event.GetEventForAdmin(myevent, nb);
            foreach (T_Event myev in listEvent)
            {
                EventItem eventItem = new EventItem()
                {
                    Id = myev.Id,
                    Titre = myev.Titre,
                    Description = myev.Description,
                    Type = myev.Type,
                    StartDate = myev.DateDebut,
                    EndDate = myev.DateFin.GetValueOrDefault(),
                    Salle = myev.T_Location.Name,
                    Email = myev.Email,
                    Tel = myev.Tel,
                    Website = myev.WebSite,
                    CP = myev.T_Location.CP,
                    Ville = myev.T_Location.Ville,
                    Rue = myev.T_Location.Rue
                };
                listRes.Add(eventItem);
            }

            return listRes;
        }

        public static List<EventItem> GetListEventByUserTag (T_Event myevent, T_User user, int nb)
        {
            List<EventItem> listRes = new List<EventItem>();
            List<T_Event> listEvent = new List<T_Event>();

            listEvent = DataAccess.Event.GetListEventByUserTag(myevent, user, nb);
            foreach (T_Event myev in listEvent)
            {
                EventItem eventItem = new EventItem()
                {
                    Id = myev.Id,
                    Titre = myev.Titre,
                    Description = myev.Description,
                    Type = myev.Type,
                    StartDate = myev.DateDebut,
                    EndDate = myev.DateFin.GetValueOrDefault(),
                    Salle = myev.T_Location.Name,
                    Email = myev.Email,
                    Tel = myev.Tel,
                    Website = myev.WebSite,
                    CP = myev.T_Location.CP,
                    Ville = myev.T_Location.Ville,
                    Rue = myev.T_Location.Rue
                };
                listRes.Add(eventItem);
            }

            return listRes;
        }
    }
}