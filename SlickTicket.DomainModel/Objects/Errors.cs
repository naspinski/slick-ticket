using System;

namespace SlickTicket.DomainModel.Objects
{
    public class Errors
    {
        private static string N = Environment.NewLine;

        public static void New(string title_prefix, Exception ex)
        { New(new stDataContext(), title_prefix, ex); }
        public static void New(stDataContext db, string title_prefix, Exception exception)
        {
            error e = new error() { title = title_prefix + " - " + exception.Message, occured = DateTime.Now };
            e.details = "Exception.Data - " + N;
            foreach (var key in exception.Data.Keys)
                e.details += key.ToString() + ": " + exception.Data[key].ToString() + " " + N;
            db.errors.InsertOnSubmit(e);
            db.SubmitChanges();
        }
    }
}
