using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JimbeApp
{
	/// <summary>
	/// Logica di interazione per Window1.xaml
	/// </summary>
	  public enum TaskType
    {
        Spot,
        Periodic,
        Delayed
    }

    public partial class UpdateTask : Window
	{		
		public UpdateTask()
		{
			this.InitializeComponent();
		}
        private void Button1_Click(object sender, EventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt";
           // dlg.Filter = "Text documents (.txt)|*.txt";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                FileNameTextBox.Text = filename;
            }
        }

     

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.Close();
			
			// TODO: aggiungere l'implementazione del gestore dell'evento in questa posizione.
        }
	}
}