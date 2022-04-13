﻿using Skillbox.CustomersApp.Command;
using System.Threading.Tasks;
using System.Windows;

namespace Skillbox.CustomersApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private ViewModelBase? _selectedViewModel;

        public MainViewModel(ManagerViewModel customersViewModel, ConsultantViewModel userSelectionViewModel)
        {
            CustomersViewModel = customersViewModel;
            UserSelectionViewModel = userSelectionViewModel;
            SelectedViewModel = CustomersViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
            CloseWindowCommand = new DelegateCommand(CloseWindow);
        }

        public ViewModelBase? SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }
        public ManagerViewModel CustomersViewModel { get; }
        public ConsultantViewModel UserSelectionViewModel { get; }
        public DelegateCommand SelectViewModelCommand { get; }
        public DelegateCommand CloseWindowCommand { get; }

        public async override Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
        private void CloseWindow(object? window)
        {
            if (window is not null)
            {
                ((Window)window).Close();
            }
        }
    }
}
