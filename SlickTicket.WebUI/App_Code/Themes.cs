//Slick-Ticket v2.0 - 2009
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net

using System.Collections.Generic;
using System.Linq;
using SlickTicket.DomainModel;

/// <summary>
/// Summary description for Themes
/// </summary>
public static class Themes
{
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
