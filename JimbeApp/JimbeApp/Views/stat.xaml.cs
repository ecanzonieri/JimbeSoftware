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
	//		tmp=new List<KeyValuePair<string, int>>();
      //      tmp.Add(new KeyValuePair<string, int>("Dog", 30));
		/*	
			chart.DataContext = new KeyValuePair<string, int>[] {
                                    new KeyValuePair<string, int>("Dog", 30),
                                    new KeyValuePair<string, int>("Cat", 25),
                                    new KeyValuePair<string, int>("Rat", 5),
                                    new KeyValuePair<string, int>("Hampster", 8),
                                    new KeyValuePair<string, int>("Rabbit", 12) };
		*/
	//	    chart.DataContext = tmp;
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO: aggiungere l'implementazione del gestore dell'evento in questa posizione.
		  Button tmp_gd = (e.OriginalSource as Button);          
            if (tmp_gd.DataContext != null)
            {              
                MainWindowViewModel tmp = this.DataContext as MainWindowViewModel;
                tmp.StatisticList.Add(new KeyValuePair<String,Double>("qwe123qwe123",1234));

                //tmp.Add(new KeyValuePair<string, int>("Dog", 30));
             //   List<KeyValuePair<string, int>> tmp2=new List<KeyValuePair<string, int>>(tmp);
       //         chart.DataContext = tmp2;
            }
		}
	}
}