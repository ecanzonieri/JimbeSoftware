namespace JimbeTrayiconApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.attivatoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.esciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "localized";
            this.notifyIcon1.BalloonTipTitle = "LocalizeMe";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attivatoreToolStripMenuItem,
            this.toolStripSeparator1,
            this.esciToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 54);
            // 
            // attivatoreToolStripMenuItem
            // 
            this.attivatoreToolStripMenuItem.Name = "attivatoreToolStripMenuItem";
            this.attivatoreToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.attivatoreToolStripMenuItem.Text = "Attiva Servizio";
            this.attivatoreToolStripMenuItem.Click += new System.EventHandler(this.attivatoreToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // esciToolStripMenuItem
            // 
            this.esciToolStripMenuItem.Name = "esciToolStripMenuItem";
            this.esciToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.esciToolStripMenuItem.Text = "Esci";
            this.esciToolStripMenuItem.Click += new System.EventHandler(this.esciToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 87);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem attivatoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem esciToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

