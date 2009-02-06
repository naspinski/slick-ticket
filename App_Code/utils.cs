using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SlickTicketExtensions;

/// <summary>
///  App Utilities
/// </summary>
public class utils
{
    public static List<string> userGroups()
    {
        List<string> groups = new List<string>();
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
        return groups;
    }

    public static string userName()
    {
        return (HttpContext.Current.User.Identity.Name.Split(new char[] { '\\' }))[1];
    }

    public static string trimForSideBar(string trimThis, int toLength)
    {
        if (trimThis.Length > toLength) trimThis = trimThis.Substring(0, toLength) + "...";
        return trimThis;
    }

    public static int ADGroupListUpdate()
    {
        string file_location = HttpContext.Current.Server.MapPath("~") + "\\App_Data\\ADGroups.xml";
        int GroupCount = 0;

        DirectoryEntry dirEnt = new DirectoryEntry("LDAP://" + utils.settings.get("domain_controller") );
        string[] loadProps = new string[] { "name" }; 

        XDocument xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
        XElement root = new XElement("groups");

        using (DirectorySearcher srch = new DirectorySearcher(dirEnt, "(objectClass=Group)", loadProps))
        {
            srch.PageSize = 6000;
            var results = safeFindAll(srch);
            foreach (SearchResult sr in results)
            {
                XElement xe = new XElement("group", sr.Properties["name"][0].ToString());
                root.Add(xe);
                GroupCount++;
            }
        }

        xDoc.Add(root);
        if (File.Exists(file_location)) File.Delete(file_location);
        xDoc.Save(file_location);

        return GroupCount;
    }

    public static IEnumerable<SearchResult> safeFindAll(DirectorySearcher searcher)
    {
        using (SearchResultCollection results = searcher.FindAll())
        {
            foreach (SearchResult result in results)
            {
                yield return result;
            } // SearchResultCollection will be disposed here
        }
    }
    
    public static void sendEmail(string to, string subject, string body)
    {
        MailMessage message = new MailMessage(utils.settings.get("system_email"), to, subject, body);
        SmtpClient smtp = new SmtpClient(utils.settings.get("smtp"));
        smtp.Send(message);
    }
    
    public static user_group accessLevel()
    {
        dbDataContext db = new dbDataContext();
        try
        {
            try
            { return (from p in dbi.permissions.list(db) where userGroups().Contains(p.ad_group) orderby p.security_level descending select p).FirstOrDefault(); }
            catch
            { return db.user_groups.First(); }
        }
        catch
        {
            user_group dummy = new user_group();
            dummy.id = 0;
            return dummy;
        }
    }

    public static void populateSubUnits(dbDataContext db, DropDownList ddlFrom, DropDownList ddlTo, int accessLevel)
    {
        ddlTo.Items.Clear();
        var sus = dbi.groups.subGroups.list(db, Int32.Parse(ddlFrom.SelectedValue), accessLevel);
        if (sus.Count() < 1)
            ddlTo.Items.Add(new ListItem("No sub-groups available to you", "0"));
        else
            foreach (sub_unit su in sus) ddlTo.Items.Add(new ListItem(su.sub_unit_name, su.id.ToString()));
    }

    public class settings
    {
        public static string get(string setting)
        {
            XElement x = XElement.Load(HttpContext.Current.Server.MapPath("~") + "\\App_Data\\settings.xml");
            return (from p in x.Descendants(setting) select p).First().Value;
        }

        public static void update(string setting, string value)
        {
            string file_location = HttpContext.Current.Server.MapPath("~") + "\\App_Data\\settings.xml";
            XElement x = XElement.Load(file_location);
            XElement xe = (from p in x.Descendants(setting) select p).First();
            xe.Value = value;
            x.Save(file_location);
        }
        public static bool emailIsRestricted()
        {
            XElement x = XElement.Load(HttpContext.Current.Server.MapPath("~") + "\\App_Data\\settings.xml");
            return bool.Parse((from p in x.Descendants("restrict_email_addresses") select p).First().Value);
        }
    }

    public class faqs
    {
        public static string import(Stream xmlFile)
        {
            string output = string.Empty;
            try
            {
                TextReader rdr = new StreamReader(xmlFile);
                XElement x = XElement.Load(rdr);
                var faqs = from p in x.Descendants("faq") select p;

                foreach (XElement xe in faqs)
                {
                    dbDataContext db = new dbDataContext();
                    faq f = new faq();
                    f.title = xe.FirstAttribute.Value;
                    f.body = xe.Value;
                    db.faqs.InsertOnSubmit(f);
                    try
                    {
                        db.SubmitChanges(); //inefficient to submit each time, *but* this will tell which faqs got inserted and which didn't
                        output += "<div class='success'>-" + xe.FirstAttribute.Value + " successfully imported</div>";
                    }
                    catch (Exception ex)
                    {
                        output += "<div class='error'>-Error importing " + xe.FirstAttribute.Value + "</div>";
                    }
                }
            }
            catch (Exception ex)
            {
                output = "<div class='error'>Error: <div class='sub_error'>" + ex.Message + "</div></div>";
            }
            return output;
        }

        public static XDocument export()
        {
            var faqs = dbi.faqs.list(new dbDataContext());
            XDocument xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            XElement root = new XElement("faqs");
            foreach (faq f in faqs)
            {
                XElement xeFaq = new XElement("faq");
                xeFaq.Add(new XAttribute("name", f.title));
                xeFaq.SetValue(f.body);
                root.Add(xeFaq);
            }
            xDoc.Add(root);
            return xDoc;
        }
    }

    public class styles
    {
        public static string import(Stream xmlFile)
        {
            string output = string.Empty;
            try
            {
                TextReader rdr = new StreamReader(xmlFile);
                XElement x = XElement.Load(rdr);
                var styles = from p in x.Descendants("style") select p;

                foreach (XElement xe in styles)
                {
                    dbDataContext db = new dbDataContext();
                    Dictionary<string, string> styleAttributes = new Dictionary<string, string>();
                    style s = new style();
                    foreach (XAttribute xa in xe.Attributes())
                    {
                        styleAttributes.Add(xa.Name.ToString(), xa.Value);
                    }
                    s.alt_rows = styleAttributes["alt_rows"];
                    s.background = styleAttributes["background"];
                    s.body = styleAttributes["body"];
                    s.borders = styleAttributes["borders"];
                    s.button_text = styleAttributes["button_text"];
                    s.header = styleAttributes["header"];
                    s.hover = styleAttributes["hover"];
                    s.links = styleAttributes["links"];
                    s.style_name = styleAttributes["style_name"];
                    s.text_color = styleAttributes["text_color"];
                    db.styles.InsertOnSubmit(s);
                    try
                    {
                        db.SubmitChanges(); //inefficient to submit each time, *but* this will tell which styles got inserted and which didn't
                        output += "<div class='success'>-" + styleAttributes["style_name"] + " successfully imported</div>";
                    }
                    catch (Exception ex)
                    {
                        output += "<div class='error'>-Error importing " + styleAttributes["style_name"] + " - <span class='smaller'>most likely a duplicate</span></div>";
                    }
                }
            }
            catch (Exception ex)
            {
                output = "<div class='error'>Error: <div class='sub_error'>" + ex.Message + "</div></div>";
            }
            return output;
        }

        public static XDocument export()
        {
            var styles = dbi.themes.list(new dbDataContext());
            XDocument xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            XElement root = new XElement("styles");
            foreach (style s in styles)
            {
                XElement xeStyle = new XElement("style");
                xeStyle.Add(new XAttribute("style_name", s.style_name));
                xeStyle.Add(new XAttribute("text_color", s.text_color));
                xeStyle.Add(new XAttribute("borders", s.borders));
                xeStyle.Add(new XAttribute("body", s.body));
                xeStyle.Add(new XAttribute("links", s.links));
                xeStyle.Add(new XAttribute("hover", s.hover));
                xeStyle.Add(new XAttribute("button_text", s.button_text));
                xeStyle.Add(new XAttribute("header", s.header));
                xeStyle.Add(new XAttribute("alt_rows", s.alt_rows));
                xeStyle.Add(new XAttribute("background", s.background));
                root.Add(xeStyle);
            }
            xDoc.Add(root);
            return xDoc;
        }
    }

    public class menus
    {
        public static IEnumerable<XElement> main()
        {
            XDocument x = XDocument.Load(HttpContext.Current.Server.MapPath("~/") + "/App_Data/main_menu.xml");
            return from p in x.Descendants("page") select p;
        }

        public static IEnumerable<XElement> admin()
        {
            XDocument x = XDocument.Load(HttpContext.Current.Server.MapPath("~/") + "/App_Data/main_menu.xml");
            return from p in x.Descendants("admin_page") select p;
        }
    }
}
