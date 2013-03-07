using System.Collections.Generic;
using System.ServiceModel;
using JimbeWCF.DataContracts;

namespace JimbeWCF.ServiceContract
{
    [ServiceContract]
    public interface ITaskManagerService
    {
        [OperationContract]
        void ExecuteTasks(IList<Task> tasks);

        [OperationContract]
        void StopTaskExecution();
    }
}
