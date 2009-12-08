﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SlickTicket.DomainModel;
using System.Web.Configuration;

/// <summary>
/// Summary description for AccessLevel
/// </summary>
public class CurrentUser// : ICurrentUser
{
    public SlickTicket.DomainModel.user Details { get; set; }
    public string UserName { get; set; }
    public SlickTicket.DomainModel.user_group HighestAccessLevelGroup { get; set; }
    public bool IsRegistered { get; set; }
    public bool IsAdmin { get; set; }

    private CurrentUser()
    {
        stDataContext db = new stDataContext();
        UserName = GetUsername();
        Details = db.users.FirstOrDefault(u => u.userName.Equals(UserName));
        HighestAccessLevelGroup = GetHighestAccessLevelGroup(db);
        IsRegistered = Details != null;
        IsAdmin = (Details != null && Details.is_admin);
    }

    public static CurrentUser Get()
    {
        CurrentUser currentUser;
        try
        {
            currentUser = (CurrentUser)HttpContext.Current.Session["currentUser"];
            if (currentUser.Details == null)
                currentUser = new CurrentUser();
        }
        catch
        {
            currentUser = new CurrentUser();
            HttpContext.Current.Session["currentUser"] = currentUser;
        }

        return currentUser;
    }

    public static void Update()
    {
        HttpContext.Current.Session["currentUser"] = new CurrentUser();
    }

    private string GetUsername()
    {
        try { return (HttpContext.Current.User.Identity.Name.Split(new char[] { '\\' }))[1]; }
        catch { return HttpContext.Current.User.Identity.Name; } // if the user is not on a domain
    }

    private SlickTicket.DomainModel.user_group GetHighestAccessLevelGroup(stDataContext db)
    {
        return AccessLevelGroup(db);
    }

    private SlickTicket.DomainModel.user_group AccessLevelGroup(stDataContext db)
    {
        if (Settings.AuthenticationMode == AuthenticationMode.Windows)
        {
            try
            {
                try
                { return (from p in db.user_groups where ADGroups().Contains(p.ad_group) orderby p.security_level descending select p).FirstOrDefault(); }
                catch
                { return db.user_groups.First(); }
            }
            catch
            {
                var dummy = new SlickTicket.DomainModel.user_group();
                dummy.id = 0;
                return dummy;
            }
        }
        else
        {
            throw new NotImplementedException("only Windows Authentication is supported at the moment");
        }
    }


    public static List<string> ADGroups()
    {
        List<string> groups = new List<string>();
        if (Settings.AuthenticationMode == AuthenticationMode.Windows)
        {
            List<string> groups_share = new List<string>();
            foreach (System.Security.Principal.IdentityReference group in System.Web.HttpContext.Current.Request.LogonUserIdentity.Groups)
            {
                string fullGroupName = group.Translate(typeof(System.Security.Principal.NTAccount)).ToString();
                int slashIndex = fullGroupName.IndexOf("\\");
                if (slashIndex > -1)
                    fullGroupName = fullGroupName.Substring(slashIndex + 1, fullGroupName.Length - slashIndex - 1);
                groups.Add(fullGroupName);
                groups_share.Add(fullGroupName);
            }
        }
        return groups;
    }
}
