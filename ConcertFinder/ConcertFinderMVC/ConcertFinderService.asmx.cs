using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ConcertFinderMVC.BusinessManagement;
using System.Xml;
using System.Web.Script.Services;
using ConcertFinderMVC.Models;

namespace ConcertFinderMVC
{
    /// <summary>
    /// Description résumée de ConcertFinderService
    /// </summary>
    [WebService(Namespace = "http://192.168.1.86:80/ConcertFinderMVC/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class ConcertFinderService : System.Web.Services.WebService
    {
        [WebMethod]
        public XmlDocument GetXEvents(int nb_event)
        {
            return Serializer.Serialize(Event.GetListTEvent(nb_event));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<EventItem> GetXEventsJSON()
        {
            return Serializer.SerializeJSON(Event.GetListTEvent(10));
        }
    }
}
