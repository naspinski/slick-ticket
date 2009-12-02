﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SlickTicket.DomainModel
{
    public class Attachments
    {
        public static void Add(string attachmentFolder, IEnumerable<FileStream> attachments, int ticket_id, int? comment_id)
        { Add(new dbDataContext(), attachmentFolder, attachments, ticket_id, comment_id); }
        public static void Add(dbDataContext db, string attachmentFolder, IEnumerable<FileStream> attachments, int ticket_id, int? comment_id)
        {
            if (attachments != null && attachments.Count() > 0)
            {
                attachment a;
                string filePath, fileName;

                foreach(FileStream file in attachments)
                {
                    fileName = Path.GetFileName(file.Name);
                    filePath = Path.GetDirectoryName(attachmentFolder);
                    filePath = filePath + "\\" + ticket_id + "\\" + fileName;

                    fileName = file.Save(filePath);

                    a = new attachment()
                    {
                        active = true,
                        attachment_name = fileName,
                        attachment_size = Convert.ToInt32(file.Length).ToString(),
                        ticket_ref = ticket_id,
                        comment_ref = comment_id,
                        submitted = DateTime.Now
                    };
                    db.attachments.InsertOnSubmit(a);
                }
                db.SubmitChanges();
            }
        }

    }
}
