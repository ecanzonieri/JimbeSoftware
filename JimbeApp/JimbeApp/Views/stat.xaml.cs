﻿using System;
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

namespace JimbeApp
{
	/// <summary>
	/// Logica di interazione per stat.xaml
	/// </summary>
	public partial class stat : UserControl
	{
	    private List<KeyValuePair<string, int>> tmp; 
		public stat()
		{
			this.InitializeComponent();
		}


        private void Switch_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
           Window parentWindow = System.Windows.Window.GetWindow(this);
           MainWindow main = (MainWindow)parentWindow;
            if (main != null && tmp!=null)
            {
                tmp.ShowStats_f();
                main.tab_control.SelectedIndex = 9;
            }
        }
	}
}