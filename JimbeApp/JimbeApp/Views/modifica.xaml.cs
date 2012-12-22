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
using JimbeWFC.DataContracts;
using JimbeApp.ViewModels;

namespace JimbeApp
{
	/// <summary>
	/// Logica di interazione per modifica.xaml
	/// </summary>
	public partial class modifica : UserControl
	{
		public modifica()
		{
			this.InitializeComponent();
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Button tmp_gd = (e.OriginalSource as Button);

            //sistemare qui!!!
            if (tmp_gd.DataContext != null)
            {
            Location loccc = (Location)tmp_gd.DataContext;           
                MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;

                tmp.delete_location(loccc);
            }

        }
	}
}