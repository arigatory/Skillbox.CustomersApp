using Skillbox.CustomersApp.Command;
using Skillbox.CustomersApp.Data;
using Skillbox.CustomersApp.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.ViewModel
{

    public class CustomersViewModel : ViewModelBase
    {
        private readonly ICustomersDataProvider _customersDataProvider;


        private CustomerItemViewModel? _selectedCustomer;

        public CustomersViewModel(ICustomersDataProvider customersDataProvider)
        {
            _customersDataProvider = customersDataProvider;
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
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

        public override async Task LoadAsync()
        {
            if (Customers.Any())
            {
                return;
            }

            var customers = await _customersDataProvider.GetAllAsync();
            if (customers is not null)
            {
                foreach (var customer in customers)
                {
                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }
        }

        private void Add(object? parameter)
        {
            var customer = new Customer { LastName = "Новый клиент" };
            var viewModel = new CustomerItemViewModel(customer);
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }

        private void Delete(object? parameter)
        {
            if (SelectedCustomer is not null)
            {
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }

        private bool CanDelete(object? parameter) => SelectedCustomer is not null;
    }
}
