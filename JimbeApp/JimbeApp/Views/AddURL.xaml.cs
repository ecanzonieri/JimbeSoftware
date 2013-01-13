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
using JimbeApp.ViewModels;

namespace JimbeApp
{
	/// <summary>
	/// Logica di interazione per AddURL.xaml
	/// </summary>

    public partial class AddURL : UserControl
	{
        private Window parentWindow;
        private MainWindow main;
        private MainWindowViewModel mainviewmodel;
        public int prec { set; get; }

		public AddURL()
		{
			this.InitializeComponent();
			
			// Inserire il codice richiesto per la creazione dell'oggetto al di sotto di questo punto.
		}

        private void close_window(object sender, System.Windows.RoutedEventArgs e)
        {
            parentWindow = System.Windows.Window.GetWindow(this);
            main = (MainWindow)parentWindow;
            mainviewmodel = this.DataContext as MainWindowViewModel;
            if (main != null)
            {
                main.tab_control.SelectedIndex = prec;
                if (prec == 1)
                main.enablebuttons();
            }
            if (mainviewmodel != null)
                mainviewmodel.eraseTaskProperty();
        }
	}
}