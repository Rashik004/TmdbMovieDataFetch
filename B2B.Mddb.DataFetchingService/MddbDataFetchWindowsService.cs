using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace B2B.Mddb.DataFetchingService
{
    public partial class MddbDataFetchWindowsService:ServiceBase
    {
        public MddbDataFetchWindowsService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            var ctx = new MddbBaseModel.MddbBaseModel();
            Console.ReadKey();
        }

        protected override void OnStop()
        {
        }
    }
}
