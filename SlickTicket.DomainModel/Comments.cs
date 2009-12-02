using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace SlickTicket.DomainModel
{
    public class Comment
    {
        public class Email
        {
            public static bool New(string email, int ticket_id, string details, IEnumerable<FileStream> attachments, string attachmentFolder)
            { return New(new stDataContext(), email, ticket_id, details, attachments, attachmentFolder); }
            public static bool New(stDataContext db, string email, int ticket_id, string details, IEnumerable<FileStream> attachments, string attachmentFolder)
            {
                try
                {
                    user u = User.GetFromEmail(email);
                    ticket t = Ticket.Get(db, ticket_id);
                    comment c = t.comments.Count > 0 ? t.comments.Last() : new comment() { status_id = t.ticket_status, priority_id = t.priority, assigned_to = t.assigned_to_group };
                    New(db, t, u, details, c.assigned_to, c.priority_id, (c.status_id == 1 ? 3 : c.status_id), attachments, attachmentFolder);
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Data.Add("email", email);
                    ex.Data.Add("ticket_id", ticket_id);
                    ex.Data.Add("details", details);
                    ex.Data.Add("attachment count", attachments.Count());
                    ex.Data.Add("attachmentFolder", attachmentFolder);
                    Errors.New("Comment.Email.New", ex);
                    return false;
                }
            }
            public static bool New(stDataContext db, ticket t, user u, string details, int assigned_to, int priority, int status, IEnumerable<FileStream> attachments, string attachmentFolder)
            {
                try
                {
                    Comment.New(db, t.id, u.id, details, assigned_to, priority, status, attachments, attachmentFolder);
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Data.Add("ticket",t.id);
                    ex.Data.Add("details", details);
                    ex.Data.Add("assigned_to", assigned_to);
                    ex.Data.Add("priority", priority);
                    ex.Data.Add("status",status);
                    ex.Data.Add("attachment count",attachments.Count());
                    ex.Data.Add("attachmentFolder",attachmentFolder);
                    Errors.New("Comment.Email.New", ex);
                    return false;
                }
            }
        }

        public static void New(int ticket_id, int user_id, string details, int assigned_to, int priority, int status, IEnumerable<FileStream> attachments, string attachmentFolder)
        { New(new stDataContext(), ticket_id, user_id, details, assigned_to, priority, status, attachments, attachmentFolder); }
        public static void New(stDataContext db, int ticket_id, int user_id, string details, int assigned_to, int priority, int status, IEnumerable<FileStream> attachments, string attachmentFolder)
        {
            Ticket.Update(db, ticket_id, status, priority, assigned_to);
            comment c = new comment()
            {
                active = true,
                assigned_to = assigned_to,
                comment1 = HttpUtility.HtmlEncode(details),
                priority_id = priority,
                status_id = status,
                submitted = DateTime.Now,
                submitter = user_id,
                ticket_ref = ticket_id
            };
            db.comments.InsertOnSubmit(c);
            db.SubmitChanges();
            Attachment.Add(db, attachmentFolder, attachments, ticket_id, c.id);
        }
    }
}
