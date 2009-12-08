using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Timers;
using OpenPOP.MIMEParser;
using OpenPOP.POP3;
using SlickTicket.EmailParser.Properties;
using SlickTicket.DomainModel;
//POP3 Component from http://sourceforge.net/projects/hpop/


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
        public static void ParseEmails(DomainModel.Mailbox mbx)
        {
            POPClient pop3 = new POPClient();
            pop3.Connect(mbx.Host, mbx.Port);
            pop3.Authenticate(mbx.EmailAddress, mbx.Password);

            int messageCount = pop3.GetMessageCount();
            int messageNumber = 0;

            while (messageNumber++ < messageCount)
            {
                Message message = pop3.GetMessage(messageNumber, false);
                DomainModel.attachment att;
                string subject = message.Subject;
                string from = message.FromEmail;
                string body = message.MessageBody[0].ToString().Replace("\r\n", "<br>");

                int tid = 0;
                if (subject.Contains("[Ticket#"))
                {
                    tid = Convert.ToInt32(subject.Substring((subject.IndexOf("[Ticket#") + 8), (subject.IndexOf("]")) - (subject.IndexOf("[Ticket#") + 8)));
                }



                message.SaveAttachments(Settings.Default.AttachmentFolder);

                foreach (string f in Directory.GetFiles(@"C:\temp\"))
                {
                    FileInfo file = new FileInfo(f);
                    att = new DomainModel.attachment();
                    att.attachment_size = file.Length.ToString();
                    att.attachment_name = file.Name;
                    att.active = true;
                    att.ticket_ref = tid;
                    FileStream fs = new FileStream(f, FileMode.Open);
                    DomainModel.Objects.Attachment.Add(@"C:\temp\", GetFiles(@"C:\temp\"), att.ticket_ref, att.comment_ref);
                }

                //DomainModel.Ticket.Email eT = new SlickTicket.DomainModel.Ticket.Email();

                bool success = pop3.DeleteMessage(messageNumber);
            }
            if (pop3.Connected)
            {
                pop3.Disconnect();
            }
        }

        private static IEnumerable<FileStream> GetFiles(string directoryName)
        {
            string[] fileNames = Directory.GetFiles(directoryName, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string name in fileNames)
            {
                yield return File.OpenRead(name);
            }
        }
        #endregion
    }
}