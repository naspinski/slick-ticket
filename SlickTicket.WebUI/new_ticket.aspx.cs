//Slick-Ticket v2.0 - 2009
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net


using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SlickTicketExtensions;
using SlickTicket.DomainModel;
using SlickTicket.DomainModel.Extensions;
using SlickTicket.DomainModel.Objects;
using System.IO;
using System.Collections.Generic;

public partial class new_ticket : System.Web.UI.Page
{
    stDataContext db = new stDataContext();
    CurrentUser currentUser;
    int accessLevel;
    string n = Environment.NewLine;

    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = CurrentUser.Get();
        this.Title = Resources.Common.NewTicket;

        ddlUnit.Focus();
        try{ accessLevel = currentUser.HighestAccessLevelGroup.security_level1.id; }
        catch { }
        if (!IsPostBack)
        {
            foreach (priority p in Misc.Priorities.List(db, 10)) ddlPriority.Items.Add(new ListItem(p.priority_name, p.id.ToString()));
            var units = Units.List(db, accessLevel);
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
                Utils.PopulateSubUnits(db, ddlUnit, ddlSubUnit, accessLevel);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        pnlInput.Style.Add(HtmlTextWriterStyle.Display, "none");
        user u = currentUser.Details;
        try
        {
            FileUpload[] fuControls = new FileUpload[] { FileUpload1, FileUpload2, FileUpload3, FileUpload4, FileUpload5 };
            IEnumerable<FileStream> attachments = fuControls.GetFileStreams(Settings.AttachmentDirectory);
            ticket newTicket = Tickets.New(db, txtTopic.Text, txtDetails.Text, Int32.Parse(ddlPriority.SelectedValue), Int32.Parse(ddlSubUnit.SelectedValue), u, attachments, Settings.AttachmentDirectory);
            fuControls.GetFileStreamsCleanup(Settings.AttachmentDirectory, attachments);
            
            string body = GetLocalResourceObject("ANew").ToString() + " " + ddlPriority.SelectedItem.Text+" "+ GetLocalResourceObject("TicketWasSubmittedBy").ToString()+" " + u.userName + " (" + u.sub_unit1.unit.unit_name + " - " + u.sub_unit1.sub_unit_name + ")" + n + n;
            body += newTicket.title + " [" + Resources.Common.TicketNumber + " #" + newTicket.id + "]" + n + n + Request.Url.OriginalString.Replace("new_ticket.aspx", string.Empty) + "ticket.aspx?ticketid=" + newTicket.id;
           
            pnlOutput.Visible = true;
            if ((bool.Parse(Utils.Settings.Get("email_notification"))))
            {
                try
                {
                    Utils.SendEmail(newTicket.sub_unit.mailto, "New " + ddlPriority.SelectedItem.Text.ToLower() + " " + Resources.Common.Priority.ToLower() + " " + Resources.Common.Ticket.ToLower() + " " + Resources.Common.AssignedTo.ToLower() + " " + newTicket.sub_unit.unit.unit_name + " - " + newTicket.sub_unit.sub_unit_name, body);
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
        Utils.PopulateSubUnits(db, ddlUnit, ddlSubUnit, accessLevel);
        ddlUnit.Focus();
    }

}
