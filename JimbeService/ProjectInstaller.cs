using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace JimbeService
{
    [RunInstaller(true)]
    public partial class JimbeServiceInstaller : System.Configuration.Install.Installer
    {
        public JimbeServiceInstaller()
        {
            InitializeComponent();

           
        }

        private void serviceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void serviceProcessInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
