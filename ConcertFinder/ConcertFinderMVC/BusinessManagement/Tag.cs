using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;

namespace ConcertFinderMVC.BusinessManagement
{
    public class Tag
    {
        public static bool Create(string tagname)
        {
            DataAccess.T_Tag tag = new DataAccess.T_Tag() { Name = tagname };

            return DataAccess.Tag.Create(tag);
        }

        public static DataAccess.T_Tag Get(string tagName)
        {
            return DataAccess.Tag.Get(tagName);
        }

        public static List<String> Keyword(string q)
        {
            List<T_Tag> tags = DataAccess.Tag.Keyword(q);
            List<String> keywords = new List<String>();
            foreach (T_Tag tag in tags)
            {
                keywords.Add(tag.Name);
            }
            return keywords;
        }
    }
}