using System;
using System.Windows;
using System.Windows.Input;
using Lab_01_Romanenko.ViewModels;

namespace Lab_01_Romanenko.Tools
{
    public class DateUpdateCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public DateUpdateCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public void Execute(object parameter)
        {
            _mainWindowViewModel.UpdateDisplayedValues();
        }

        public bool CanExecute(object parameter)
        {
            return _mainWindowViewModel.CanUpdate();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}