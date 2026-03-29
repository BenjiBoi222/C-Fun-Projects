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

namespace TryingUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFileMenu();
        }

        private void ShowFileMenu()
        {
            // clear existing children
            MainContent.Children.Clear();

            // create a simple stack panel with some controls representing the File view
            var panel = new StackPanel { Margin = new Thickness(20) };

            var header = new TextBlock
            {
                Text = "File Menu",
                FontSize = 28,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(51,51,51)),
                Margin = new Thickness(0,0,0,20)
            };

            var openButton = new Button { Content = "Open", Width = 120, Margin = new Thickness(0,0,0,10) };
            var saveButton = new Button { Content = "Save", Width = 120, Margin = new Thickness(0,0,0,10) };
            var info = new TextBlock { Text = "Choose an action.", FontSize = 16, Foreground = new SolidColorBrush(Color.FromRgb(80,80,80)) };

            panel.Children.Add(header);
            panel.Children.Add(openButton);
            panel.Children.Add(saveButton);
            panel.Children.Add(info);

            MainContent.Children.Add(panel);
        }
    }
}