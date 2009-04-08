//Slick-Ticket v1.0 - 2008
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using SlickTicketExtensions;

public partial class _ticket : System.Web.UI.Page
{
    dbDataContext db;
    ticket t;
    int accessLevel;
    string userName;

    protected void Page_Load(object sender, EventArgs e)
    {
        userName = utils.userName();
        db = new dbDataContext();
        txtGoToTicket.Focus();
        if (Request.QueryString["ticketID"] != null)
            populateTicket(Request.QueryString["ticketID"].ToString());
    }

    protected void populateTicket(string ticketID)
    {
        try
        {
            bool userCanEditThisTicket = true;
            user me = dbi.users.get(db, userName);
            t = dbi.tickets.get(db, Int32.Parse(ticketID));
            lblTitle.Text = t.title + " <span class='smaller'>[" + t.id + "]</span>";
            lblDetails.Text = t.details;
            lblSubmitter.Text = " <a href='javascript:void();' class='tooltip limited'>" + t.user.userName + "<span class='border_color'><q class='inner_color base_text'>";
            lblSubmitter.Text += t.user.email;
            lblSubmitter.Text += "<br />" + t.user.phone + "<br />" + t.user.sub_unit1.unit.unit_name + "<br />" + t.user.sub_unit1.sub_unit_name + "</q></span></a>";
            lblSubmitted.Text = t.submitted.ToString();

            ////populate comments
            IEnumerable<comment> comments = dbi.tickets.comments.list(db, t.id);
            StringBuilder sb = new StringBuilder();
            foreach (comment c in comments)
                buildComments(c);

            if (sb.Length > 0)
                lblComments.Text = "<h6>Comments</h6>" + sb.ToString();


            //only do the user is at the right level... if not, read only
            user_group ugAccessLevel = utils.accessLevel();
            int intToPopATG = ugAccessLevel.security_level;
            accessLevel = intToPopATG;
            if (ugAccessLevel.security_level < t.sub_unit.access_level)
            {
                intToPopATG = 10; //if the user has too low a level to change this ticket, thi s is set to 10;
                userCanEditThisTicket = false;
            }

            if (!IsPostBack)
            {
                btnOpen.Visible = (t.ticket_status == 5);
                btnClose.Visible = !(t.ticket_status == 5);
                if (userCanEditThisTicket) // full edit privs
                {
                    foreach (priority p in dbi.priorities.list(db, 10)) ddlPriority.Items.Add(new ListItem(p.priority_name, p.id.ToString()));
                    foreach (statuse s in dbi.statuses.list(db)) ddlStatus.Items.Add(new ListItem(s.status_name, s.id.ToString()));
                    ddlStatus.set(t.ticket_status.ToString());
                    ddlPriority.set(t.priority.ToString());
                    var units = dbi.groups.list(db, accessLevel);
                    foreach (unit u in units.OrderBy(p => p.unit_name))
                        ddlUnit.Items.Add(new ListItem(u.unit_name, u.id.ToString()));
                    ddlUnit.set(t.sub_unit.unit.id.ToString());
                    utils.populateSubUnits(db, ddlUnit, ddlSubUnit, accessLevel);
                    ddlSubUnit.set(t.assigned_to_group.ToString());
                }
                else //user can not edit anything but comments
                {
                    ddlUnit.Items.Add(new ListItem(t.sub_unit.unit.unit_name, t.sub_unit.unit.ToString()));
                    ddlSubUnit.Items.Add(new ListItem(t.sub_unit.sub_unit_name, t.assigned_to_group.ToString()));
                    ddlPriority.Items.Add(new ListItem(t.priority1.priority_name, t.priority.ToString()));
                    ddlStatus.Items.Add(new ListItem(t.statuse.status_name, t.statuse.id.ToString()));
                    ddlSubUnit.Enabled = false;
                    ddlUnit.Enabled = false;
                    ddlPriority.Enabled = false;
                    ddlStatus.Enabled = false;

                    if (me.sub_unit == t.user.sub_unit || dbi.tickets.comments.commentingGroups(db, t.id).Contains(me.sub_unit))
                        lblReport.report(false, "Your access level is too low to do anything but comment, attach to and close this ticket", null);
                    else
                    {
                        pnlComment.Style.Add(HtmlTextWriterStyle.Display, "none");
                        lblReport.report(false, "Your access level is too low to do anything but view this ticket", null);
                    }
                }
            }
            pnlShowTicket.Style.Clear();
            pnlShowTicket.Style.Add(HtmlTextWriterStyle.Display, "block");
            pnlNoQuery.Visible = false;
        }
        catch (Exception ex)
        {
            lblTopReport.report(false, "Error - No Ticket Number " + Request.QueryString["ticketID"].ToString(), ex);
        }
    }

    protected string getExtension(string full)
    {
        string[] split = full.Split(new char[] { '.' });
        return split[split.Length - 1];
    }

    protected void buildComments(comment c)
    {
        lblComments.Controls.Add(new LiteralControl("<h3 class='smaller'><span class='title_header'>"));
        lblComments.Controls.Add(new LiteralControl("<a href='javascript:void();' class='tooltip'>" + c.user.userName + "<span class='border_color'><q class='inner_color base_text'>"));
        lblComments.Controls.Add(new LiteralControl("<div>" + c.user.email + "</div><div class='rollover'>" + c.user.phone + "</div><div class='rollover'>" + c.user.sub_unit1.unit.unit_name + "</div>"));
        lblComments.Controls.Add(new LiteralControl("<div class='rollover'>" + c.user.sub_unit1.sub_unit_name + "</div>"));
        lblComments.Controls.Add(new LiteralControl("</q></span></a>"));
        lblComments.Controls.Add(new LiteralControl("</span>" + c.submitted.ToString() + "</h3>"));
        lblComments.Controls.Add(new LiteralControl("<div class='comment_border border_color'><div class='comment inner_color'>"));
        lblComments.Controls.Add(new LiteralControl(c.comment1));
        foreach (attachment a in c.attachments.AsEnumerable())
        {
            lblComments.Controls.Add(new LiteralControl("<div class='iconize'>"));
            LinkButton lb = new LinkButton()
            {
                CommandArgument = a.attachment_name,
                Text = a.attachment_name + " (" + a.attachment_size + " bytes)",
                CssClass = getExtension(a.attachment_name)
            };
            lb.Click += new EventHandler(btnAttachment_Click);
            lblComments.Controls.Add(lb);
            lblComments.Controls.Add(new LiteralControl("</div>"));
        }
        lblComments.Controls.Add(new LiteralControl("</div></div>"));
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ddlStatus.set("5");
        ddlStatus.Items.Clear();
        ddlStatus.Items.Add(new ListItem("Closed", "5"));
        btnUpdate_Click(null, null);
    }

    protected void btnOpen_Click(object sender, EventArgs e)
    {
        ddlStatus.set("5");
        ddlStatus.Items.Clear();
        ddlStatus.Items.Add(new ListItem("Resolved", "4"));
        btnUpdate_Click(null, null);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string error = "Error";
        try
        {
            FileUpload[] fuControls = new FileUpload[] { FileUpload1, FileUpload2, FileUpload3, FileUpload4, FileUpload5 };
            user me = dbi.users.get(db, userName);
            string strComment = "<div class='comment_header'><span style='float:left'><span class='bold'>Assigned to: </span>" + ddlUnit.SelectedItem.Text + " - " + ddlSubUnit.SelectedItem.Text + "</span>";
            strComment += "<span style='float:right'><span class='bold'>Status: </span>" + ddlStatus.SelectedItem.Text + " <span class='bold'>Priority: </span>" + ddlPriority.SelectedItem.Text + "</span>";
            strComment += "<span  class='clear'></span></div>";
            strComment += "<div>" + txtDetails.Text + "</div>";
            int commentID = dbi.tickets.comments.add(db, strComment, t.id, me.id);
            int t_id = t.id;

            dbDataContext db2 = new dbDataContext(); // have to get new dbdatacontext in order to chage the foreign key since it was already open

            ticket _t = dbi.tickets.update(db2, t_id, Int32.Parse(ddlStatus.SelectedValue), Int32.Parse(ddlPriority.SelectedValue), Int32.Parse(ddlSubUnit.SelectedValue));
            string fromGroup = me.sub_unit1.unit.unit_name + " - " + me.sub_unit1.sub_unit_name;
            string toGroup = _t.sub_unit.unit.unit_name + " - " + _t.sub_unit.sub_unit_name;
            string groupEmail = _t.assigned_to_group == _t.assigned_to_group_last ? "0" : t.sub_unit.mailto;

            try { dbi.tickets.attachments.saveMultiple(db2, fuControls, t_id, commentID); }
            //dbi.tickets.attachments.saveMultiple(db, fuControls, Server.MapPath("~") + "\\" + utils.settings.get("attachments"), newTicket.id, 0);
            catch { error += " - saving attachments"; }
            if ((bool.Parse(utils.settings.get("email_notification"))))
            {
                try { buildAndSendEmail(fromGroup, toGroup, t.user.userName, t.title, t.statuse.status_name, t.priority1.priority_name, _t.id, t.user.email, _t.sub_unit.mailto); }
                catch { error += " - sending email"; }
            }
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex) { lblReport.report(false, error, ex); }
    }

    protected void buildAndSendEmail(string fromGroup, string toGroup, string submitterName, string title, string status, string _priority, int id, string originalEmail, string groupEmail)
    {
        bool sendToGroup = !toGroup.Equals("0");
        string body, subject;
        string rootUrl = Page.Request.ServerVariables["HTTP_HOST"].ToString();
        if (status.Equals("closed", StringComparison.CurrentCultureIgnoreCase))
        {
            sendToGroup = false;
            subject = ddlPriority.SelectedItem.Text + " priority ticket number " + id + " has been closed";
            body = submitterName + " (" + fromGroup + ") has closed ticket number " + id + ": " + title + ".   If this is incorrect, please re-open the ticket.\n\n" + rootUrl + "/ticket.aspx?ticketID=" + id;
        }
        else
        {
            subject = "Ticket number " + id + " has been updated - assigned to " + toGroup;
            body = submitterName + " (" + fromGroup + ") has updated ticket number " + id + ":\n" + title + "\n\n";
            body += "Priority: " + _priority + "\n" + "Status: " + status;
            body += "\n\nView the ticket:\nhttp://" + rootUrl + "/ticket.aspx?ticketID=" + id;
        }
        utils.sendEmail(originalEmail, subject, body);
        if (sendToGroup) utils.sendEmail(groupEmail, subject, body);
    }

    protected void btnGoToTicket_Click(object sender, EventArgs e)
    {
        Response.Redirect("ticket.aspx?ticketID=" + txtGoToTicket.Text);
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        utils.populateSubUnits(db, ddlUnit, ddlSubUnit, accessLevel);
        ddlUnit.Focus();
    }

    protected void btnAttachment_Click(object sender, EventArgs e)
    {
        LinkButton clicked = (LinkButton)sender;
        string root = utils.settings.get("attachments") + Request.QueryString["ticketID"].ToString() + "/";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + clicked.CommandArgument);
        Response.TransmitFile(Server.MapPath("~/" + root + clicked.CommandArgument));
        Response.End();
    }

}
