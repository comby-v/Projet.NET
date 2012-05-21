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
            DataAccess.Tag tag = new DataAccess.Tag() { TAG_CONTENT = tagname };

            return DataAccess.Tag.Create(tag);
        }

        public static DataAccess.Tag Get(string tagName)
        {
            return DataAccess.Tag.Get(tagName);
        }
    }
}