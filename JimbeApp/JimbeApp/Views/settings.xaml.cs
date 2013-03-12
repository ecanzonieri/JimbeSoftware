using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
	/// Logica di interazione per settings.xaml
	/// </summary>

	

	public partial class settings : UserControl
	{
		public settings()
		{
			this.InitializeComponent();
           
		}

		private void Slider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
		{			
			Slider slider = e.OriginalSource as Slider;

            if (slider != null && labelsens!=null)
            {
                switch((int)slider.Value)
				{
					case 1:
					{
						labelsens.Content="Bassa";
						break;
						
					}
					case 2:
					{
						labelsens.Content="Media";
						break;
					}
					case 3:
					{
						labelsens.Content="Alta";
						break;
					}
				}
            }
			
		}

        private void Salva(object sender, RoutedEventArgs e)
        {
            //salvo e mi riporto al menù getstatus

            MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
            tmp.saveSettings();
			Window parentWindow = Window.GetWindow(this);
            MainWindow main = (MainWindow)parentWindow;
            if (main != null)
            {
                main.enablebuttons();
                main.getstatus.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
			}
			
        }

        private void Annulla(object sender, RoutedEventArgs e)
        {
            //lascio tutto come è e mi riporto al menù getstatus
            
            Window parentWindow = Window.GetWindow(this);
            MainWindow main = (MainWindow)parentWindow;
            if (main != null)
            {
                main.enablebuttons();
                main.getstatus.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

        }
     
        private void list_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            int i = 0;
            Window parentWindow = Window.GetWindow(this);
            MainWindow main = (MainWindow)parentWindow;
            if (main != null)
            {
                main.disableunwantedbuttons();
            }
           


        }
	}
}