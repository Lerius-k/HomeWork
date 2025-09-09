using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task10.Models;

namespace Task10.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
                UpdateStatusMessage(); // Обновляем статус при изменении
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
                UpdateStatusMessage(); // Обновляем статус при изменении
            }
        }

        private string statusMessage;
        public string StatusMessage
        {
            get => statusMessage;
            set
            {
                if (IsSuccess)
                {
                    statusMessage = "Успешный вход! Добро пожаловать!";

                }
                else
                {
                    statusMessage = "Введите учетные данные";
                }
                OnPropertyChanged();
            }
        }

        private bool isSuccess;
        public bool IsSuccess
        {
            get => isSuccess;
            set
            {
                isSuccess = value;
                OnPropertyChanged();
                UpdateStatusMessage(); // Обновляем статус при изменении
            }
        }
        private void UpdateStatusMessage()
        {
            if (IsSuccess)
            {
                StatusMessage = "Успешный вход! Добро пожаловать!";
            }
            else if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                StatusMessage = "Введите учетные данные";
            }
            else
            {
                StatusMessage = "Неверные учетные данные";
            }
        }

        public ICommand LoginCommand { get; }

        private void OnLoginCommandExecute(object? parameter)
        {
            IsSuccess = AuthModel.Authenticate(Username, Password);
        }

        private bool CanLoginCommandExecuted(object? parameter)
        {
            return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Username);
        }
        
        public MainWindowViewModel()
        {
            StatusMessage = "Введите учетные данные"; // Начальное сообщение
            LoginCommand = new RelayCommand(OnLoginCommandExecute, CanLoginCommandExecuted);
        }
    }
}
