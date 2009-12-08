using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickTicket.DomainModel.Objects
{
    public partial class Mailbox
    {
        public static int GetSubUnitId(string email)
        { return GetSubUnitId(new stDataContext(), email); }
        public static int GetSubUnitId(stDataContext db, string email)
        {
            if (string.IsNullOrEmpty(email.Trim())) return Unit.Default;
            Mailbox m = db.Mailboxes.FirstOrDefault(x => x.EmailAddress.ToLower().Trim() == email.ToLower().Trim());
            if (m == null) return Unit.Default;
            return m.SubUnitID;
        }

        public static Mailbox Get(int id)
        { return Get(new stDataContext(), id); }
        public static Mailbox Get(stDataContext db, int id)
        { return db.Mailboxes.FirstOrDefault(x => x.Id == id); }

        public static void New(string host, string email_address, string username, string password, int port, int sub_unit_id)
        { New(new stDataContext(), host, email_address, username, password, port, sub_unit_id); }
        public static void New(stDataContext db, string host, string email_address, string username, string password, int port, int sub_unit_id)
        {
            Mailbox m = new Mailbox()
            {
                Host = host,
                EmailAddress = email_address,
                Password = password,
                UserName = username,
                Port = port,
                SubUnitID = sub_unit_id
            };
            db.Mailboxes.InsertOnSubmit(m);
            db.SubmitChanges();
        }
    }
}
