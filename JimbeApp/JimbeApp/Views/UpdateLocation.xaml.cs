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
using JimbeWCF.DataContracts;

namespace JimbeApp
{
	/// <summary>
	/// Logica di interazione per aggiungit.xaml
	/// </summary>
	public partial class UpdateLocation : UserControl
	{
        private Window parentWindow;
        private MainWindow main;
        private MainWindowViewModel mainviewmodel;
        //public int prec { set; get; }
		public UpdateLocation()
		{
			this.InitializeComponent();
		}        

        private void NewTask(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            MainWindow main = (MainWindow)parentWindow;
            if (main != null)
                main.tab_control.SelectedIndex = 5;
            AddTask tmp = (AddTask)((TabItem)main.tab_control.Items[5]).Content;
            tmp.prec = 7;
        }

        private void NewUrl(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            MainWindow main = (MainWindow)parentWindow;
            if (main != null)
                main.tab_control.SelectedIndex = 6;
            AddURL tmp = (AddURL)((TabItem)main.tab_control.Items[6]).Content;
            tmp.prec = 7;
        }   

        private void ModifyTask(object sender, RoutedEventArgs e)
        {
            Button tmp_gd = (e.OriginalSource as Button);          
            if (tmp_gd.DataContext != null)
            {
                StartProcess loccc = (StartProcess)tmp_gd.DataContext;
                MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
                tmp.ModifyTask(loccc);
                Window parentWindow = Window.GetWindow(this);
                MainWindow main = (MainWindow)parentWindow;
                if (main != null)
                    main.tab_control.SelectedIndex = 8;
                UpdateTask tmp1 = (UpdateTask)((TabItem)main.tab_control.Items[8]).Content;
                tmp1.prec = 7;
                
                

            }
        }

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            Button tmp_gd = (e.OriginalSource as Button);
            if (tmp_gd.DataContext != null)
            {
                StartProcess loccc = (StartProcess)tmp_gd.DataContext;
                MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
                if(tmp!=null)
                tmp.DeleteTask(loccc);
            }
        }

        private void UpLocation(object sender, RoutedEventArgs e)
        {
            Button tmp_gd = (e.OriginalSource as Button);
            if (tmp_gd.DataContext != null)
            {                
                MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
                if (tmp != null)
                    tmp.SaveLocationChanges();
                close_window(sender, e);
            }
            
        }

        private void close_window(object sender, RoutedEventArgs e)
        {
            parentWindow = System.Windows.Window.GetWindow(this);
            main = (MainWindow)parentWindow;
            mainviewmodel = this.DataContext as MainWindowViewModel;
            if (main != null)
            {
                main.tab_control.SelectedIndex = 2;                
                main.enablebuttons();
            }
            if (mainviewmodel != null)
                mainviewmodel.eraseLocationProperty();


        }       
	}
}