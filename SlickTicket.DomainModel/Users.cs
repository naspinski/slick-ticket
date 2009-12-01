using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;

namespace SlickTicket.DomainModel
{
    public class Users
    {
        public static user GetFromEmail(string email)
        { return GetFromEmail(new dbDataContext(), email); }
        public static user GetFromEmail(dbDataContext db, string email)
        {
            user u = db.users.FirstOrDefault(x => x.email.ToLower().Trim() == email.ToLower().Trim());
            if (u != null) return u; //user is alreayd in the system
            return u;
        }

        public class ActiveDirectoryInfo
        {

            public string UserName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public int SubUnit { get; set; }

            /// <summary>
            /// Gets a ActiveDirectoryInfo object by searching with either Windows Username or Email Address
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
                    Errors.New(new dbDataContext(), "Users.ActiveDirectoryInformation.GetFromEmail", ex);
                    throw;
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
    }
}
