using System.Collections.ObjectModel;
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

namespace Task8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Products.Add(new Product()
            {
                ProductName = "Мясо",
                Price = 350,
                Icon = "icons/meat.png",
                ProductType = ProductTypes.Food
            });
            Products.Add(new Product()
            {
                ProductName = "Морковка",
                Price = 35,
                Icon = "icons/сarrot.png",
                ProductType = ProductTypes.Food
            });
            Products.Add(new Product()
            {
                ProductName = "Наушники",
                Price = 2390,
                Icon = "icons/headphones.png",
                ProductType = ProductTypes.Device
            });
            
        }
    }
}