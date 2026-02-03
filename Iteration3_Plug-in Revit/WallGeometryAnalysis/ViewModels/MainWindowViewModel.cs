using Autodesk.Revit.DB;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WallGeometryAnalysis.Abstractions;
using WallGeometryAnalysis.Models;

namespace WallGeometryAnalysis.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private WallInfo _wallInfo;
        private double _thicknessLimit = 1000;
        private readonly ISelectionService _selectionService;
        private readonly IGeomertyService _geomertyService;

        public MainWindowViewModel(ISelectionService selectionService, IGeomertyService geomertyService)
        {

            CalcWall = new RelayCommand(OnCalcWallExecute);
            
            _selectionService = selectionService;
            _geomertyService = geomertyService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WallInfo WallInfo 
        {
            get => _wallInfo;
            set
            {
                _wallInfo = value;
                OnPropertyChanged();
            }
        }

        public double ThicknessLimit
        {
            get => _thicknessLimit;
            set
            {
                _thicknessLimit = value;
                OnPropertyChanged();
            }
        }
        public ICommand CalcWall { get; }

        private void OnCalcWallExecute(object parameter)
        {
            Wall wall = _selectionService.PickWall();
            if (wall == null)
            {
                return;
            }

            WallInfo = _geomertyService.GetWallInfo(wall, ThicknessLimit);

        }
    }
}
