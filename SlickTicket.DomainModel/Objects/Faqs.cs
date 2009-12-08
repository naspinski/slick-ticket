using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace SlickTicket.DomainModel.Objects
{
    public class Faqs
    {
        public static void Add(stDataContext db, string q, string a)
        {
            faq f = new faq();
            f.title = HtmlFilter.Filter(q);
            f.body = HtmlFilter.Filter(a);
            db.faqs.InsertOnSubmit(f);
            db.SubmitChanges();
        }

        public static void Delete(stDataContext db, int id)
        {
            db.faqs.DeleteOnSubmit(Get(db, id));
            db.SubmitChanges();
        }

        public static faq Get(stDataContext db, int id)
        {
            return db.faqs.First(f => f.id == id);
        }

        public static IEnumerable<faq> List(stDataContext db)
        {
            return from p in db.faqs select p;
        }

        public static void Edit(stDataContext db, int id, string q, string a)
        {
            faq f = Get(db, id);
            f.title = HtmlFilter.Filter(q);
            f.body = HtmlFilter.Filter(a);
            db.SubmitChanges();
        }

        public static XDocument Export()
        {
            var faqs = Faqs.List(new stDataContext());
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
}
