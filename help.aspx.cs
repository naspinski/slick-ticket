//Slick-Ticket v1.0 - 2008
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net


using System;
using System.Web;
using System.Web.UI.WebControls;
using SlickTicketExtensions;

public partial class info : System.Web.UI.Page
{
    dbDataContext db;
    public bool isAdmin;
    protected void Page_Load(object sender, EventArgs e)
    {
        db = new dbDataContext();

        string userName = utils.userName();
        try
        {
            user thisUser = dbi.users.get(db, userName);
            if (thisUser.is_admin) isAdmin = true;
        }
        catch
        { isAdmin = false; }
    }

    public string trimJunk(string fromThis)
    {
        string[] junk = new string[] { " ", "?", "/", "#", "^", "&", "*", ".", ",", "\\", "'", "`" };
        foreach (string s in junk) fromThis = fromThis.Replace(s, string.Empty);
        return fromThis;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        try
        {
            dbi.faqs.delete(db, Int32.Parse(btn.CommandArgument));
            lblReport.report(true, "Question/Answer Deleted", null);
            rptIndex.DataBind();
            rpt.DataBind();
        }
        catch (Exception ex)
        { lblReport.report(false, "Error", ex); }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int cmdArg = Int32.Parse(btnSubmit.CommandArgument);
        try
        {
            if( cmdArg != 0 )
            {
                dbi.faqs.edit(db, cmdArg, Server.HtmlEncode(txtQ.Text), txtA.Text);
                lblReport.report(true, "Question/Answer updated", null);
            }
            else
            {
                dbi.faqs.add(db, Server.HtmlEncode(txtQ.Text), txtA.Text);
                lblReport.report(true, "Question/Answer inserted", null);
            }
            rptIndex.DataBind();
            rpt.DataBind();
            txtA.Text = string.Empty;
            txtQ.Text = string.Empty;
        }
        catch (Exception ex)
        { lblReport.report(false, "Error", ex); }
        btnSubmit.CommandArgument="0";
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        faq f = dbi.faqs.get(db, Int32.Parse(btn.CommandArgument));
        txtQ.Text = f.title;
        txtA.Text = f.body;
        btnSubmit.CommandArgument = btn.CommandArgument.ToString();
        mpe.Show();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSubmit.CommandArgument = "0";
    }
}
