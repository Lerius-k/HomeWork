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

namespace Task3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        // Дополнительная переменная-хранилище для выбранных курсов
        private List<string> currentSelectedCourses = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }        

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {    
            // Проверяем и скрываем только те элементы, которые не null
            if (physics != null) physics.Visibility = Visibility.Collapsed;
            if (mathematics != null) mathematics.Visibility = Visibility.Collapsed;
            if (russianLanguage != null) russianLanguage.Visibility = Visibility.Collapsed;
            if (programming != null) programming.Visibility = Visibility.Collapsed;

            // Затем показываем только нужный c проверкой на null
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    if (physics != null)
                    {
                        physics.Visibility = Visibility.Visible;
                        physics.SelectedItems.Clear();
                    }
                    break;
                case 1:
                    if (mathematics != null)
                    {
                        mathematics.Visibility = Visibility.Visible;
                        mathematics.SelectedItems.Clear();
                    }
                    break;
                case 2:
                    if (russianLanguage != null)
                    {
                        russianLanguage.Visibility = Visibility.Visible;
                        russianLanguage.SelectedItems.Clear();
                    }
                    break;
                case 3:
                    if (programming != null)
                    {
                        programming.Visibility = Visibility.Visible;
                        programming.SelectedItems.Clear();
                    }
                    break;
            }

        }
        // Добавляем обработчики для каждого ListBox, чтобы обновлять хранилище при выборе
        private void Physics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (physics != null && physics.Visibility == Visibility.Visible)
            {
                currentSelectedCourses = physics.SelectedItems
                    .Cast<ListBoxItem>()
                    .Select(item => item.Content.ToString())
                    .ToList();
            }
        }

        private void Mathematics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mathematics != null && mathematics.Visibility == Visibility.Visible)
            {
                currentSelectedCourses = mathematics.SelectedItems
                    .Cast<ListBoxItem>()
                    .Select(item => item.Content.ToString())
                    .ToList();
            }
        }

        private void RussianLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (russianLanguage != null && russianLanguage.Visibility == Visibility.Visible)
            {
                currentSelectedCourses = russianLanguage.SelectedItems
                    .Cast<ListBoxItem>()
                    .Select(item => item.Content.ToString())
                    .ToList();
            }
        }

        private void Programming_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (programming != null && programming.Visibility == Visibility.Visible)
            {
                currentSelectedCourses = programming.SelectedItems
                    .Cast<ListBoxItem>()
                    .Select(item => item.Content.ToString())
                    .ToList();
            }
        }


        private void hoursChange_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (trainingHours != null) trainingHours.Content = hoursChange.Value;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            // Проверка заполнения имени
            if (string.IsNullOrWhiteSpace(User.Text))
            {
                MessageBox.Show("Ошибка: необходимо ввести имя!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // ПРОВЕРКА НА ВЫБРАННЫЕ КУРСЫ
            if (currentSelectedCourses == null || currentSelectedCourses.Count == 0)
            {
                MessageBox.Show("Ошибка: необходимо выбрать хотя бы один курс!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка согласия на рассылку
            if (!MailingCheckBox.IsChecked ?? false)
            {
                MessageBox.Show("Ошибка: необходимо согласиться на рассылку!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Формирование сообщения
            string message = $"Вы записаны на курс!\n\n" +
                             $"Имя: {User.Text}\n" +
                             $"Факультет: {comboBox.Text}\n" +
                             $"Курсы: {string.Join(", ", currentSelectedCourses)}\n" +
                             $"Форма обученя: {(fulltimeEducation.IsChecked == true ? "Очная" : "Заочная")}\n" +
                             $"Количесвто часов: {trainingHours.Content} ч. в неделю\n" +
                             $"Согласие на рассылку: {(MailingCheckBox.IsChecked == true ? "Да" : "Нет")}";

            MessageBoxResult result = MessageBox.Show(message, "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }
    }
}