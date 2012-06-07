﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using ConcertFinderMVC.DataAccess;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Drawing;

namespace ConcertFinderMVC.BusinessManagement
{
    public class Serializer
    {
        public static XmlDocument Serialize(List<T_Event> events)
        {
            XmlDocument xml = new XmlDocument();
            using (XmlWriter writer = xml.CreateNavigator().AppendChild())
            {
                writer.WriteStartDocument();
                writer.WriteDocType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
                writer.WriteStartElement("plist");
                writer.WriteAttributeString("version", "1.0");
                writer.WriteStartElement("array");

                foreach (T_Event evnt in events)
                {
                    writer.WriteStartElement("dict");

                    WriteField(writer, "Id", evnt.Id.ToString());
                    WriteField(writer, "Titre", evnt.Titre);
                    WriteField(writer, "Description", evnt.Description);
                    WriteField(writer, "DateDebut", evnt.DateDebut.ToString());
                    WriteField(writer, "Type", evnt.Type);
                    if (evnt.Image != null)
                    {
                        WriteField(writer, "Image", ImageToString(evnt.Image));
                    }
                    if (evnt.Tel != null)
                    {
                        WriteField(writer, "Tel", evnt.Tel);
                    }
                    if (evnt.WebSite != null)
                    {
                        WriteField(writer, "WebSite", evnt.WebSite);
                    }
                    if (evnt.Email != null)
                    {
                        WriteField(writer, "Email", evnt.Email);
                    }
                    WriteField(writer, "Latitude", evnt.T_Location.Latitude.ToString());
                    WriteField(writer, "Longitude", evnt.T_Location.Longitude.ToString());
                    WriteField(writer, "Name", evnt.T_Location.Name);
                    WriteField(writer, "Rue", evnt.T_Location.Rue);
                    WriteField(writer, "Ville", evnt.T_Location.Ville);
                    WriteField(writer, "CP", evnt.T_Location.CP);
                    WriteField(writer, "Pays", evnt.T_Location.Pays);

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            return xml;
        }

        private static void WriteField(XmlWriter writer, String key, String value)
        {
            writer.WriteElementString("key", key);
            writer.WriteElementString("string", value);
        }

        private static string ImageToString(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            Image im = Image.FromFile(path);

            MemoryStream ms = new MemoryStream();

            im.Save(ms, im.RawFormat);

            byte[] array = ms.ToArray();

            return Convert.ToBase64String(array);

        }

        private static Image StringToImage(string imageString)
        {

            if (imageString == null)
            {
                throw new ArgumentNullException("imageString");
            }

            byte[] array = Convert.FromBase64String(imageString);

            Image image = Image.FromStream(new MemoryStream(array));

            return image;

        }

        public static String NameFile(String filename)
        {
            var extension = filename.Split('.')[1];
            MD5 md5HashAlgo = MD5.Create();
            // Place le texte à hacher dans un tableau d'octets 
            byte[] byteArrayToHash = Encoding.UTF8.GetBytes(DateTime.Now.ToString() + filename.Split('.')[0]);
            // Hash le texte et place le résulat dans un tableau d'octets 
            byte[] hashResult = md5HashAlgo.ComputeHash(byteArrayToHash);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hashResult.Length; i++)
            {
                // Affiche le Hash en hexadecimal 
                result.Append(hashResult[i].ToString("X2"));
            }

            return result.ToString() + "." + extension;
        }
    }
}