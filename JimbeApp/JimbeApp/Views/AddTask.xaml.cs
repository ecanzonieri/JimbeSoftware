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
using System.Windows.Navigation;
using System.Windows.Shapes;
using JimbeApp.ViewModels;
using JimbeWCF.DataContracts;

namespace JimbeApp
{
	/// <summary>
	/// Logica di interazione per AddTask.xaml
	/// </summary>


    public partial class AddTask : UserControl
	{
	    private Window parentWindow;
	    private MainWindow main;
	    private MainWindowViewModel mainviewmodel;
        public int prec { set; get; }
		public AddTask()
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

        private void saveTask(object sender, System.Windows.RoutedEventArgs e)
        {
            parentWindow = Window.GetWindow(this);
            main = (MainWindow) parentWindow;
            mainviewmodel = this.DataContext as MainWindowViewModel;
            if (main != null)
            {
                main.tab_control.SelectedIndex = prec;
                if (prec == 1)
                main.enablebuttons();
            }
        }

	    private void close_window(object sender, System.Windows.RoutedEventArgs e)
        {
            parentWindow = Window.GetWindow(this);
            main = (MainWindow)parentWindow;
            mainviewmodel = this.DataContext as MainWindowViewModel;
            if (main != null)
            {
                main.tab_control.SelectedIndex = prec;
                if(prec==1)
                main.enablebuttons();
            }
	        if (mainviewmodel != null)
                mainviewmodel.eraseTaskProperty();
        }

       
	}
}