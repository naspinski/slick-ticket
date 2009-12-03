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
            public static bool New(string senders_email, string title, string details, IEnumerable<FileStream> attachments, string attachmentDirectory)
            { return New(new stDataContext(), senders_email, title, details, attachments, attachmentDirectory); }
            public static bool New(stDataContext db, string senders_email, string title, string details, IEnumerable<FileStream> attachments, string attachmentDirectory)
            {
                try
                {
                    user u = User.GetFromEmail(senders_email);
                    Ticket.New(db, title, details, 1, Unit.Default, u, attachments, attachmentDirectory);
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Data.Add("email", senders_email);
                    ex.Data.Add("attachmentDirectory", attachmentDirectory);
                    ex.Data.Add("title", title);
                    ex.Data.Add("details", details);
                    ex.Data.Add("attachment count", attachments.Count());
                    Errors.New("Ticket.Email.New", ex);
                    return false;
                }
            }
        }
        public static void New(stDataContext db, string title, string details, int priority, int assigned_to, user u, IEnumerable<FileStream> attachments, string attachmentDirectory)
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
            Attachment.Add(db, attachmentDirectory, attachments, t.id, null);
        }

        public static ticket Get(int id)
        { return Get(new stDataContext(), id); }
        public static ticket Get(stDataContext db, int id)
        { return db.tickets.FirstOrDefault(x => x.id == id); }

        public static void Update(int ticket_id, int status, int priority_id, int assigned_to)
        { Update(new stDataContext(), ticket_id, status, priority_id, assigned_to); }
        public static void Update(stDataContext db, int ticket_id, int status, int priority_id, int assigned_to)
        {
            ticket t = Get(db, ticket_id);
            t.ticket_status = status;
            t.priority = priority_id;
            t.assigned_to_group_last = t.assigned_to_group;
            t.assigned_to_group = assigned_to;
            if (status == 5) t.closed = DateTime.Now;
            else t.closed = NullDate;
            t.last_action = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
