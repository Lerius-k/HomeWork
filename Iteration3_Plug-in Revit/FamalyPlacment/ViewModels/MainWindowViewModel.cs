using Autodesk.Revit.UI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FamalyPlacment.Abstractions;
using FamalyPlacment.Models;
using System.Collections.ObjectModel;

namespace FamalyPlacment.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly IPlacementService _placementService;
        private PlantType _selectedPlantType;
        private int _count;
        private string _statusMessage;

        public MainWindowViewModel(IPlacementService placementService)
        {
            PlaceCommand = new RelayCommand(PlaceFurniture);

            PlantTypes = new ObservableCollection<PlantType>
            {
                PlantType.Tree,
                PlantType.Bush,
                PlantType.Flower
            };

            _placementService = placementService;
        }

        public ObservableCollection<PlantType> PlantTypes { get; }

        public PlantType SelectedPlantType
        {
            get => _selectedPlantType;
            set => SetProperty(ref _selectedPlantType, value);
        }

        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public RelayCommand PlaceCommand { get; }

        private void PlaceFurniture()
        {
            CSharpFunctionalExtensions.Result result = _placementService.Place(SelectedPlantType, Count);
            if (result.IsSuccess)
            {
                StatusMessage = $"Размещено {Count} экземпляров растений";
                TaskDialog.Show("Размещение растений", StatusMessage);
            }
            else
            {
                StatusMessage = $"Ошибка {result.Error}";
                TaskDialog.Show("Размещение растений", result.Error);
            }
        }
    }
}
