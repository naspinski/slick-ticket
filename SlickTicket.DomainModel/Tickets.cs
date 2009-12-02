using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace SlickTicket.DomainModel
{
    public class Ticket
    {
        public static DateTime NullDate { get { return DateTime.Parse("1/1/2001"); } }

        public class Email
        {
            public static bool New(dbDataContext db, string email, string saveDestination, string title, string details, IEnumerable<FileStream> attachments)
            {
                try
                {
                    user u = Users.GetFromEmail(email);
                    Ticket.New(db, saveDestination, title, details, 1, attachments, Units.Default, u);
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Data.Add("email", email);
                    ex.Data.Add("saveDestination", saveDestination);
                    ex.Data.Add("title", title);
                    ex.Data.Add("details", details);
                    Errors.New("Tickets.Email.New", ex);
                    return false;
                }
            }
        }
        public static void New(dbDataContext db, string saveDestination, string title, string details, int priority, IEnumerable<FileStream> attachments, int assigned_to, user u)
        {
            ticket t = new ticket()
            {
                active = true,
                assigned_to_group = assigned_to,
                closed = NullDate,
                details = HttpUtility.HtmlEncode(details),
                last_action = DateTime.Now,
                originating_group = u.sub_unit,
                priority = priority,
                submitted = DateTime.Now,
                ticket_status = 1,
                submitter = u.id,
                title = HttpUtility.HtmlEncode(title)
            };
            db.tickets.InsertOnSubmit(t);
            db.SubmitChanges();
            Attachments.Add(db, saveDestination, attachments, t.id, null);
        }
    }
}
