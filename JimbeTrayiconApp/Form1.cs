using System;
using System.Drawing;
using System.ServiceProcess;
using System.Windows.Forms;
using JimbeApp;
using JimbeWCF.DataContracts;
using JimbeWCF.ServiceContract;
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
                notifyIcon1.Icon = Properties.Resources.flag_red;
                Application.Exit();
            }                       
            
            if (jimbeService.Status == ServiceControllerStatus.Running)
           {
               //servizio attivo
               //mi collego al servizio tramite wcf per ottenere la locazione attuale
               try
               {
                   _proxy = ProxyFactory.GetProxy(Properties.Settings.Default.Url_TryApp);
               }
               catch (Exception)
               {
                   attivatoreToolStripMenuItem.Text = "Attiva LocalizeMe";
                   locationName = "LocalizeMe non attivo";
                   notifyIcon1.Icon = Properties.Resources.flag_red;
                   notifyIcon1.Text = locationName;
               }               
                actualLocation = _proxy.GetCurrentLocation();
                attivatoreToolStripMenuItem.Text = "Disattiva LocalizeMe";
               if (actualLocation == null)
               {
                   //locazione non trovata
                   locationName = "nessuna locazione individuata";
                   notifyIcon1.Icon = Properties.Resources.flag_blue;
                   notifyIcon1.Text = locationName;
               }
               else
               {
                   //locazione trovata
                   locationName = actualLocation.Name;
                   notifyIcon1.Icon = Properties.Resources.flag_green;

               }
           }
           else               
               {
                   attivatoreToolStripMenuItem.Text = "Attiva LocalizeMe";
                   locationName = "LocalizeMe non attivo";
                   notifyIcon1.Icon = Properties.Resources.flag_red;
               }
        }


        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //chiude tutto 
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            jimbeService = new ServiceController("JimbeService");
            _proxy = ProxyFactory.GetProxy(Properties.Settings.Default.Url_TryApp);

            //controlla che il servizio è attivo e prende la locazione attuale aggiornando l'icona come serve
            if (jimbeService.Status != ServiceControllerStatus.StopPending && jimbeService.Status != ServiceControllerStatus.Stopped)
           {
               attivatoreToolStripMenuItem.Text = "Disattiva LocalizeMe";
               //servizio attivo
               //mi collego al servizio tramite wcf per ottenere la locazione attuale
               try
               {
                   actualLocation = _proxy.GetCurrentLocation();
               }
                catch(Exception){}

               if (actualLocation == null)
               {
                   //locazione non trovata
                   locationName = "nessuna locazione individuata";
                   notifyIcon1.Icon = Properties.Resources.flag_blue;
                   notifyIcon1.Text = locationName;

               }
               else
               {
                   //locazione trovata
                   locationName = actualLocation.Name;
                   notifyIcon1.Icon = Properties.Resources.flag_green;
                   notifyIcon1.Text = locationName;
               }
           }
           else               
               {
                   attivatoreToolStripMenuItem.Text = "Attiva LocalizeMe";
                   locationName = "LocalizeMe non attivo";
                   notifyIcon1.Icon = Properties.Resources.flag_red;
                   notifyIcon1.Text = locationName;
               }
            attivatoreToolStripMenuItem.Enabled = true;

        }

        private void attivatoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //attiva e dissattiva il servizio
            if (jimbeService.Status != ServiceControllerStatus.StopPending && jimbeService.Status != ServiceControllerStatus.Stopped)
            {
                try
                {
                    jimbeService.Stop();
                }
                catch (InvalidOperationException)
                {
               
                }
                catch(Exception){
                    MessageBox.Show("Errore durante lo stop del servizio, l'applicazione verrà chiusa");
                    Application.Exit();
                }
                notifyIcon1.Icon = Properties.Resources.flag_yellow;
                notifyIcon1.Text = "Disattivazione in corso";
                attivatoreToolStripMenuItem.Text = "Attiva LocalizeMe";
                attivatoreToolStripMenuItem.Enabled = false;

            }
            else
            {
                try
                {
                    jimbeService.Start();
                }
                catch (InvalidOperationException)
                {

                }
                catch (Exception)
                {
                    MessageBox.Show("Errore durante lo stop del servizio, l'applicazione verrà chiusa");
                    Application.Exit();
                }
                notifyIcon1.Icon = Properties.Resources.flag_yellow;
                notifyIcon1.Text = "Attivazione in corso";
                attivatoreToolStripMenuItem.Text = "Disattiva LocalizeMe";
                attivatoreToolStripMenuItem.Enabled = false;

            }
        }

    }
}
