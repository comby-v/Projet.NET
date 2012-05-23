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

    public class ForbidForm
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Raisons")]
        public string resaon { get; set; }
    }
}