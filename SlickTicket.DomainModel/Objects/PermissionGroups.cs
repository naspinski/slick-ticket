using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlickTicket.DomainModel.Objects
{
    public class PermissionGroups
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

}
