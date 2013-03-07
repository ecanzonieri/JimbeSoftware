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
using JimbeWCF.DataContracts;
using JimbeApp.ViewModels;

namespace JimbeApp
{
	/// <summary>
	/// Logica di interazione per modifica.xaml
	/// </summary>
	public partial class modifica : UserControl
	{
        private Window parentWindow;
        private MainWindow main;
        private MainWindowViewModel mainviewmodel;

		public modifica()
		{
			this.InitializeComponent();
		}

        private void DeleteLocation(object sender, RoutedEventArgs e)
        {
            
            Button tmp_gd = (e.OriginalSource as Button);          
            if (tmp_gd.DataContext != null)
            {
            Location loccc = (Location)tmp_gd.DataContext;           
                MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;

                tmp.delete_location(loccc);
            }

        }

        private void ModifyLocation(object sender, RoutedEventArgs e)
        {
            Button tmp_gd = (e.OriginalSource as Button);
            if (tmp_gd.DataContext != null)
            {
                Location loccc = (Location)tmp_gd.DataContext;
                MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
                tmp.ModifyLocation(loccc);

                Window parentWindow = Window.GetWindow(this);
                MainWindow main = (MainWindow)parentWindow;
                if (main != null)
                {
                    main.tab_control.SelectedIndex = 6;
                    main.disableunwantedbuttons();
                }

            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
	}
}