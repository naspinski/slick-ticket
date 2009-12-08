using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SlickTicket.DomainModel.Objects;

namespace SlickTicket.DomainModel.Objects
{
    public class Tickets
    {
        public static DateTime NullDate { get { return DateTime.Parse("1/1/2001"); } }

        public class Email
        {
            //no mailbox_email
            public static bool New(string senders_email, string title, string details, IEnumerable<FileStream> attachments, string attachmentFolder)
            { return New(new stDataContext(), senders_email, title, details, attachments, attachmentFolder); }
            public static bool New(stDataContext db, string senders_email, string title, string details, IEnumerable<FileStream> attachments, string attachmentFolder)
            { return New(db, senders_email, null, title, details, attachments, attachmentFolder); }

            public static bool New(string senders_email, string mailbox_email, string title, string details, IEnumerable<FileStream> attachments, string attachmentFolder)
            { return New(new stDataContext(), senders_email, mailbox_email, title, details, attachments, attachmentFolder); }
            public static bool New(stDataContext db, string senders_email, string mailbox_email, string title, string details, IEnumerable<FileStream> attachments, string attachmentFolder)
            {
                try
                {
                    Tickets.New(db, title, details, 1, Mailbox.GetSubUnitId(mailbox_email), Users.GetFromEmail(senders_email), attachments, attachmentFolder);
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Data.Add("senders_email", senders_email);
                    ex.Data.Add("mailbox_email", mailbox_email);
                    ex.Data.Add("attachmentFolder", attachmentFolder);
                    ex.Data.Add("title", title);
                    ex.Data.Add("details", details);
                    ex.Data.Add("attachment count", attachments.Count());
                    Errors.New("Ticket.Email.New", ex);
                    return false;
                }
            }
        }
        public static ticket New(stDataContext db, string title, string details, int priority, int assigned_to, user u, IEnumerable<FileStream> attachments, string attachmentFolder)
        {
            ticket t = new ticket()
            {
                active = true,
                assigned_to_group = assigned_to,
                closed = NullDate,
                details = HtmlFilter.Filter(details),
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
            Attachment.Add(db, attachmentFolder, attachments, t.id, null);
            return t;
        }

        public static ticket Get(int id)
        { return Get(new stDataContext(), id); }
        public static ticket Get(stDataContext db, int id)
        { return db.tickets.FirstOrDefault(x => x.id == id); }

        public static ticket Update(int ticket_id, int status, int priority_id, int assigned_to)
        { return Update(new stDataContext(), ticket_id, status, priority_id, assigned_to); }
        public static ticket Update(stDataContext db, int ticket_id, int status, int priority_id, int assigned_to)
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
            return t;
        }

        public static IEnumerable<ticket> ICommentedIn(stDataContext db, int userID)
        {
            return (from c in db.comments where c.active && c.submitter == userID && c.ticket.statuse.id != 5 select c.ticket).Distinct();
        }

        public static IEnumerable<ticket> MyTickets(stDataContext db, int userID)
        {
            return (from p in db.tickets where p.submitter == userID && p.statuse.id != 5 select p).Union(ICommentedIn(db, userID)).OrderByDescending(p => p.priority1.level).OrderBy(p => p.submitted);
        }

        public static IEnumerable<ticket> MyGroupsTickets(stDataContext db, user usr)
        {
            IEnumerable<ticket> groupTix = from p in db.tickets where p.submitter != usr.id && (p.assigned_to_group == usr.sub_unit || p.originating_group == usr.sub_unit) && p.closed == NullDate select p;
            IEnumerable<ticket> ITix = ICommentedIn(db, usr.id);
            if (groupTix != null && ITix != null)
                return groupTix.Except(ITix).OrderByDescending(p => p.priority1.level).OrderBy(p => p.submitted);
            else
                return groupTix.OrderByDescending(p => p.priority1.level).OrderBy(p => p.submitted);
        }


        public static IEnumerable<ticket> ListOpen(stDataContext db)
        {
            return List(db, true, true);
        }

        public static IEnumerable<ticket> ListClosed(stDataContext db)
        {
            return List(db, true, false);
        }

        public static IEnumerable<ticket> List(stDataContext db, bool? active, bool? open)
        {
            return from p in GetTickets(db, active)
                   where
                   (open == null ? true :
                        ((bool)open ? p.closed == NullDate : p.closed != NullDate))
                   select p;
        }

        public static IEnumerable<ticket> Search(stDataContext db, string[] keywords, int usr, DateTime dtFrom, DateTime dtTo, int prty, int stat, bool onlyOpen, int grp, int subgrp)
        {
            return Search(db, keywords, usr, dtFrom, dtTo, prty, stat, onlyOpen, grp, subgrp, true);
        }

        public static IEnumerable<ticket> Search(stDataContext db, string[] keywords, int usr, DateTime dtFrom, DateTime dtTo, int prty, int stat, bool onlyOpen, int grp, int subgrp, bool? active)
        {
            return from p in GetTickets(db, active)
                   where
                       p.title.ToLower().Contains(keywords[0].ToLower()) &&
                       p.title.ToLower().Contains(keywords[1].ToLower()) &&
                       p.title.ToLower().Contains(keywords[2].ToLower()) &&
                       p.title.ToLower().Contains(keywords[3].ToLower()) &&
                       p.title.ToLower().Contains(keywords[4].ToLower()) &&
                       p.title.ToLower().Contains(keywords[5].ToLower()) &&
                       p.title.ToLower().Contains(keywords[6].ToLower()) &&
                       p.title.ToLower().Contains(keywords[7].ToLower()) &&
                       p.title.ToLower().Contains(keywords[8].ToLower()) &&
                       p.title.ToLower().Contains(keywords[9].ToLower()) &&

                       p.submitter == (usr < 0 ? p.submitter : usr) &&
                       p.submitted >= dtFrom &&
                       p.submitted <= dtTo &&
                       (prty == 0 ? true : p.priority == prty) &&
                       (stat == 0 ? true : p.ticket_status == stat) &&
                       (!onlyOpen ? true : p.ticket_status != 5) &&
                       (grp == 0 ? true : (
                       (p.sub_unit2.unit_ref == grp) ||
                       (p.sub_unit.unit_ref == grp))) &&
                       (subgrp == 0 ? true : (
                       (p.originating_group == subgrp) ||
                       (p.assigned_to_group == subgrp)))
                   select p;
        }

        public static IEnumerable<ticket> GetTickets(stDataContext db, bool? active)
        {
            return from p in db.tickets
                   where
                       (active == null ? true :
                       ((bool)active ? p.active : !p.active))
                   select p;
        }
    }
}
