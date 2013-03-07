using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using JimbeService.Business;
using JimbeService.IoC;
using JimbeWCF.ServiceContract;
using Ninject;
using TracerX;

namespace JimbeServiceTestLikeApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Current.BinaryFileTraceLevel=TraceLevel.Verbose;
            Logger.DefaultBinaryFile.Open();

            IKernel kernel = new StandardKernel(new AutoMapperModule(), new BusinessModule(), new NHibernateModule(), new WCFModule());

            var _serviceManager = kernel.Get<ServiceManager>();

            var _serviceThread = new Thread(_serviceManager.RunServiceManager);

            _serviceThread.Start();

            var _host = new ServiceHost(kernel.Get<ILocationService>(), new Uri("http://localhost:9090/Jimbe"));
            var smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

            _host.Description.Behaviors.Add(smb);
            _host.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
            _host.AddServiceEndpoint(typeof (ILocationService),
                                     new WSDualHttpBinding(), "");
            _host.Open();

            Console.WriteLine("Host opened");
            Console.ReadLine();
            _serviceManager.RequestStop();

        }

    }
}
