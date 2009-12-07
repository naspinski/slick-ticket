using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using SlickTicket.EmailParser.Properties;
using OpenPOP.MIMEParser;
using OpenPOP.POP3;

using SlickTicket.DomainModel;
// POP3 Component from http://sourceforge.net/projects/hpop/ s

namespace SlickTicket.EmailParser
{
    public class BusinessLogic
    {
        // NOT FINISHED YET!!!
        // NOT WORKING WITH SLICK TICKET FOR THE MOMENT!

        #region private properties
        private static System.Timers.Timer timerDoJobs = new System.Timers.Timer();
        private static List<Mailbox> Mailboxes = new List<Mailbox>();
        #endregion

        #region ctor
        public static void StartParser()
        {
            timerDoJobs.Interval = Settings.Default.Pop3Intervall;
            timerDoJobs.Elapsed += new ElapsedEventHandler(timeElapsed);
            timerDoJobs.Start();
            getMails();
        }
        #endregion

        #region Threading
        private class DoJob
        {
            Mailbox parseMailbox;
            public DoJob(Mailbox mbx)
            {
                parseMailbox = mbx;
            }
            public void runApp()
            {
                ParseEmails(parseMailbox);
            }
        }
        private static void getMails()
        {
            foreach (Mailbox mbx in Mailboxes)
            {
                DoJob launcher = new DoJob(mbx);
                new Thread(new ThreadStart(launcher.runApp)).Start();
                Thread.Sleep(1000);
            }
        }
        #endregion

        #region Timer stuff
        private static void timeElapsed(object sender, ElapsedEventArgs args)
        {
            getMails();
        }
        #endregion

        #region Methods
        public static void ParseEmails(Mailbox mbx)
        {
            POPClient pop3 = new POPClient();
            pop3.Connect(mbx.Host, mbx.Port);
            pop3.Authenticate(mbx.User, mbx.Password);

            int messageCount = pop3.GetMessageCount();
            int messageNumber = 0;

            while (messageNumber++ < messageCount)
            {
                Message message = pop3.GetMessage(messageNumber, false);

                string subject = message.Subject;
                string from = message.FromEmail;
                string body = message.MessageBody[0].ToString().Replace("\r\n", "<br>");

                int tid = 0;
                if (subject.Contains("[Ticket#"))
                {
                    tid = Convert.ToInt32(subject.Substring((subject.IndexOf("[Ticket#") + 8), (subject.IndexOf("]")) - (subject.IndexOf("[Ticket#") + 8)));
                }

                //DomainModel.Ticket.

                message.SaveAttachments(Settings.Default.AttachmentFolder);
                bool success = pop3.DeleteMessage(messageNumber);
            }

            if (pop3.Connected)
            {
                pop3.Disconnect();
            }
        }
        #endregion

        #region Objects
        public class Mailbox
        {
            public string Host { get; set; }
            public string EMailAdress { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public int Port { get; set; }
        }
        public class Ticket
        {
            public int id { get; set; }
            public string title { get; set; }
            public string details { get; set; }
            public int submitter { get; set; }
            public DateTime submitted { get; set; }
            public DateTime last_action { get; set; }
            public DateTime closed { get; set; }
            public int assigned_to_group { get; set; }
            public int assigned_to_group_last { get; set; }
            public int ticket_status { get; set; }
            public int priority { get; set; }
            public int originating_group { get; set; }
            public int active { get; set; }
            public int owner { get; set; }
            public int assigned_to { get; set; }
        }
        #endregion
    }
}