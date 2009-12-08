//Slick-Ticket v2.0 - 2009
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net

using System.Collections.Generic;
using System.Linq;
using System.Web;
using SlickTicket.DomainModel;

/// <summary>
/// Summary description for Permissions
/// </summary>
public static class Permissions
{
    public static void AddGroup(stDataContext db, string ad_group, int access_level)
    {
        user_group ug = new user_group();
        ug.ad_group = HttpUtility.HtmlEncode(ad_group);
        ug.security_level = access_level;
        db.user_groups.InsertOnSubmit(ug);
        db.SubmitChanges();
    }

    public static IEnumerable<user_group> List(stDataContext db)
    {
        return from p in db.user_groups select p;
    }
}
