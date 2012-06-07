using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ConcertFinderMVC.BusinessManagement;
using System.Xml;

namespace ConcertFinderMVC
{
    /// <summary>
    /// Description résumée de ConcertFinderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class ConcertFinderService : System.Web.Services.WebService
    {
        [WebMethod]
        public XmlDocument GetXEvents(int nb_event)
        {
            return Serializer.Serialize(Event.GetListTEvent(10));
        }
    }
}
