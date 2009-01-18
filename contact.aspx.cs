//Slick-Ticket v1.0 - 2008
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net


using System;
using System.Net.Mail;
using SlickTicketExtensions;

public partial class contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            string userName = utils.userName();
            dbDataContext db = new dbDataContext();
            user u = dbi.users.get(db, userName);
            string strBody = u.userName + " (" + u.sub_unit1.unit.unit_name + " - " + u.sub_unit1.sub_unit_name + ") has sent a question/comment:\n\n" + txtSubject.Text + "\n\n" + txtBody.Text;
            MailMessage message = new MailMessage(u.email, utils.settings.get("admin_email"), utils.settings.get("title") + " Contact", strBody);
            SmtpClient smtp = new SmtpClient(utils.settings.get("smtp"));
            smtp.Send(message);
            lblReport.report(true, "Message sent", null);
        }
        catch(Exception ex)
        {
            lblReport.report(false, "Error sending message", ex);
        }
        pnlInput.Visible = false;
        pnlOutput.Visible = true;
    }
}
