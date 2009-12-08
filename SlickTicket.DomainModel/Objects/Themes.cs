using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SlickTicket.DomainModel.Objects
{
    public class Themes
    {
        public static XDocument Export()
        {
            var styles = List(new stDataContext());
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

        public static void Set(stDataContext db, string text, string borders, string body, string links, string hover, string buttonText, string alt, string header, string bg)
        {
            style _style = db.styles.First(s => s.id == 1);
            _style.text_color = HtmlFilter.Filter(text);
            _style.borders = HtmlFilter.Filter(borders);
            _style.body = HtmlFilter.Filter(body);
            _style.links = HtmlFilter.Filter(links);
            _style.hover = HtmlFilter.Filter(hover);
            _style.button_text = HtmlFilter.Filter(buttonText);
            _style.alt_rows = HtmlFilter.Filter(alt);
            _style.header = HtmlFilter.Filter(header);
            _style.background = HtmlFilter.Filter(bg);
            db.SubmitChanges();
        }

        public static IEnumerable<style> List(stDataContext db)
        {
            return from p in db.styles select p;
        }

        public static style Get(stDataContext db, int theme)
        {
            return db.styles.First(t => t.id == theme);
        }

        public static void Delete(stDataContext db, int theme)
        {
            db.styles.DeleteOnSubmit(Get(db, theme));
            db.SubmitChanges();
        }

        public static style Current(stDataContext db)
        {
            return Get(db, 1);
        }

        public static void Reset(stDataContext db)
        {
            style _default = db.styles.First(s => s.id == 2); //index of the default template
            Set(db, _default.text_color, _default.borders, _default.body, _default.links, _default.hover, _default.button_text, _default.alt_rows, _default.header, _default.background);
            db.SubmitChanges();
        }

        public static void Add(stDataContext db, string name, string text, string borders, string body, string links, string hover, string button, string headers, string alt, string background)
        {
            style s = new style();
            s.style_name = name;
            s.text_color = text;
            s.borders = borders;
            s.body = body;
            s.links = links;
            s.hover = hover;
            s.button_text = button;
            s.header = headers;
            s.alt_rows = alt;
            s.background = background;
            db.styles.InsertOnSubmit(s);
            db.SubmitChanges();
        }
    }
}
