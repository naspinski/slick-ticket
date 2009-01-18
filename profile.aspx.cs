﻿//Slick-Ticket v1.0 - 2008
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net


using System;
using System.Linq;
using System.Web.UI.WebControls;
using SlickTicketExtensions;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Web.Hosting;

public partial class profile : System.Web.UI.Page
{
    dbDataContext db;
    string userName, prefix, domain;
    int securityLevel;
    bool userIsRegistered;
    int accessLevel;
    static string[] allowUserToEdit = { "telephoneNumber" };
    static string adsPath;
    public static string strPhone = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtPhone.Focus();
        db = new dbDataContext();
        userName = utils.userName();
        userIsRegistered = dbi.users.exists(db, userName);
        List<string> groups = new List<string>();
        try 
        {
            groups = utils.userGroups(); 
            accessLevel = utils.accessLevel().security_level;
        }
        catch { accessLevel = 0; }
        bool emailIsRestricted = utils.settings.emailIsRestricted();

        ddlDomain.Visible = emailIsRestricted;
        txtDomain.Visible = !emailIsRestricted;
        rfvDomain.Enabled = txtDomain.Visible;
        regDomain.Enabled = txtDomain.Visible;

        if (!IsPostBack)
        {
            txtUserName.Text = userName;
            var units = dbi.groups.list(db, accessLevel);
            if (units.Count() < 1)
            {
                ddlUnit.Items.Add(new ListItem("No groups available to you", "0"));
                lblProfileHeader.report(false, "There are no groups with a low enough access level for you to join, please <a href='contact.aspx'>contact your administrator</a><br /><br />", null);
            }
            var _units = dbi.groups.list(db, accessLevel);
            foreach (unit u in _units.OrderBy(p => p.unit_name))
                ddlUnit.Items.Add(new ListItem(u.unit_name, u.id.ToString()));
            utils.populateSubUnits(db, ddlUnit, ddlSubUnit, accessLevel);
            if (!userIsRegistered)
            {
                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    try 
                    {
                        fillPhone();
                    }
                    catch { }
                }
                lblProfileHeader.CssClass = "error top_error";
                lblProfileHeader.Text = "<h2>It is required that you fill in your Profile</h2>If you already have filled in your profile, please <a href='profile.aspx'>reload</a><br /><br />";
                txtEmail.Text = userName;
            }
            else
            {
                user thisUser = dbi.users.get(db, userName);
                ddlUnit.SelectedIndex = ddlUnit.Items.IndexOf(ddlUnit.Items.FindByValue(thisUser.sub_unit1.unit.id.ToString()));
                prefix = thisUser.email.Split(new char[] { '@' })[0];
                domain = thisUser.email.Split(new char[] { '@' })[1];
                txtEmail.Text = prefix;
                utils.populateSubUnits(db, ddlUnit, ddlSubUnit, accessLevel);
                ddlDomain.DataBind();
                strPhone = thisUser.phone;
                ddlSubUnit.SelectedIndex = ddlSubUnit.Items.IndexOf(ddlSubUnit.Items.FindByValue(thisUser.sub_unit.ToString()));
                ddlDomain.set(domain);
                txtDomain.Text = domain;
            }

            try
            {
                bool x = true;
                string cssClass;
                foreach (string s in groups)
                {
                    cssClass = x ? "alt_row" : string.Empty;
                    lblGroups.Text += "<div class='"+cssClass+"'>" + s + "</div>";
                    x = !x;
                }
            }
            catch
            {
                lblGroups.Text = "Not part of any groups";
            }
        }

        if(!string.IsNullOrEmpty(strPhone)) txtPhone.Text = strPhone;
    }

    protected void fillPhone()
    {
        using (HostingEnvironment.Impersonate())
        {
            DirectoryEntry entryRoot = new DirectoryEntry("LDAP://RootDSE"); //this will dynamically get your AD root
            string domain = entryRoot.Properties["defaultNamingContext"][0].ToString();//pulls the relevant information out
            adsPath = "LDAP://" + domain;//this is your complete baseLDAP string
            SearchResult sr = FindCurrentUser(new string[] { "allowedAttributesEffective" });

            int count = sr.Properties["allowedAttributesEffective"].Count;
            if (count > 0)
            {
                int i = 0;
                string[] effectiveAttributes = new string[count];
                foreach (string attrib in sr.Properties["allowedAttributesEffective"])
                    effectiveAttributes[i++] = attrib;
                sr = FindCurrentUser(effectiveAttributes);

                foreach (string key in allowUserToEdit)//comment out this line to show all attributes
                //foreach (string key in effectiveAttributes)//uncomment this line to show all attributes
                {
                    if (sr.Properties.Contains(key))
                        strPhone =  sr.Properties[key][0].ToString();
                }
            }
        }
    }

    private SearchResult FindCurrentUser(string[] attribsToLoad)
    {
        //parse the current user's logon name as search key
        string sFilter = String.Format("(&(objectClass=user)(objectCategory=person)(sAMAccountName={0}))", User.Identity.Name.Split(new char[] { '\\' })[1]);
        DirectoryEntry searchRoot = new DirectoryEntry(adsPath, null, null, AuthenticationTypes.Secure);//sets search root to adsPath

        using (searchRoot)//this just pulls the infomation for the current user
        {
            string user = Environment.UserName;
            DirectorySearcher ds = new DirectorySearcher(searchRoot, sFilter, attribsToLoad, SearchScope.Subtree);
            ds.SizeLimit = 1;
            return ds.FindOne();
        }
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        utils.populateSubUnits(db, ddlUnit, ddlSubUnit, accessLevel);
        ddlUnit.Focus();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text + "@" + (txtDomain.Text.Length > 0 ? txtDomain.Text : ddlDomain.SelectedValue);
        if (!userIsRegistered)
        {
            try
            {
                dbi.users.add(db, userName, email, txtPhone.Text, Int32.Parse(ddlSubUnit.SelectedValue));
                lblProfileHeader.Text = "<div class='success'><h2>Your profile has been created</h2></div>";
            }
            catch (Exception ex) { lblProfileHeader.report(false, "Error saving profile", ex); }
        }
        else
        {
            try
            {
                dbi.users.update(db, userName, email, txtPhone.Text, Int32.Parse(ddlSubUnit.SelectedValue));
                lblProfileHeader.Text = "<div class='success'><h2>Your profile has been saved</h2></div>";
            }
            catch (Exception ex) { lblProfileHeader.report(false, "Error saving profile", ex); }
        }
    }
}
