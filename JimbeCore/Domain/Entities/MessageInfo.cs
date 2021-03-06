﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Model;
using System.Windows.Forms;

namespace JimbeCore.Domain.Entities
{
    public class MessageInfo : Task
    {

        public virtual string Message { get; set; }
        
        public MessageInfo()
        {
        }

        public MessageInfo(string message)
        {
            Message = message;
        }
        
        public MessageInfo(string message, TaskType type): base(type)
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

        public override void Execute(object obj)
        {

                MessageBox.Show(Message,"JimbeService Notification",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
                Success = true;          
        }

        #endregion
    }
}
