using System;
using System.Collections.Generic;
using System.Linq;
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

namespace JimbeApp.Views
{
    /// <summary>
    /// Interaction logic for statisto.xaml
    /// </summary>
    public partial class statisto : UserControl
    {
        public statisto()
        {
            InitializeComponent();
        }

        private void Switch_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
            Window parentWindow = System.Windows.Window.GetWindow(this);
            MainWindow main = (MainWindow)parentWindow;
            if (main != null && tmp != null)
            {
                tmp.ShowStats_f();
                main.tab_control.SelectedIndex = 3;
            }
        }
    }
}
