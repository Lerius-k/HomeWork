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

namespace Task1_2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    byte i = 0;
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        i++;
        if (i > 3)
        {
            i = 1;
        }
        switch (i)
        {
            case 1:
                {
                    GreenLight.Opacity = 0;
                    YellowLight.Opacity = 100;
                    RedLight.Opacity = 100;
                    break;
                }
            case 2:
                {
                    GreenLight.Opacity = 100;
                    YellowLight.Opacity = 0;
                    RedLight.Opacity = 100;
                    break;
                }
            case 3:
                {
                    GreenLight.Opacity = 100;
                    YellowLight.Opacity = 100;
                    RedLight.Opacity = 0;
                    break;
                }

        }
    }
}