using System.Windows;
using WallOpening.ViewModels;

namespace WallOpening.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow(MainWindowViewModel mainWindowViewModel)//инициализируется запуск окна. и получаем экземпляр MainWindowViewModel
        {
            this.DataContext = mainWindowViewModel;
            InitializeComponent(); //инициализация компонентов окна            
        }
    }
}
