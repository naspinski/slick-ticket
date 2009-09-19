//Slick-Ticket v2.0 - 2009
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net


using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SlickTicketExtensions;

public partial class new_ticket : System.Web.UI.Page
{
    dbDataContext db;
    string userName;
    int accessLevel;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = Resources.Common.NewTicket;
        db = new dbDataContext();
        userName = utils.userName();

        ddlUnit.Focus();
        try{ accessLevel = utils.accessLevel().security_level1.id; }
        catch { }
        if (!IsPostBack)
        {
            foreach (priority p in dbi.priorities.list(db, 10)) ddlPriority.Items.Add(new ListItem(p.priority_name, p.id.ToString()));
            var units = dbi.groups.list(db, accessLevel);
            if (units.Count() < 1)
            {
                // user has no privelege to post new tickets
                lblReport.report(false, GetLocalResourceObject("DontHaveAccess").ToString() + "<br /><br /><span class='smaller'>" +
                    GetLocalResourceObject("ContactAdmin").ToString() + "</span>", null);
                pnlInput.Style.Add(HtmlTextWriterStyle.Display, "none");
                pnlError.Visible = true;
            }
            else 
            {
                foreach (unit u in units.OrderBy(p => p.unit_name))
                    ddlUnit.Items.Add(new ListItem(u.unit_name, u.id.ToString()));
                utils.populateSubUnits(db, ddlUnit, ddlSubUnit, accessLevel);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        pnlInput.Style.Add(HtmlTextWriterStyle.Display, "none");
        user u = dbi.users.get(db, userName);
        try
        {
            string strComment = "<div class='comment_header smaller'><span style='float:left'><span class='bold'>"+ GetLocalResourceObject("OriginallyAssignedTo").ToString()+": </span>" + ddlUnit.SelectedItem.Text + " - " + ddlSubUnit.SelectedItem.Text + "</span>";
            strComment += "<span style='float:right'><span class='bold'>" + Resources.Common.Priority + ": </span>" + ddlPriority.SelectedItem.Text + "</span>";
            strComment += "<span  class='clear'></span></div>";
            strComment += "<div>" + txtDetails.Text + "</div>";
            ticket newTicket = dbi.tickets.add(db, Server.HtmlEncode(txtTopic.Text), strComment, Int32.Parse(ddlSubUnit.SelectedValue), Int32.Parse(ddlPriority.SelectedValue), u.id, u.sub_unit);

            FileUpload[] fuControls = new FileUpload[] { FileUpload1, FileUpload2, FileUpload3, FileUpload4, FileUpload5 };
            dbi.tickets.attachments.saveMultiple(db, fuControls, newTicket.id, 0);
            string body = GetLocalResourceObject("ANew").ToString() +ddlPriority.SelectedItem.Text+" "+ GetLocalResourceObject("TicketWasSubmittedBy").ToString()+" " + u.userName + " (" + u.sub_unit1.unit.unit_name + " - " + u.sub_unit1.sub_unit_name + ") \n\n";
            body += newTicket.title + " [" + Resources.Common.TicketNumber + " #" + newTicket.id + "]\n\n" + Request.Url.OriginalString.Replace("new_ticket.aspx", string.Empty) + "ticket.aspx?ticketid=" + newTicket.id;
           
            pnlOutput.Visible = true;
            if ((bool.Parse(utils.settings.get("email_notification"))))
            {
                try
                {
                    utils.sendEmail(newTicket.sub_unit.mailto, "New " + ddlPriority.SelectedItem.Text.ToLower() + " " + Resources.Common.Priority.ToLower() + " " + Resources.Common.Ticket.ToLower() + " " + Resources.Common.AssignedTo.ToLower() + " " + newTicket.sub_unit.unit.unit_name + " - " + newTicket.sub_unit.sub_unit_name, body);
                    lblSentTo.Text = "<span class='bold'>" + newTicket.sub_unit.unit.unit_name + " - " + newTicket.sub_unit.sub_unit_name + "</span> " + GetLocalResourceObject("HasBeenNotified").ToString();
                }
                catch (Exception ex)
                {
                    lblSentTo.report(false, "<br />" + Resources.Common.EmailError, ex);
                }
            }
            lblTicketNumber.Text = "<a href='ticket.aspx?ticketID=" + newTicket.id.ToString() + "'>" + newTicket.id.ToString() + "</a>";
        }
        catch (Exception ex)
        {
            lblReport.report(false, Resources.Common.Error, ex);
            pnlError.Visible = true;
        }
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        utils.populateSubUnits(db, ddlUnit, ddlSubUnit, accessLevel);
        ddlUnit.Focus();
    }

}
