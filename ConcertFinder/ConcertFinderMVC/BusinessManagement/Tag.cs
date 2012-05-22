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
    }
}