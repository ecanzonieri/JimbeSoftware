﻿using System;
using System.Collections.Generic;
using System.Threading;
using JimbeCore.Domain.Entities;
using JimbeCore.Domain.Interfaces;
using TracerX;

namespace JimbeService.Business
{
    public class TaskManager 
    {

        private static Logger logger = Logger.GetLogger("TaskManager");

        private IEnumerable<ITask> _tasks;

        private IList<Timer> _periodic;

        private IList<Timer> _deleyed;

        public TaskManager(IEnumerable<ITask> tasks)
        {
            _tasks=tasks;
            _periodic=new List<Timer>();
            _deleyed=new List<Timer>();
        }

        public void StartTasksExecution()
        {
            DateTime current= DateTime.Now;


            foreach (Task task in _tasks)
            {
                if (task.Type == Task.TaskType.Spot)
                    task.execute(null);
                else if (task.Type == Task.TaskType.Periodic)
                {
                    var timer = new Timer(task.execute, null, 0, (long)task.Delay.TotalMilliseconds);
                    _periodic.Add(timer);
                }
                else
                {
                    var timer = new Timer(task.execute, null, (long)task.Delay.TotalMilliseconds, 0);
                    _deleyed.Add(timer);
                }
            }
        }

        public void RequestStop()
        {
            foreach (Timer timer in _periodic)
            {
                timer.Dispose();
            }
            foreach (Timer timer in _deleyed)
            {
                timer.Dispose();
            }
        }


    }
}