using System.Collections.Generic;
using System.ServiceModel;
using JimbeWFC.DataContracts;

namespace JimbeWFC.ServiceContract
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
