using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Model;
using System.Windows.Forms;

namespace JimbeCore.Domain.Entities
{
    class MessageInfo : Task
    {

        public virtual string Message { get; set; }
        
        public MessageInfo()
        {
        }
        
        public MessageInfo(string message)
        {
            Message = message;
        }
        
        #region Overrides of Entity<Guid>

        public override bool EqualsBusiness(IBusinessComparable comparable)
        {
            if (ReferenceEquals(comparable, this)) return true;
            var other = comparable as MessageInfo;
            if (ReferenceEquals(other, null)) return false;
            return Message.Equals(other.Message);
        }

        #endregion

        #region Overrides of Task

        public override void execute(object obj)
        {
            MessageBox.Show(Message);
        }

        #endregion
    }
}
