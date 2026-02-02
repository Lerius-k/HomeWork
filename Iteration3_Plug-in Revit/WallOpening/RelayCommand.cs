using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WallOpening
{
    internal class RelayCommand : ICommand
    {
        /*всю чепухню с делегатами, подписками и проверками, что мы реалоизуем, при дется делать в каждой программе.
        поэтому есть фреймворки(например, "призм"), котоыре уже реализуют класс RelayCommand со всей инфраструктурой команды.*/ 
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged //событие для пересчета активности кнопки
        {
            //управление сигналом о подписке оставляем CommandManager(свой метод не пишем, как в ViewModel),
            //указываем, как ему подписать или отписать подписчика.
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /*создаем конструктор RelayCommand, который принимает два аргумента, делегатов, Action<> с результатом void для Execute
        и Func<> с возвращением значения для метода CanExecute*/
        // конструктор будет сохранять делегатов в свойства класса (корзинки) RelayCommand, а корзинки размещаем в методах этого класса.
        // делегаты, размещаясь в свойстве, которые заполнит констуктор, методу будут передвать действие, заключенное в них.
        // это нужно, чтобы не писать кучу команд для разных дейсвий. 
        // будет одна команда RelayCommand, которая за счет делегатов умеет реализовывать разные действия.
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute)); //нужна проверка на null в действии. Если действие == null, вызвать исключиние.
            _canExecute = canExecute; //тут null не страшен, кнопка останется активной
        }

        //да/нет для активности кнопки
        public bool CanExecute(object parameter) /*синтаксический сахар:
        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true; */
        {
            if (_canExecute == null)
            {
                return true;
            }
            else
            {
                return _canExecute(parameter);
            }
        }

        //само действие
        public void Execute(object parameter) /*синтаксический сахар:
        public void Execute(object parameter) => _execute(parameter); */
        {
            _execute(parameter);
        }
    }
}
