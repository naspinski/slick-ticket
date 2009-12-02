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
            public static bool New(string email, string title, string details, IEnumerable<FileStream> attachments, string saveDestination)
            { return New(new stDataContext(), email, title, details, attachments, saveDestination); }
            public static bool New(stDataContext db, string email, string title, string details, IEnumerable<FileStream> attachments, string saveDestination)
            {
                try
                {
                    user u = User.GetFromEmail(email);
                    Ticket.New(db, title, details, 1, Unit.Default, u, attachments, saveDestination);
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Data.Add("email", email);
                    ex.Data.Add("saveDestination", saveDestination);
                    ex.Data.Add("title", title);
                    ex.Data.Add("details", details);
                    ex.Data.Add("attachment count", attachments.Count());
                    Errors.New("Ticket.Email.New", ex);
                    return false;
                }
            }
        }
        public static void New(stDataContext db, string title, string details, int priority, int assigned_to, user u, IEnumerable<FileStream> attachments, string saveDestination)
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
        public static ticket Get(int id)
        { return Get(new stDataContext(), id); }
        public static ticket Get(stDataContext db, int id)
        { return db.tickets.FirstOrDefault(x => x.id == id); }
    }
}
