using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SlickTicket.Screenshooter
{
    static class Program
    {
        public static string homeURI = "";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
