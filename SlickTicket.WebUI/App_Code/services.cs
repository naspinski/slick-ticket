﻿//Slick-Ticket v2.0 - 2009
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan[at]naspinski[dot]net


using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using SlickTicket.DomainModel;

/// <summary>
/// Summary description for services
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class services : System.Web.Services.WebService {

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] getGroups(string prefixText, int count)
    {
        string file_location = HttpContext.Current.Server.MapPath("~") + "\\App_Data\\ADGroups.xml";
        XElement x = XElement.Load(file_location);
        return (from p in x.Descendants("group") where p.Value.ToLower().Contains(prefixText.ToLower()) select p.Value).ToArray();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] getUsers(string prefixText, int count)
    {
        stDataContext db = new stDataContext();
        return (from p in db.users where p.userName.ToLower().Contains(prefixText.ToLower()) select p.userName).ToArray();
    }
    
}

