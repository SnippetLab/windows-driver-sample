using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;


namespace FileProtectorCS
{
    public partial class EaseFilterService : ServiceBase
    {
        public EaseFilterService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            FilterWorker.StartService();
        }

        protected override void OnStop()
        {
            FilterWorker.StopService();
        }
    }
}
