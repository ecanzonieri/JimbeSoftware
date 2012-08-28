using System;
using System.Diagnostics;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;
using TracerX;

namespace JimbeCore.Domain.Entities
{
    public class StartProcess : Task
    {
        
        private static Logger logger = Logger.GetLogger("StartProcess");
        
        public virtual string ProcessName { get; set; }
        public virtual string Arguments { get; set; }
        private ProcessStartInfo _processStartInfo;
        private Process _process;

        public StartProcess () :base()
        {
            Logger.DefaultBinaryFile.Open();
        }

        public StartProcess(string filename)
        {
            ProcessName = filename;
        }

        public StartProcess(string filename, string arguments)
        {
            ProcessName = filename;
            Arguments = arguments;
        }


        public override bool execute()
        {
            _processStartInfo = new ProcessStartInfo(ProcessName, Arguments);
            try
            {
                _process= new Process
                              {
                                  StartInfo = _processStartInfo
                              };

                return _process.Start();
            }
            catch (System.Exception ioe)
            {
                logger.Error(ioe.Message);
                throw;
            }
        }

        #region Overrides of Entity<Guid>

        public override bool EqualsBusiness(IBusinessComparable comparable)
        {
            if (ReferenceEquals(comparable, this)) return true;
            var other = comparable as StartProcess;
            if (ReferenceEquals(other, null)) return false;
            return ProcessName.Equals(other.ProcessName) && Arguments.Equals(other.Arguments);
        }

        #endregion
    }
}