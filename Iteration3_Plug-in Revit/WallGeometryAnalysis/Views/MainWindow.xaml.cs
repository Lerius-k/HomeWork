using System.Windows;
using WallGeometryAnalysis.ViewModels;

namespace WallGeometryAnalysis.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            this.DataContext = mainWindowViewModel;
            InitializeComponent();
        }
    }
}
