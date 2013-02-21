using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using JimbeService.Business;
using JimbeService.IoC;
using JimbeWFC.ServiceContract;
using Ninject;
using TracerX;
using TraceLevel = System.Diagnostics.TraceLevel;

namespace JimbeService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private ServiceHost _host=null;

        private Thread _serviceThread;

        private ServiceManager _serviceManager;
        
        public void debugOnStart()
        {
            OnStart(new string[]{});
        }
        protected override void OnStart(string[] args)
        {

            Logger.Current.BinaryFileTraceLevel = TracerX.TraceLevel.Verbose;
            Logger.DefaultBinaryFile.Open();

            IKernel kernel = new StandardKernel(new AutoMapperModule(), new BusinessModule(), new NHibernateModule(), new WCFModule());

            _serviceManager = kernel.Get<ServiceManager>();

            _serviceThread = new Thread(_serviceManager.RunServiceManager);

            _serviceThread.Start();

            _host = new ServiceHost(kernel.Get<ILocationService>(), new Uri("http://localhost:9090/Jimbe"));
            var smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

            _host.Description.Behaviors.Add(smb);
            _host.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
            _host.Open();
        }

        protected override void OnStop()
        {
            if (_serviceThread.IsAlive && _serviceManager!=null)
            {
                _serviceManager.RequestStop();
                _serviceThread.Join();
                _serviceThread = null;
                _serviceManager = null;
            }
            if(_host!=null)
            {
                _host.Close();
                _host = null;
            }
        }
    }
}
