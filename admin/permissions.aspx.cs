//Slick-Ticket v1.0 - 2008
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net


using System;
using System.Web.UI.WebControls;
using SlickTicketExtensions;

public partial class admin_user_groups : System.Web.UI.Page
{
    dbDataContext db;
    protected void Page_Load(object sender, EventArgs e)
    {
        db = new dbDataContext();
        if (!IsPostBack)
        {
            System.Drawing.Color alt_color = System.Drawing.ColorTranslator.FromHtml(dbi.themes.current(db).alt_rows);
            gvADGroups.HeaderStyle.BackColor = alt_color;
            gvADGroups.AlternatingRowStyle.BackColor = alt_color;
            foreach (security_level sl in dbi.accessLevels.list(db, 0))
                ddlSecurityLevel.Items.Add(new ListItem("[" + sl.id.ToString() + "] " + sl.security_level_name, sl.id.ToString()));
        }
    }
    protected void btnNewSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            dbi.permissions.addGroup(db, txtADGroup.Text, Int32.Parse(ddlSecurityLevel.SelectedValue));
            lblReport.report(true, "Group Saved", null);
            txtADGroup.Text = string.Empty;
            ddlSecurityLevel.set("1");
            gvADGroups.DataBind();
        }
        catch (Exception ex)
        {
            lblReport.report(false, "Error Saving Group", ex);
        }
    }
    protected void btnResetADxml_Click(object sender, EventArgs e)
    {
        try
        {
            int numberOfGroups = utils.ADGroupListUpdate();
            lblReport.report(true, "AD groups xml file refreshed - " + numberOfGroups + " groups", null);
            lblReport.CssClass = "success top_error";
            utils.settings.update("ad_groups", numberOfGroups.ToString());
        }
        catch (Exception ex)
        {
            lblReport.report(false, "Error refreshing the AD group xml file; this often has to be done from the web server itself and by an administrator with domain rights", ex);
        }
    }

}
