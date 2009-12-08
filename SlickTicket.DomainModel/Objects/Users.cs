using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Web;

namespace SlickTicket.DomainModel.Objects
{
    public class Users
    {
        public static string OutsideUser { get { return "outside.user"; } }

        public static user GetFromEmail(string email)
        { return GetFromEmail(new stDataContext(), email); }
        public static user GetFromEmail(stDataContext db, string email)
        {
            user u = db.users.FirstOrDefault(x => x.email.ToLower().Trim() == email.ToLower().Trim());
            if (u != null) return u; //user is already in the system

            //otherwise, make a new user
            try
            {  //this will make a new user depending on their AD information *if* they exist in AD
                ActiveDirectoryInfo adInfo = ActiveDirectoryInfo.Get(email);
                u = new user() { userName = adInfo.UserName, is_admin = false, phone = adInfo.Phone, sub_unit = adInfo.SubUnit, email = email };
                db.users.InsertOnSubmit(u);
                db.SubmitChanges();
                return u;
            }
            catch //if they are an outside user (no it AD) it will push them to a default dummy account
            {
                u = GetFromUserName(db, OutsideUser);
                if (u == null) // if this is the first time the dummy has been used, it makes the dummy user
                {
                    u = new user() { userName = OutsideUser, email = OutsideUser + "@unknown.com", sub_unit = Units.Default, phone = "555-5555" };
                    db.users.InsertOnSubmit(u);
                    db.SubmitChanges();
                }
                return u;
            }
        }

        public static user GetFromUserName(string userName)
        { return GetFromUserName(new stDataContext(), userName); }
        public static user GetFromUserName(stDataContext db, string userName)
        {
            return db.users.FirstOrDefault(x => x.userName.ToLower().Trim() == userName.ToLower().Trim());
        }

        public class ActiveDirectoryInfo
        {

            public string UserName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public int SubUnit { get; set; }

            /// <summary>
            /// Gets a ActiveDirectoryInfo object by searching with either Windows Username or Email Address
            /// IMPORTANT: this can only do lookups successfully if it is run with an account that is able to do AD lookups
            /// </summary>
            /// <param name="searchString">Windows Username or Email Address</param>
            /// <returns>ActiveDirectoryInfo of user</returns>
            public static ActiveDirectoryInfo Get(string searchString)
            {
                try
                {
                    string[] infoToGet = new string[] { "telephonenumber", "samaccountname" };
                    string sFilter = String.Format("(&(objectClass=user)(objectCategory=person)(|(mail={0})(mailNickName={1})))", searchString, searchString.Split(new char[] { '@' })[0]);
                    SearchResult sr = Get(AdsPath(), sFilter, infoToGet);

                    ActiveDirectoryInfo AdInfo = new ActiveDirectoryInfo();
                    AdInfo.Email = searchString;
                    AdInfo.Phone = sr.Properties["telephonenumber"] == null ? "555-5555" : sr.Properties["telephonenumber"][0].ToString();
                    AdInfo.UserName = sr.Properties["samaccountname"][0].ToString();
                    AdInfo.SubUnit = Units.Default;

                    return AdInfo;
                }
                catch (Exception ex)
                {
                    ex.Data.Add("searchString", searchString);
                    Errors.New(new stDataContext(), "User.ActiveDirectoryInformation.GetFromEmail", ex);
                    throw ex;
                }
            }
            private static string AdsPath()
            {
                DirectoryEntry entryRoot = new DirectoryEntry("LDAP://RootDSE"); //this will dynamically get your AD root
                string domain = entryRoot.Properties["defaultNamingContext"][0].ToString();//pulls the relevant information out
                return "LDAP://" + domain;//this is your complete baseLDAP string
            }
            private static SearchResult Get(string adsPath, string sFilter, string[] attribsToLoad)
            {
                DirectoryEntry searchRoot = new DirectoryEntry(adsPath, null, null, AuthenticationTypes.Secure);//sets search root to adsPath

                using (searchRoot)//this just pulls the infomation for the current user
                {
                    string user = Environment.UserName;
                    DirectorySearcher ds = new DirectorySearcher(searchRoot, sFilter, attribsToLoad, SearchScope.Subtree);
                    ds.SizeLimit = 1;
                    return ds.FindOne();
                }
            }
        }

        public static void Add(stDataContext db, string name, string email, string phone, int _sub_unit)
        {
            user newUser = new user();
            newUser.userName = name;
            newUser.email = HttpUtility.HtmlEncode(email);
            newUser.phone = phone;
            newUser.sub_unit = _sub_unit;
            db.users.InsertOnSubmit(newUser);
            db.SubmitChanges();
            if (newUser.id == 1) newUser.is_admin = true;
            db.SubmitChanges();
        }

        public static void Update(stDataContext db, string name, string email, string phone, int _sub_unit)
        {
            user thisUser = db.users.First(u => u.userName.Equals(name));
            thisUser.userName = name;
            thisUser.email = HttpUtility.HtmlEncode(email);
            thisUser.phone = phone;
            thisUser.sub_unit = _sub_unit;
            db.SubmitChanges();
        }

        public static IEnumerable<user> allInUnit(stDataContext db, int unitID)
        {
            return from p in db.users where p.sub_unit1.unit_ref == unitID select p;
        }

        public static void FlipAdmin(stDataContext db, int userID)
        {
            user u = db.users.First(p => p.id == userID);
            u.is_admin = !u.is_admin;
            db.SubmitChanges();
        }
    }
}
