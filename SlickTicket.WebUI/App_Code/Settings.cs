using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Settings
/// </summary>
public static class Settings
{
    public static string AttachmentDirectory
    {
        get { return HttpContext.Current.Server.MapPath("~" + Utils.Settings.Get("attachments")); }
        set { Utils.Settings.Update("attachments", value); }
    }
}
