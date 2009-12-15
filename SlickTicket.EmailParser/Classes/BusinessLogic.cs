﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;
using System.Timers;
using OpenPOP.MIMEParser;
using OpenPOP.POP3;
using SlickTicket.EmailParser.Properties;
using SlickTicket.DomainModel;
using SlickTicket.DomainModel.Objects;
//POP3 Component from http://sourceforge.net/projects/hpop/

// 15.12 8:13 <

namespace SlickTicket.EmailParser
{
    public class BusinessLogic
    {
        #region private properties
        private static System.Timers.Timer timerDoJobs = new System.Timers.Timer();
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
            foreach (Mailbox mbx in Units.SubUnits.Get(0).Mailboxes)  // <<<< Is correct?
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

        #region Get Mails
        public static void ParseEmails(DomainModel.Mailbox mbx)
        {
            try
            {
                POPClient pop3 = new POPClient();
                pop3.Connect(mbx.Host, mbx.Port);
                pop3.Authenticate(mbx.EmailAddress, mbx.Password);

                int messageCount = pop3.GetMessageCount();
                int messageNumber = 0;

                while (messageNumber++ < messageCount)
                {
                    Message m = pop3.GetMessage(messageNumber, false);
                    string subject = m.Subject;
                    string from = m.FromEmail;
                    string body = m.MessageBody[0].ToString().Replace("\r\n", "<br>");
                    string pathToAttachments = Settings.Default.AttachmentFolder + @"\" + System.Guid.NewGuid().ToString();

                    user u = Users.GetFromEmail(m.FromEmail);


                    int tid = 0;
                    if (subject.Contains("[Ticket#"))
                    {
                        tid = Convert.ToInt32(subject.Substring((subject.IndexOf("[Ticket#") + 8), (subject.IndexOf("]")) - (subject.IndexOf("[Ticket#") + 8)));
                    }

                    DomainModel.ticket ticket = DomainModel.Objects.Tickets.Get(tid);

                    m.SaveAttachments(pathToAttachments);

                    if (ticket == null)
                    {
                        stDataContext db = new stDataContext();    // <<<<< ???
                        Tickets.New(db, m.Subject, body, 0, 0, u, GetFiles(pathToAttachments), pathToAttachments);
                    }
                    else
                    {
                        Comments.Email.New(m.FromEmail, ticket.id, body, GetFiles(pathToAttachments), pathToAttachments);
                    }

                    try
                    {
                        Directory.Delete(pathToAttachments, true);
                        pop3.DeleteMessage(messageNumber);
                    }
                    catch (Exception exp)
                    {
                        Errors.New("Deleting Objects", exp);
                    }
                }
                if (pop3.Connected)
                {
                    pop3.Disconnect();
                }
            }
            catch (Exception exp)
            {
                Errors.New("ParseEmails() > END", exp);
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