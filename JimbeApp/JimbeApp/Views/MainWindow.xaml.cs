using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Navigation;

namespace JimbeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly SolidColorBrush _selectedButtonBrush = new SolidColorBrush(Colors.Black);
        private readonly SolidColorBrush _normalButtonBrush = new SolidColorBrush(Colors.Transparent);
        private readonly DropShadowEffect _dropShadowEffect= new DropShadowEffect();
     //   private readonly DropShadowEffect _effect= new DropShadowEffect();
        
        UIElement _uie = new UIElement();
        private UIElement _uie2= new UIElement();
        public MainWindow()
        {
            InitializeComponent();
            _buttons = new Collection<Button>() { getstatus, store, manage, stat };
            _selectedButtonBrush.Opacity = 0.5;
            _dropShadowEffect.Color = new Color();
            _dropShadowEffect.Color = Colors.Red;
            _dropShadowEffect.BlurRadius = 20;
            _dropShadowEffect.Opacity = 5;
            _dropShadowEffect.ShadowDepth = 10;  
      
           // _effect.Color=new Color();

            _uie.Effect = _dropShadowEffect;
        }
      
        private Collection<Button> _buttons;

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void store_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtonColors(sender);
           tab_control.SelectedIndex = 1;
        }

        private void ToggleButtonColors(object sender)
        {
            foreach (var b in _buttons.Where(x => x != sender))
            {
                b.Effect=_uie2.Effect;

            }
            var selected = sender as Button;
            selected.Effect = _uie.Effect;

        }

        private void getstatus_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtonColors(sender);
            tab_control.SelectedIndex = 0;
        }

        private void manage_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtonColors(sender);
            tab_control.SelectedIndex = 2;
        }

        private void stat_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtonColors(sender);
            tab_control.SelectedIndex = 3;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ToggleButtonColors(getstatus);
            tab_control.SelectedIndex = 0;
        }


        
    }
}
