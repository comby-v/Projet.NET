using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;

namespace ConcertFinderMVC.BusinessManagement
{
    public class T_Tag
    {
        public static bool Create(string tagname)
        {
            TAG tag = new TAG() { TAG_CONTENT = tagname};

            return DataAccess.T_Tag.Create(tag);
        }

        public static TAG Get(string tagName)
        {
            return DataAccess.T_Tag.Get(tagName);
        }
    }
}