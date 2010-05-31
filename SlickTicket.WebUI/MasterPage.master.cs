﻿//Slick-Ticket v2.0 - 2009
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan[at]naspinski[dot]net


using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SlickTicket.DomainModel;
using SlickTicket.DomainModel.Objects;

public partial class MasterPage : System.Web.UI.MasterPage
{
    stDataContext db;
    //ICurrentUser currentUser;
    CurrentUser currentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(Utils.Settings.Get("installed"))) Response.Redirect("~/setup/");

        side_bar.CssClass = Utils.Settings.Get("sidebar");
        main_container.CssClass = Utils.Settings.Get("sidebar").Equals("left") ? "right" : "left";

        if (Utils.Settings.Get("title").Length > 0)
            this.Page.Title = Utils.Settings.Get("title") + " :: " + this.Page.Title;
        else
            lblTitle.Visible = false;
        lblTitle.Text = Utils.Settings.Get("title");
        imgTitle.ImageUrl = Utils.Settings.Get("image");
        if (Utils.Settings.Get("image").Length < 1) imgTitle.Visible = false;
        db = new stDataContext();

        currentUser = CurrentUser.Get();
        Session["currentuser"] = currentUser;

        if (!currentUser.IsRegistered)
        {
            string url = Request.Url.ToString();
            if (!url.EndsWith("profile.aspx") && !url.Contains("help.aspx")) Response.Redirect("~/profile.aspx");
        }

        lblUserName.Controls.Add(new LiteralControl(currentUser.UserName));
        try
        {
            lblUserName.Controls.Add(new LiteralControl("<div class='smaller' style='font-weight:normal;'>"));
            int myTickets = Tickets.MyTickets(db, currentUser.Details.id).Count();
            int groupTix = Tickets.MyGroupsTickets(db, currentUser.Details).Count();
            this.myIssues(lblTickets, myTickets, groupTix);
            lblUserName.Controls.Add( new LiteralControl("</div>"));
            lblEmail.Text = "<a href='mailto:" + currentUser.Details.email + "'>" + Utils.TrimForSideBar(currentUser.Details.email, 25) + "</a>";
            lblPhone.Text = currentUser.Details.phone;
            lblUnit.Controls.Add(new HyperLink() { Text = currentUser.Details.sub_unit1.unit.unit_name, NavigateUrl = "~/search.aspx?group=" + currentUser.Details.sub_unit1.unit_ref });
            lblSubUnit.Controls.Add(new HyperLink() { Text = currentUser.Details.sub_unit1.sub_unit_name, NavigateUrl = "~/search.aspx?group=" + currentUser.Details.sub_unit1.unit_ref + "&subgroup=" + currentUser.Details.sub_unit });
            //this.myIssues(lblSubUnit,  Tickets.MyGroupsTickets(db, currentUser.Details).Count());
            string accessName;
            int accessLevel;
            var ugAccessLevel = currentUser.HighestAccessLevelGroup;
            if (ugAccessLevel == null)
            {
                accessLevel = 0;
                accessName = Resources.Common.NoAccess;
            }
            else
            {
                accessLevel = ugAccessLevel.security_level;
                accessName = string.IsNullOrEmpty(ugAccessLevel.security_level1.security_level_name) ? "<i>" + Resources.Common.Undefined + "</i>" : ugAccessLevel.security_level1.security_level_name;
            }
            lblAccess.Text = accessLevel.ToString();
            lblAccessName.Text = accessName;
        }
        catch
        {
            lblPhone.CssClass = Resources.Common.Error;
            lblPhone.Text = GetLocalResourceObject("PleaseUpdate").ToString();
        }
        setMenu();
    }

    protected void myIssues(Label lbl, int number, int number2)
    {
        lbl.Controls.Add(new LiteralControl("<div class='smaller' style='padding-left:10px;text-align:left;'>"));
        lbl.Controls.Add(new HyperLink() { NavigateUrl = "~/my_issues.aspx", Text = Resources.Common.MyIssues + ": [" + number.ToString() + "] [" + number2.ToString() + "]"  } );
        lbl.Controls.Add(new LiteralControl("</div>"));
    }

    protected void setMenu()
    {
        XDocument x = XDocument.Load(Server.MapPath("~/") + "/App_Data/main_menu.xml");
        Panel pnl = new Panel();
        pnl.Controls.Add(new LiteralControl("<div id='nav' class='inner_color'><ul>"));

        string[] url = Request.Url.ToString().Split(new char[] { '/' });
        string page = url[url.Length - 1];
        int aspx = page.IndexOf('.');
        page = aspx > 0 ? page.Substring(0, aspx) : page;
        int count = 1;
        var xes = Utils.Menus.Main();
        foreach (XElement xe in xes)
        {
            string xmlPage = xe.Value.ToLower().Replace("~/", string.Empty).Replace(".aspx", string.Empty);
            string li = "<li class='inner_color'>";
            if (xmlPage.Equals(page.ToLower()))
            {
                li = "<li class='current_tab'>";
                imgCurrentPage.ImageUrl = xe.Attribute("image").Value;
                lblCurrentPage.Text = GetGlobalResourceObject("Common", xe.FirstAttribute.Value).ToString();
            }
            if (Request.Url.ToString().ToLower().Contains("/admin"))
            {
                li = li.Replace("current_tab", "inner_color");
                imgCurrentPage.ImageUrl = "~/images/icons/warning.png";
                lblCurrentPage.Text = "<span class='smaller'>" + Resources.Common.Admin + "</span><br />" + Resources.Common.Dashboard;
            }
            if(xe.Attribute("hidden").Value.Equals("false"))
            {
                string strText = GetGlobalResourceObject("Common", xe.FirstAttribute.Value).ToString();
                pnl.Controls.Add(new LiteralControl(li));
                pnl.Controls.Add(new HyperLink() { Text = strText, NavigateUrl = xe.Value, CssClass = "inner_color" });
                pnl.Controls.Add(new LiteralControl("</li>"));
                lblFooter.Controls.Add(new HyperLink() { Text = strText, NavigateUrl = xe.Value });
                if (count++ < xes.Count() -1) lblFooter.Controls.Add(new LiteralControl(" | "));
            }
        }
        if (currentUser.IsAdmin)
        {
            HyperLink hl = new HyperLink();
            hl.NavigateUrl = "~/admin/";
            hl.Text = Resources.Common.Admin;
            hl.CssClass = "inner_color";
            string liAdm = Request.Url.ToString().Contains("/admin/") ? "<li class='current_tab'>" : "<li class='inner_color'>";
            pnl.Controls.Add(new LiteralControl(liAdm));
            pnl.Controls.Add(hl);
            pnl.Controls.Add(new LiteralControl("<span><span id='stop_ie_from_moving' >"));

            foreach (XElement xe in Utils.Menus.Admin())
            {
                string xmlPage = xe.Value.ToLower().Replace("~/admin/", string.Empty).Replace(".aspx", string.Empty);
                if (xmlPage.Equals(page.ToLower()))
                {
                    imgCurrentPage.ImageUrl = xe.Attribute("image").Value;
                    lblCurrentPage.Text = "<span class='smaller'>" + Resources.Common.Admin + "</span><br />" + GetGlobalResourceObject("Common", xe.FirstAttribute.Value).ToString();
                }
                pnl.Controls.Add(new HyperLink() { Text = GetGlobalResourceObject("Common", xe.FirstAttribute.Value).ToString(), NavigateUrl = xe.Value });
            }

            pnl.Controls.Add(new LiteralControl("</span></span>"));
            pnl.Controls.Add(new LiteralControl("</li>"));
            lblFooter.Controls.Add(new LiteralControl(" | "));
            HyperLink hlF = new HyperLink();
            hlF.Text = Resources.Common.Admin;
            hlF.NavigateUrl = "~/admin/";
            lblFooter.Controls.Add(hlF);
        }
        pnl.Controls.Add(new LiteralControl("</ul></div>"));


        phMenu.Controls.Add(pnl);
    }
    protected void btnQuickJump_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ticket.aspx?ticketID=" + txtQuickJump.Text);
    }
}
