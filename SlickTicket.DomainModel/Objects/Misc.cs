using System.Collections.Generic;
using System.Linq;

namespace SlickTicket.DomainModel.Objects
{
    public class Misc
    {
        public class Statuses
        {
            public static IEnumerable<statuse> List(stDataContext db)
            {
                return from p in db.statuses orderby p.status_order select p;
            }
        }

        public class Priorities
        {
            public static IEnumerable<priority> List(stDataContext db, int userAccessLevel)
            {
                return from p in db.priorities where p.level <= userAccessLevel select p;
            }
        }

        public class AccessLevels
        {
            public static IEnumerable<security_level> List(stDataContext db, int lower_limit)
            {
                return from p in db.security_levels where p.id > lower_limit select p;
            }
        }

        public class Domains
        {
            public static void Add(stDataContext db, string domain)
            {
                allowed_email_domain aed = new allowed_email_domain();
                aed.domain = domain;
                db.allowed_email_domains.InsertOnSubmit(aed);
                db.SubmitChanges();
            }
        }
    }
}
