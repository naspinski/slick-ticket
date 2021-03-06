﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SlickTicket.DomainModel.Objects;

namespace SlickTicket.DomainModel.Objects
{
    public class Comments
    {
        public class Email
        {
            // without a reassignment
            public static bool New(string senders_email, int ticket_id, string details, IEnumerable<FileStream> attachments, string attachmentFolder)
            { return New(new stDataContext(), senders_email, null, ticket_id, details, attachments, attachmentFolder); }
            public static bool New(stDataContext db, string senders_email, int ticket_id, string details, IEnumerable<FileStream> attachments, string attachmentFolder)
            { return New(db, senders_email, null, ticket_id, details, attachments, attachmentFolder); }

            public static bool New(string senders_email, string mailbox_email, int ticket_id, string details, IEnumerable<FileStream> attachments, string attachmentFolder)
            { return New(new stDataContext(), senders_email, mailbox_email, ticket_id, details, attachments, attachmentFolder); }
            public static bool New(stDataContext db, string senders_email, string mailbox_email, int ticket_id, string details, IEnumerable<FileStream> attachments, string attachmentFolder)
            {
                try
                {
                    int assign_to = Mailboxes.GetSubUnitId(db, mailbox_email);
                    user u = Users.GetFromEmail(senders_email);
                    ticket t = Tickets.Get(db, ticket_id);
                    comment c = t.comments.Count > 0 ? t.comments.Last() : new comment() { status_id = t.ticket_status, priority_id = t.priority, assigned_to = t.assigned_to_group };
                    assign_to = assign_to != Units.Default ? assign_to : c.assigned_to;
                    New(db, t, u, details, assign_to, c.priority_id, (c.status_id == 1 ? 3 : c.status_id), attachments, attachmentFolder);
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Data.Add("senders_email", senders_email);
                    ex.Data.Add("mailbox_email", mailbox_email);
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
                    Comments.New(db, t.id, u.id, details, assigned_to, priority, status, attachments, attachmentFolder);
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Data.Add("ticket", t.id);
                    ex.Data.Add("details", details);
                    ex.Data.Add("assigned_to", assigned_to);
                    ex.Data.Add("priority", priority);
                    ex.Data.Add("status", status);
                    ex.Data.Add("attachment count", attachments.Count());
                    ex.Data.Add("attachmentFolder", attachmentFolder);
                    Errors.New("Comment.Email.New", ex);
                    return false;
                }
            }
        }

        public static int New(int ticket_id, int user_id, string details, int assigned_to, int priority, int status, IEnumerable<FileStream> attachments, string attachmentFolder)
        { return New(new stDataContext(), ticket_id, user_id, details, assigned_to, priority, status, attachments, attachmentFolder); }
        public static int New(stDataContext db, int ticket_id, int user_id, string details, int assigned_to, int priority, int status, IEnumerable<FileStream> attachments, string attachmentFolder)
        {
            Tickets.Update(db, ticket_id, status, priority, assigned_to);
            comment c = new comment()
            {
                active = true,
                assigned_to = assigned_to,
                comment1 = HtmlFilter.Filter(details),
                priority_id = priority,
                status_id = status,
                submitted = DateTime.Now,
                submitter = user_id,
                ticket_ref = ticket_id
            };
            db.comments.InsertOnSubmit(c);
            db.SubmitChanges();
            Attachments.Add(db, attachmentFolder, attachments, ticket_id, c.id);
            return c.id;
        }

        public static IEnumerable<comment> List(stDataContext db, int ticket_ref)
        {
            return from c in db.comments where c.ticket_ref == ticket_ref && c.active select c;
        }

        public static List<int> CommentingGroups(stDataContext db, int ticketID)
        {
            return (from c in db.comments where c.ticket_ref == ticketID && c.active select c.user.sub_unit).ToList();
        }
    }
}
