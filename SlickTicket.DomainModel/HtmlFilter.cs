using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Web;

namespace SlickTicket.DomainModel
{
    public static class HtmlFilter
    {
        public static string[] DefaultBlockedTags = new string[] { "script", "style" };
        public static string Filter(string html)
        { return Filter(html, DefaultBlockedTags); }
        public static string Filter(string html, string[] blockedTags)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            StringBuilder buffer = new StringBuilder();
            Process(doc.DocumentNode, buffer, blockedTags);

            return buffer.ToString().Replace("<#document>", string.Empty).Replace("</#document>", string.Empty);
        }

        static void Process(HtmlNode node, StringBuilder buffer, string[] blockedTags)
        {
            bool allowedTag;
            switch (node.NodeType)
            {
                case HtmlNodeType.Text:
                    buffer.Append(HttpUtility.HtmlEncode(((HtmlTextNode)node).Text));
                    break;
                case HtmlNodeType.Element:
                case HtmlNodeType.Document:
                    allowedTag = !blockedTags.Contains(node.Name.ToLower());
                    if (allowedTag)
                    {
                        buffer.AppendFormat("<{0}>", node.Name);
                        foreach (HtmlNode childeNode in node.ChildNodes)
                            Process(childeNode, buffer, blockedTags);
                        buffer.AppendFormat("</{0}>", node.Name);
                    }
                    break;
            }
        }
    }
}
