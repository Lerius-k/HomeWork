using Microsoft.Win32;
using System.IO;
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

namespace Task2
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

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*)";
            if (openFileDialog.ShowDialog() == true)
            {
                myTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
            StatusText.Text = $"Открыт файл {openFileDialog.FileName}";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*)";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, myTextBox.Text);
            }
            StatusText.Text = $"Изменения сохранены в файле {saveFileDialog.FileName}";
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "Ожидание";
            WindowWithInfo windowWithInfo = new WindowWithInfo();
            windowWithInfo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowWithInfo.WindowStyle = WindowStyle.ToolWindow;
            windowWithInfo.ResizeMode = ResizeMode.NoResize;
            windowWithInfo.ShowDialog();
            StatusText.Text = "Готов";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StatusText.Text = "Ожидание";
            MessageBoxResult result = MessageBox.Show("Вы точно хотите закрыть программу?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            StatusText.Text = "Готов";
        }

        private void myTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StatusText.Text = "Внесены изменения. Сохраните их перед выходом.";
        }



        private void myTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            myTextBlock.Visibility = Visibility.Collapsed;
        }

    }
}