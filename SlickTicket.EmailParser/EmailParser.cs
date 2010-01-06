using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SlickTicket.EmailParser
{
    public partial class EmailParser : ServiceBase
    {
        public EmailParser()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            BusinessLogic.StartParser();
        }

        protected override void OnStop()
        {
        }
    }
}
