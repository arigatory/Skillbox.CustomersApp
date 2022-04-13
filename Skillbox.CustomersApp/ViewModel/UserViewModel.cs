using Skillbox.CustomersApp.Command;
using Skillbox.CustomersApp.Data;
using Skillbox.CustomersApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.ViewModel
{
    public class UserViewModel : ValidationViewModelBase
    {
        private readonly ICustomersDataProvider _customersDataProvider;
        private readonly User _user;
        private CustomerItemViewModel? _selectedCustomer;

        public UserViewModel(ICustomersDataProvider customersDataProvider, User user)
        {
            _customersDataProvider = customersDataProvider;
            _user = user;
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            SaveAllCommand = new DelegateCommand(SaveAll, CanSaveAll);

        }

        public bool IsCustomerSelected => SelectedCustomer is not null;
        public bool IsCustomerNotSelected => SelectedCustomer is null;

        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();

        public CustomerItemViewModel? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsCustomerSelected));
                RaisePropertyChanged(nameof(IsCustomerNotSelected));
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand SaveAllCommand { get; }


        public override async Task LoadAsync()
        {
            Customers.Clear();
            SelectedCustomer = null;
            var customers = await _customersDataProvider.GetAllAsync();
            if (customers is not null)
            {
                foreach (var customer in customers)
                {
                    var customerVM = new CustomerItemViewModel(customer);
                    customerVM.PropertyChanged += (object? sender, PropertyChangedEventArgs e)
                        => SaveAllCommand.RaiseCanExecuteChanged();
                    Customers.Add(customerVM);
                }
            }
        }


        private void Add(object? parameter)
        {
            var customer = new Customer { LastName = "Новый клиент" };
            var viewModel = new CustomerItemViewModel(customer);
            viewModel.PropertyChanged += (object? sender, PropertyChangedEventArgs e)
                => SaveAllCommand.RaiseCanExecuteChanged();
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
            SelectedCustomer.PhoneNumber = "";
            SelectedCustomer.LastName = "";
            DeleteCommand.RaiseCanExecuteChanged();
        }

        private void Delete(object? parameter)
        {
            if (SelectedCustomer is not null)
            {
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
                SaveAllCommand.RaiseCanExecuteChanged();
                SaveAll(parameter);
            }
        }

        private bool CanDelete(object? parameter) => SelectedCustomer is not null;

        private bool CanSaveAll(object? arg)
        {
            return Customers.All(c => !c.HasErrors);
        }

        private async void SaveAll(object? obj)
        {
            foreach (var customer in Customers)
            {
                customer.Model.LastEdited = DateTime.Now;
                customer.EditedBy = _user.GetTitle();
            }
            await _customersDataProvider.SaveAllAsync(Customers.Select(c => c.Model).ToArray());
        }
    }
}
