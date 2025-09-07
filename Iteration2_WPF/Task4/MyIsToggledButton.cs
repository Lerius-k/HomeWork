using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task4
{
    public class MyIsToggledButton : Button
    {
        // 1. Регистрируем DependencyProperty
        public static readonly DependencyProperty IsToggledProperty =
            DependencyProperty.Register(
                nameof(IsToggled),
                typeof(bool),
                typeof(MyIsToggledButton),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.None,
                    ClickStatusChanged));

        // 2. Обертка для удобного доступа
        public bool IsToggled
        {
            get => (bool)GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }

        // 3. Колбэк при изменении статуса
        private static void ClickStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = (MyIsToggledButton)d;
            bool newStatus = button.IsToggled;
            if (newStatus == false)
            {
                button.Content = "OFF";
                button.Background = Brushes.Red;
            }
            else
            {
                button.Content = "ON";
                button.Background = Brushes.Green;
            }
        }

        // 4. Конструктор кнопки и привязка события к методу обработки
        public MyIsToggledButton()
        {
            Content = "OFF";
            IsToggled = false;
            Background = Brushes.Red;
            Click += MyIsToggledButton_Click;
        }
        
        // 5. Метод обработки клика
        private void MyIsToggledButton_Click(object sender, RoutedEventArgs e)
        {
            IsToggled = !IsToggled;
        }
    }
}
