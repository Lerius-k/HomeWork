using Autodesk.Revit.DB;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WallOpening.Abstractions;
using WallOpening.Models;

namespace WallOpening.ViewModels
{
    /// <summary>
    /// Модель представления
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ISelectionService _selectionService;
        private readonly IGeomertyService _geomertyService;

        /*конструктор класса ViewModel, который помещает в свойсвто CalcOpening команду OnCalcOpeningExecute, 
обернутую в инфраструктуру RelayCommand*/
        public MainWindowViewModel(ISelectionService selectionService, IGeomertyService geomertyService)
        {

            CalcOpening = new RelayCommand(OnCalcOpeningExecute); /*почему он не просит у нас второй аргумент? 
                                                                    потому что в конструкторе RelayCommand воторому аргументу задали по-умолчанию = null*/
            _selectionService = selectionService;
            _geomertyService = geomertyService;
        }

        public event PropertyChangedEventHandler PropertyChanged; //событие - изменение свойства

        //создаем метод, который будет сигналить собитию об изменениях в свойствах, чтобы все подписчики события о них знали
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //аргументу ставим null, и предворяем [Имя вызывающего абонента], 
        //чтобы не приходилось каждый раз в метод подставалять имя свойства. 
        //теперь метод можно использовать с пустыми скобками OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this/*текщий объект события - экземпл класса MainWindowViewModel*/
                , new PropertyChangedEventArgs(propertyName)/*аргумент события*/)
                ;//вопросик проверяет, есть ли подписчики на событие, чтобы не вызвалось исключение на методе Invoke
        }

        //свойства видовой модели
        //приватное свойство (президент)
        private OpeningInfo _openingInfo; //приватные свойства предворяются нижним подчеркиванием

        //свойство -- подручный президента 
        public OpeningInfo OpeningInfo //свойства пишутся с большой буквы.
        {
            get { return _openingInfo; } //синтаксически сахар: get => _openingInfo;
            set
            {
                _openingInfo = value;
                OnPropertyChanged();//автоматически подставляется "OpeningInfo"
            }
        }

        private string _statusMessage = "Выберите проем";
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        private double _limit = 1000;

        public double Limit
        {
            get => _limit;
            set
            {
                _limit = value;
                OnPropertyChanged();
            }
        }

        //создаем свойство типа интрефеса, заключающее в себе команду, описанную методом ниже
        public ICommand CalcOpening { get; } //только на чтение

        //метод, заключающий в себе действия команды
        private void OnCalcOpeningExecute(object parameter)
        {
            FamilyInstance opening = _selectionService.PickOpening(); //соответсвенно он имеет метод для вбора и возвращает экземпляр семейтсва
            if (opening == null)
            {
                return;
            }

            OpeningInfo = _geomertyService.GetOpeningInfo(opening, Limit); //будет иметь метод "взять инфомрацию из геометрии". и будет проверять допуск по лимиту

            StatusMessage = $"Обработан {OpeningInfo.Name}";
        }



    }
}
