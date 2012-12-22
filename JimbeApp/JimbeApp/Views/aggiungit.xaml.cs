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

namespace JimbeApp
{
	/// <summary>
	/// Logica di interazione per aggiungit.xaml
	/// </summary>
	public partial class aggiungit : UserControl
	{
		public aggiungit()
		{
			this.InitializeComponent();
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Grid item = (((((((e.OriginalSource as Button).Parent as Grid).Parent as Grid).Parent as UserControl).Parent as TabItem).Parent as TabControl).Parent as StackPanel).Parent as Grid;
		    //StackPanel a =item. as StackPanel;                        
		}
	}
}