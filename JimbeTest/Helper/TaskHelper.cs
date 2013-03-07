using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using JimbeCore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTO = JimbeWCF.DataContracts;

namespace JimbeTest.Helper
{
    internal class TaskHelper
    {
        
        static internal IEnumerable<Task> GetTasks()
        {
            IList<Task> tasks = new List<Task>();
            tasks.Add(new StartProcess("http://www.google.it"));
            Task task = new MessageInfo("Periodioco 30s", Task.TaskType.Periodic);
            task.Delay=new TimeSpan(0,0,0,30);
            tasks.Add(task);
            task = new MessageInfo("Delayed 10 s ", Task.TaskType.Delayed);
            task.Delay=new TimeSpan(0,0,0,10);
            tasks.Add(task);
            task = new MessageInfo("Periodic 60s",Task.TaskType.Periodic);
            task.Delay=new TimeSpan(0,0,1,0);
            tasks.Add(task);
            return tasks;

        }

        public static IList<JimbeWCF.DataContracts.Task> GetDtoTasks()
        {
            IList<DTO.Task> tasks = new List<DTO.Task>();
            tasks.Add(new DTO.StartProcess()
                {
                    ProcessName = "pippo",
                    Arguments = "pippo",
                    Type = DTO.Task.TaskType.Spot
                });
            tasks.Add(new DTO.StartProcess()
            {
                ProcessName = "pippo1",
                Arguments = "pippo1",
                Delay = new TimeSpan(0,0,1),
                Type = DTO.Task.TaskType.Delayed
            });
            tasks.Add(new DTO.StartProcess()
            {
                ProcessName = "pippo2",
                Arguments = "pippo2",
                Delay = new TimeSpan(0,0,2),
                Type = DTO.Task.TaskType.Periodic
            });
            return tasks;
        }
    }
}
