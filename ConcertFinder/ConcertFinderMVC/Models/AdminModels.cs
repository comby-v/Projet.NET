using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using ConcertFinderMVC.Models;


namespace ConcertFinderMVC.Models
{
    public class AdminModels
    {
        public List<UserItem> listUser { get; set; }
        public List<String> listPost { get; set; }
        public List<EventItem> listEvent { get; set; }
    }

    public class UserItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Login { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public bool Deleted { get; set; }
    }
}