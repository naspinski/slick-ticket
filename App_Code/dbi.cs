//Slick-Ticket v1.0 - 2008
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Database Interaction Layer (dbi)
/// </summary>
public class dbi
{
    public class faqs
    {
        public static void add(dbDataContext db, string q, string a)
        {
            faq f = new faq();
            f.title = q;
            f.body = a;
            db.faqs.InsertOnSubmit(f);
            db.SubmitChanges();
        }

        public static void delete(dbDataContext db, int id)
        {
            db.faqs.DeleteOnSubmit(get(db, id));
            db.SubmitChanges();
        }

        public static faq get(dbDataContext db, int id)
        {
            return db.faqs.First(f => f.id == id);
        }

        public static IEnumerable<faq> list(dbDataContext db)
        {
            return from p in db.faqs select p;
        }

        public static void edit(dbDataContext db, int id, string q, string a)
        {
            faq f = get(db, id);
            f.title = q;
            f.body = a;
            db.SubmitChanges();
        }
    }

    public class groups
    {
        public static IEnumerable<unit> list(dbDataContext db, int access_level)
        {
            return (from p in db.sub_units
                    where
                        p.access_level <= access_level
                    orderby p.unit.unit_name
                    select p.unit).Distinct();
        }

        public static unit getByID(dbDataContext db, int id)
        {
            return db.units.First(u => u.id == id);
        }


        public static void add(dbDataContext db, string unitName)
        {
            unit newUnit = new unit();
            newUnit.unit_name = unitName;
            db.units.InsertOnSubmit(newUnit);
            db.SubmitChanges();
        }

        public static IEnumerable<ticket> openTickets(dbDataContext db, int unit_ref)
        {
            return from p in db.tickets where p.sub_unit.unit_ref == unit_ref && p.closed == DateTime.Parse("1/1/2001") select p;
        }

        public static IEnumerable<ticket> closedTickets(dbDataContext db, int unit_ref)
        {
            return from p in db.tickets where p.sub_unit.unit_ref == unit_ref && p.closed != DateTime.Parse("1/1/2001") select p;
        }

        public class subGroups
        {
            public static IEnumerable<sub_unit> list(dbDataContext db, int unit_ref, int access_level)
            {
                var query =
                    from p in db.sub_units
                    where
                        p.unit_ref == unit_ref &&
                        p.access_level <= access_level
                    orderby p.sub_unit_name
                    select p;

                return query;
            }

            public static IEnumerable<ticket> openTickets(dbDataContext db, int sub_unit_ref)
            {
                return from p in db.tickets where p.sub_unit.id == sub_unit_ref && p.closed == DateTime.Parse("1/1/2001") select p;
            }

            public static IEnumerable<ticket> closedTickets(dbDataContext db, int sub_unit_ref)
            {
                return from p in db.tickets where p.sub_unit.id == sub_unit_ref && p.closed != DateTime.Parse("1/1/2001") select p;
            }

            public static sub_unit getByID(dbDataContext db, int id)
            {
                return db.sub_units.First(su => su.id == id);
            }

            public static void add(dbDataContext db, string subUnitName, int unit, int access_level, string email)
            {
                sub_unit newSubUnit = new sub_unit();
                newSubUnit.sub_unit_name = subUnitName;
                newSubUnit.unit_ref = unit;
                newSubUnit.access_level = access_level;
                newSubUnit.mailto = email;
                db.sub_units.InsertOnSubmit(newSubUnit);
                db.SubmitChanges();
            }
        }
    }

    public class permissions
    {
        public static void addGroup(dbDataContext db, string ad_group, int access_level)
        {
            user_group ug = new user_group();
            ug.ad_group = ad_group;
            ug.security_level = access_level;
            db.user_groups.InsertOnSubmit(ug);
            db.SubmitChanges();
        }

        public static IEnumerable<user_group> list(dbDataContext db)
        {
            return from p in db.user_groups select p;
        }
    }

    public class themes
    {
        public static void set(dbDataContext db, string text, string borders, string body, string links, string hover, string buttonText, string alt, string header, string bg)
        {
            style _style = db.styles.First(s => s.id == 1);
            _style.text_color = text;
            _style.borders = borders;
            _style.body = body;
            _style.links = links;
            _style.hover = hover;
            _style.button_text = buttonText;
            _style.alt_rows = alt;
            _style.header = header;
            _style.background = bg;
            db.SubmitChanges();
        }

        public static IEnumerable<style> list(dbDataContext db)
        {
            return from p in db.styles select p;
        }

        public static style get(dbDataContext db, int theme)
        {
            return db.styles.First(t => t.id == theme);
        }

        public static void delete(dbDataContext db, int theme)
        {
            db.styles.DeleteOnSubmit(get(db, theme));
            db.SubmitChanges();
        }

        public static style current(dbDataContext db)
        {
            return get(db, 1);
        }

        public static void reset(dbDataContext db)
        {
            style _default = db.styles.First(s => s.id == 2); //index of the default template
            set(db, _default.text_color, _default.borders, _default.body, _default.links, _default.hover, _default.button_text, _default.alt_rows, _default.header, _default.background);
            db.SubmitChanges();
        }

        public static void add(dbDataContext db, string name, string text, string borders, string body, string links, string hover, string button, string headers, string alt, string background)
        {
            style s = new style();
            s.style_name = name;
            s.text_color = text;
            s.borders = borders;
            s.body = body;
            s.links = links;
            s.hover = hover;
            s.button_text = button;
            s.header = headers;
            s.alt_rows = alt;
            s.background = background;
            db.styles.InsertOnSubmit(s);
            db.SubmitChanges();
        }
    }

    public class tickets
    {
        public static IEnumerable<ticket> ICommentedIn(dbDataContext db, int userID)
        {
            return (from p in db.comments where p.submitter == userID && p.ticket.closed == DateTime.Parse("1/1/2001") select p.ticket).Distinct();
        }

        public static IEnumerable<ticket> myTickets(dbDataContext db, int userID)
        {
            return (from p in db.tickets where p.submitter == userID && p.closed == DateTime.Parse("1/1/2001") select p).Union(tickets.ICommentedIn(db, userID)).OrderByDescending(p => p.priority1.level).OrderBy(p => p.submitted);
        }

        public static IEnumerable<ticket> myGroupsTickets(dbDataContext db, user usr)
        {
            IEnumerable<ticket> groupTix = from p in db.tickets where p.submitter != usr.id && (p.assigned_to_group == usr.sub_unit || p.originating_group == usr.sub_unit) select p;
            IEnumerable<ticket> ITix = tickets.ICommentedIn(db, usr.id);
            if (groupTix != null && ITix != null)
                return groupTix.Except(ITix).OrderByDescending(p => p.priority1.level).OrderBy(p => p.submitted);
            else
                return groupTix.OrderByDescending(p => p.priority1.level).OrderBy(p => p.submitted);
        }

        public static ticket add(dbDataContext db, string title, string details, int assign_to_group, int _priority, int submitter, int originating_group)
        {
            ticket newTicket = new ticket();
            newTicket.title = title;
            newTicket.details = details;
            newTicket.closed = DateTime.Parse("1/1/2001");
            newTicket.submitter = submitter;
            newTicket.submitted = DateTime.Now;
            newTicket.last_action = DateTime.Now;
            newTicket.priority = _priority;
            newTicket.assigned_to_group = assign_to_group;
            newTicket.ticket_status = 1; // 1=new
            newTicket.originating_group = originating_group;
            db.tickets.InsertOnSubmit(newTicket);
            db.SubmitChanges();


            return newTicket;
        }

        public static ticket get(dbDataContext db, int ticketID)
        {
            return db.tickets.First(t => t.id == ticketID);
        }

        public static ticket update(dbDataContext db, int ticketID, int status, int _priority, int assigned_to_group)
        {
            ticket t = get(db, ticketID);
            t.ticket_status = status;
            t.priority = _priority;
            t.assigned_to_group_last = t.assigned_to_group;
            t.assigned_to_group = assigned_to_group;
            if (status == 5) t.closed = DateTime.Now;
            t.last_action = DateTime.Now;
            db.SubmitChanges();
            return t;
        }

        public static IEnumerable<ticket> listOpen(dbDataContext db)
        {
            return from p in db.tickets where p.closed == DateTime.Parse("1/1/2001") select p;
        }

        public static IEnumerable<ticket> listClosed(dbDataContext db)
        {
            return from p in db.tickets where p.closed != DateTime.Parse("1/1/2001") select p;
        }

        public static IEnumerable<ticket> search(dbDataContext db, string[] keywords, int usr, DateTime dtFrom, DateTime dtTo, int prty, int stat, bool onlyOpen, int grp, int subgrp)
        {
            return from p in db.tickets
                   where
                       p.title.Contains(keywords[0]) &&
                       p.title.Contains(keywords[1]) &&
                       p.title.Contains(keywords[2]) &&
                       p.title.Contains(keywords[3]) &&
                       p.title.Contains(keywords[4]) &&
                       p.title.Contains(keywords[5]) &&
                       p.title.Contains(keywords[6]) &&
                       p.title.Contains(keywords[7]) &&
                       p.title.Contains(keywords[8]) &&
                       p.title.Contains(keywords[9]) &&

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

        public class attachments
        {
            public static void add(dbDataContext db, string fileName, int ticket_ref, long size, int comment_ref)
            {
                attachment a = new attachment();
                a.attachment_name = fileName;
                a.attachment_size = size.ToString();
                a.submitted = DateTime.Now;
                a.ticket_ref = ticket_ref;
                if (comment_ref > 0) a.comment_ref = comment_ref;

                db.attachments.InsertOnSubmit(a);
            }


            public static void saveMultiple(dbDataContext db, FileUpload[] fuControls, int ticket_ref, int comment_ref)
            {
                bool hasAttachments = false;
                foreach (FileUpload fu in fuControls) if (fu.HasFile) hasAttachments = true;

                if (hasAttachments)
                {
                    string dir = HttpContext.Current.Server.MapPath("~") + utils.settings.get("attachments");
                    string dirPath = dir + ticket_ref.ToString();
                    Directory.CreateDirectory(dirPath);
                    dirPath = dirPath + "\\";
                    foreach (FileUpload fu in fuControls)
                        if (fu.HasFile) dbi.tickets.attachments.save(db, fu, dirPath, ticket_ref, comment_ref);
                }
                db.SubmitChanges();
            }

            public static void save(dbDataContext db, FileUpload fu, string dir, int ticket_ref, int comment_ref)
            {
                int count = 1;
                string fileName = fu.FileName;
                string[] fileNameSplit = fileName.Split(new char[] { '.' });
                string ext = "." + fileNameSplit[fileNameSplit.Count() - 1];
                string prefix = fileName.Substring(0, fileName.Length - ext.Length);
                while (File.Exists(dir + fileName))
                {
                    fileName = prefix + "[" + count.ToString() + "]" + ext;
                    count++;
                }
                fu.SaveAs(dir + fileName);
                FileInfo info = new FileInfo(dir + fileName);

                add(db, fileName, ticket_ref, info.Length, comment_ref);
            }
        }

        public class comments
        {
            public static int add(dbDataContext db, string _comment, int ticket_ref, int userID)
            {
                comment c = new comment();
                c.submitter = userID;
                c.comment1 = _comment;
                c.submitted = DateTime.Now;
                c.ticket_ref = ticket_ref;
                db.comments.InsertOnSubmit(c);
                db.SubmitChanges();
                return c.id;
            }

            public static IEnumerable<comment> list(dbDataContext db, int ticket_ref)
            {
                return from c in db.comments where c.ticket_ref == ticket_ref select c;
            }

            public static List<int> commentingGroups(dbDataContext db, int ticketID)
            {
                return (from p in db.comments where p.ticket_ref == ticketID select p.user.sub_unit).ToList();
            }
        }
    }

    public class users
    {
        public static bool exists(dbDataContext db, string _user)
        {
            bool exists = (from p in db.users where p.userName.Equals(_user) select p).Count() > 0;
            HttpContext.Current.Session["isUser"] = exists.ToString();
            if (!exists)
            {
                string url = HttpContext.Current.Request.Url.ToString();
                if (!url.EndsWith("profile.aspx") && !url.Contains("help.aspx")) HttpContext.Current.Response.Redirect("~/profile.aspx");
            }
            return exists;
        }

        public static void add(dbDataContext db, string name, string email, string phone, int _sub_unit)
        {
            user newUser = new user();
            newUser.userName = name;
            newUser.email = email;
            newUser.phone = phone;
            newUser.sub_unit = _sub_unit;
            db.users.InsertOnSubmit(newUser);
            db.SubmitChanges();
            if (newUser.id == 1) newUser.is_admin = true;
            db.SubmitChanges();
        }

        public static void update(dbDataContext db, string name, string email, string phone, int _sub_unit)
        {
            user thisUser = db.users.First(u => u.userName.Equals(name));
            thisUser.userName = name;
            thisUser.email = email;
            thisUser.phone = phone;
            thisUser.sub_unit = _sub_unit;
            db.SubmitChanges();
        }

        public static user get(dbDataContext db, string name)
        {
            return db.users.First(u => u.userName.Equals(name));
        }

        public static IEnumerable<user> allInUnit(dbDataContext db, int unitID)
        {
            return from p in db.users where p.sub_unit1.unit_ref == unitID select p;
        }

        public static void flipAdmin(dbDataContext db, int userID)
        {
            user u = db.users.First(p => p.id == userID);
            u.is_admin = !u.is_admin;
            db.SubmitChanges();
        }

        public static IEnumerable<sub_unit> availableGroups(dbDataContext db, int userAccessLevel)
        {
            return from p in db.sub_units where p.access_level <= userAccessLevel orderby p.unit.unit_name, p.sub_unit_name select p;
        }
    }
    public static IEnumerable<sub_unit> availableGroups(dbDataContext db, int userAccessLevel)
    {
        return from p in db.sub_units where p.access_level <= userAccessLevel orderby p.unit.unit_name, p.sub_unit_name select p;
    }

    public class statuses
    {
        public static IEnumerable<statuse> list(dbDataContext db)
        {
            return from p in db.statuses orderby p.status_order select p;
        }
    }

    public class priorities
    {
        public static IEnumerable<priority> list(dbDataContext db, int userAccessLevel)
        {
            return from p in db.priorities where p.level <= userAccessLevel select p;
        }
    }

    public class accessLevels
    {
        public static IEnumerable<security_level> list(dbDataContext db, int lower_limit)
        {
            return from p in db.security_levels where p.id > lower_limit select p;
        }
    }

    public class domains
    {
        public static void add(dbDataContext db, string domain)
        {
            allowed_email_domain aed = new allowed_email_domain();
            aed.domain = domain;
            db.allowed_email_domains.InsertOnSubmit(aed);
            db.SubmitChanges();
        }
    }
}
