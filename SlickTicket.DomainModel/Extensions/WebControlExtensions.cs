using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;

namespace SlickTicket.DomainModel.Extensions
{
    public static class WebControlExtensions
    {
        public static IEnumerable<FileStream> GetFileStreams(this FileUpload[] fuControls, string attachmentFolder)
        {
            List<FileStream> attachments = new List<FileStream>();
            foreach (FileUpload fu in fuControls.Where(x => x.HasFile))
            {
                string savePath = attachmentFolder + "temp\\";
                if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
                savePath = savePath + fu.FileName;
                if (!File.Exists(savePath))
                {
                    fu.SaveAs(savePath);
                    attachments.Add(File.OpenRead(savePath));
                }
            }
            return attachments;
        }

        public static void GetFileStreamsCleanup(this FileUpload[] fuControls, string attachmentFolder, IEnumerable<FileStream> attachments)
        {
            foreach (FileStream fs in attachments) { fs.Close(); fs.Dispose(); }
            string savePath = attachmentFolder + "temp\\";
            foreach (var file in Directory.GetFiles(savePath))
                File.Delete(file);
        }
    }
}
