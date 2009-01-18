﻿//Slick-Ticket v1.0 - 2008
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net


using System;

public partial class admin_controls_security_levels : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dbDataContext db = new dbDataContext();

        string securityLevelExplanations = string.Empty;
        foreach (security_level sl in dbi.accessLevels.list(db, 0))
        {
            

            securityLevelExplanations += "<div><span class='bold'>[" + sl.id.ToString() + "] ";
            securityLevelExplanations += string.IsNullOrEmpty(sl.security_level_name) ? "<span style='font-style:italic;'>undefined</span>" : sl.security_level_name;
            securityLevelExplanations += " : </span>";
            securityLevelExplanations += string.IsNullOrEmpty(sl.security_level_description) ? "<span style='font-style:italic;'>undefined</span>" : sl.security_level_description;
            securityLevelExplanations += "</div><br />";
        }
        lblSecurityLevelExplanations.Text = securityLevelExplanations;
    }
}
