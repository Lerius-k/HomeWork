using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControlExample_v2
{
    /// <summary>
    /// Логика взаимодействия для CircularProgressBar.xaml
    /// </summary>
    public partial class CircularProgressBar : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(CircularProgressBar),
                new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                nameof(Maximum),
                typeof(double),
                typeof(CircularProgressBar),
                new PropertyMetadata(100.0, OnValueChanged));

        // Read-only Dependency Properties
        private static readonly DependencyPropertyKey ProgressAnglePropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(ProgressAngle),
                typeof(double),
                typeof(CircularProgressBar),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty ProgressAngleProperty = ProgressAnglePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey PercentageTextPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(PercentageText),
                typeof(string),
                typeof(CircularProgressBar),
                new PropertyMetadata("0%"));

        public static readonly DependencyProperty PercentageTextProperty = PercentageTextPropertyKey.DependencyProperty;

        // новые свойства зависимостей

        private static readonly DependencyProperty EndPointProperty =
            DependencyProperty.Register(
                nameof(EndPoint),
                typeof(Point),
                typeof(CircularProgressBar));

        private static readonly DependencyProperty IsLargeArcTrueFalseProperty =
            DependencyProperty.Register(
                nameof(IsLargeArcTrueFalse),
                typeof(string),
                typeof(CircularProgressBar));

        // 

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public double ProgressAngle
        {
            get => (double)GetValue(ProgressAngleProperty);
            private set => SetValue(ProgressAnglePropertyKey, value);
        }

        public Point EndPoint
        {
            get => (Point)GetValue(EndPointProperty);
            set => SetValue(EndPointProperty, value);
        }

        public string IsLargeArcTrueFalse
        {
            get => (string)GetValue(IsLargeArcTrueFalseProperty);
            set => SetValue(IsLargeArcTrueFalseProperty, value);
        }


        public string PercentageText
        {
            get => (string)GetValue(PercentageTextProperty);
            private set => SetValue(PercentageTextPropertyKey, value);
        }

        public CircularProgressBar()
        {
            InitializeComponent();
            UpdateProgress();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progressBar = (CircularProgressBar)d;
            progressBar.UpdateProgress();
        }

        private void UpdateProgress()
        {
            // Ограничиваем значения
            if (Value < 0) Value = 0;
            if (Value > Maximum) Value = Maximum;
            if (Maximum <= 0) Maximum = 100;

            // Вычисляем прогресс
            double percentage = Maximum == 0 ? 0 : (Value / Maximum);
            ProgressAngle = 360 * percentage;
            PercentageText = $"{(percentage * 100):F0}%";

            // Определяем дугу: большая или мальенькая
            IsLargeArcTrueFalse = Value > 50 ? "True" : "False";


            // Вычисляем новые координаты конечной точки дуги
            // Координаты исходной точки
            double x = 50, y = 95;

            // Координаты центра вращения
            double cx = 50, cy = 50;

            //// Угол поворота в градусах
            //double angleDegrees = 90;

            // Выполняем поворот
            double angleRad = ProgressAngle * Math.PI / 180.0;

            EndPoint = Value < 100 ? new Point(cx + (x - cx) * Math.Cos(angleRad) - (y - cy) * Math.Sin(angleRad),
                cy + (x - cx) * Math.Sin(angleRad) + (y - cy) * Math.Cos(angleRad))
                : new Point(cx + (x - cx) * Math.Cos(angleRad) - (y - cy) * Math.Sin(angleRad) + 0.001,
                cy + (x - cx) * Math.Sin(angleRad) + (y - cy) * Math.Cos(angleRad)) ;
        }
    }
}
