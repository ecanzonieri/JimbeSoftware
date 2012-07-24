using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using TracerX;

namespace Jimbe
{
    namespace JimbeCore
    {
        public class StartProcess : ITask
        {
            
            private static Logger logger = Logger.GetLogger("StartProcess");
            private Process process;

            public StartProcess(ProcessStartInfo info)
            {
                process = new Process();
                process.StartInfo = info;
                Logger.DefaultBinaryFile.Open();
            }

            public StartProcess(string filename) :this (new ProcessStartInfo(filename))
            {
                
            }

            public StartProcess(string filename, string arguments) :this(new ProcessStartInfo(filename,arguments))
            {
                
            }


            public virtual bool execute()
            {
                try
                {
                    process.Start();
                }
                catch (Exception ioe)
                {
                    logger.Error(ioe.Message);
                    throw;
                } 
                
                
            }
        }
    }
}