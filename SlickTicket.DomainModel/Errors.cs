using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickTicket.DomainModel
{
    public class Errors
    {
        private static string N = Environment.NewLine;

        public static void New(string title_prefix, Exception ex)
        { New(new dbDataContext(), title_prefix, ex); }
        public static void New(dbDataContext db, string title_prefix, Exception exception)
        {
            error e = new error() { title = title_prefix + " - " + exception.Message, occured = DateTime.Now };
            e.details = "Exception.Data - " + N;
            foreach(var key in exception.Data.Keys)
                    e.details += key.ToString() + ": " + exception.Data[key].ToString() + " " + N;
            db.errors.InsertOnSubmit(e);
            db.SubmitChanges();
        }
    }
}
