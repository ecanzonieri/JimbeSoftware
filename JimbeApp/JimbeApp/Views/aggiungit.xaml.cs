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
using JimbeWFC.DataContracts;

namespace JimbeApp
{
	/// <summary>
	/// Logica di interazione per aggiungit.xaml
	/// </summary>
	public partial class aggiungit : UserControl
	{
        private Window parentWindow;
        private MainWindow main;
        private MainWindowViewModel mainviewmodel;

		public aggiungit()
		{
			this.InitializeComponent();
		}
       
        private void StoreLocation(object sender, System.Windows.RoutedEventArgs e)
		{
            parentWindow = Window.GetWindow(this);
            main = (MainWindow)parentWindow;
            if (main != null)
                main.tab_control.SelectedIndex = 0;          
		}

		private void addtask_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
		    tmp.NewTask_f();		   
		}

        private void ModifyTask(object sender, RoutedEventArgs e)
        {
            Button tmp_gd = (e.OriginalSource as Button);          
            if (tmp_gd.DataContext != null)
            {
                StartProcess loccc = (StartProcess)tmp_gd.DataContext;
                MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
                tmp.ModifyTask(loccc);
                parentWindow = Window.GetWindow(this);
                main = (MainWindow)parentWindow;
                if (main != null)
                {
                    main.tab_control.SelectedIndex = 7;
                    main.disableunwantedbuttons();
                }
                UpdateTask tmp1 = (UpdateTask)((TabItem)main.tab_control.Items[7]).Content;
                tmp1.prec = 1;
                
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

        private void NewTask(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            MainWindow main = (MainWindow) parentWindow;
            if (main != null)
            {
                main.tab_control.SelectedIndex = 4;
                main.disableunwantedbuttons();
            }
            AddTask tmp = (AddTask)((TabItem)main.tab_control.Items[4]).Content;
            tmp.prec = 1;          
        }

        private void NewUrl(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            MainWindow main = (MainWindow)parentWindow;
            if (main != null)
            {
                main.tab_control.SelectedIndex = 5;
                main.disableunwantedbuttons();
            }
            AddURL tmp = (AddURL)((TabItem)main.tab_control.Items[5]).Content;
            tmp.prec = 1;
        }

        private void inizializeLocationdata(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
            if (tmp != null)
                tmp.eraseLocationProperty();
        }     
    }
}