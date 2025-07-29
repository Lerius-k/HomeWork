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

namespace Task1_3;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_MouseEnter(object sender, MouseEventArgs e)
    {
        int halfWidth = Convert.ToInt32(MyGrid.ActualWidth / 2);
        int halfHeight = Convert.ToInt32(MyGrid.ActualHeight / 2);

        Random rnd = new Random();
        int marginWidth = rnd.Next(-halfWidth, ++halfWidth);
        int marginHeight = rnd.Next(-halfHeight, ++halfHeight);

        button.Margin = new Thickness(marginWidth, marginHeight,0,0);

    }
}