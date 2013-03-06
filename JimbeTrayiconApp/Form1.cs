using System;
using System.Drawing;
using System.ServiceProcess;
using System.Windows.Forms;
using JimbeApp;
using JimbeWFC.DataContracts;
using JimbeWFC.ServiceContract;
namespace JimbeTrayiconApp
{
    public partial class Form1 : Form
    {
        private ILocationService _proxy=null;
        private Location actualLocation = null;
        private ServiceController jimbeService=null;
        private String locationName = null;


        public Form1()
        {
            InitializeComponent();
            
            //ottengo l'instanza del servizio
            try
            {
                jimbeService = new ServiceController("JimbeService");
            }
            catch (Exception)
            {
                MessageBox.Show("Servizio non trovato, assicurati di averlo installato");
                notifyIcon1.Icon = Properties.Resources.serviceOff;
                Application.Exit();
            }                       
            
            if (jimbeService.Status == ServiceControllerStatus.Running)
           {
               //servizio attivo
               //mi collego al servizio tramite wcf per ottenere la locazione attuale
               try
               {
                   _proxy = ProxyFactory.GetProxy();
               }
               catch (Exception)
               {
                   attivatoreToolStripMenuItem.Text = "Attiva LocalizeMe";
                   locationName = "LocalizeMe non attivo";
                   notifyIcon1.Icon = Properties.Resources.serviceOff;
                   notifyIcon1.Text = locationName;
               }               
                actualLocation = _proxy.GetCurrentLocation();
                attivatoreToolStripMenuItem.Text = "Disattiva LocalizeMe";
               if (actualLocation == null)
               {
                   //locazione non trovata
                   locationName = "nessuna locazione individuata";
                   notifyIcon1.Icon = Properties.Resources.serviceOn;
                   notifyIcon1.Text = locationName;
               }
               else
               {
                   //locazione trovata
                   locationName = actualLocation.Name;
                   notifyIcon1.Icon=Properties.Resources.locationFound;

               }
           }
           else               
               {
                   attivatoreToolStripMenuItem.Text = "Attiva LocalizeMe";
                   locationName = "LocalizeMe non attivo";
                   notifyIcon1.Icon = Properties.Resources.serviceOff;
               }
        }


        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //chiude tutto 
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //controlla che il servizio è attivo e prende la locazione attuale aggiornando l'icona come serve
            if (jimbeService.Status == ServiceControllerStatus.Running)
           {
               attivatoreToolStripMenuItem.Text = "Disattiva LocalizeMe";
               //servizio attivo
               //mi collego al servizio tramite wcf per ottenere la locazione attuale
               actualLocation = _proxy.GetCurrentLocation();
               if (actualLocation == null)
               {
                   //locazione non trovata
                   locationName = "nessuna locazione individuata";
                   notifyIcon1.Icon = Properties.Resources.serviceOn;
                   notifyIcon1.Text = locationName;

               }
               else
               {
                   //locazione trovata
                   locationName = actualLocation.Name;
                   notifyIcon1.Icon=Properties.Resources.locationFound;
                   notifyIcon1.Text = locationName;
               }
           }
           else               
               {
                   attivatoreToolStripMenuItem.Text = "Attiva LocalizeMe";
                   locationName = "LocalizeMe non attivo";
                   notifyIcon1.Icon = Properties.Resources.serviceOff;
                   notifyIcon1.Text = locationName;
               }
        }

        private void attivatoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //attiva e dissattiva il servizio
            if (jimbeService.Status == ServiceControllerStatus.Running)
            {
                try
                {
                    jimbeService.Stop();
                }
                catch (Exception)
                {
                    MessageBox.Show("Errore durante lo stop del servizio, l'applicazione verrà chiusa");
                    Application.Exit();
                }
                notifyIcon1.Icon = Properties.Resources.serviceOff;
                notifyIcon1.Text = "LocalizeMe non attivo";
                attivatoreToolStripMenuItem.Text = "Attiva LocalizeMe";

            }
            else
            {
                try
                {
                    jimbeService.Start();
                }
                catch (Exception)
                {
                    MessageBox.Show("Errore durante l'avvio del servizio, l'applicazione verrà chiusa");                   
                    Application.Exit();
                }
                notifyIcon1.Icon = Properties.Resources.serviceOn;
                notifyIcon1.Text = "nessuna locazione individuata";
                attivatoreToolStripMenuItem.Text = "Disattiva LocalizeMe";

            }
        }

    }
}
