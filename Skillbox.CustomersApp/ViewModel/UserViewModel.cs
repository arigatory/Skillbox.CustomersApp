using Skillbox.CustomersApp.Command;
using Skillbox.CustomersApp.Data;
using Skillbox.CustomersApp.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private readonly ICustomersDataProvider _customersDataProvider;


        private CustomerItemViewModel? _selectedCustomer;

        public UserViewModel(ICustomersDataProvider customersDataProvider)
        {
            _customersDataProvider = customersDataProvider;
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


        private bool CanSaveAll(object? arg)
        {
            return Customers.All(c => ! c.HasErrors);
        }

        private async void SaveAll(object? obj)
        {
            await _customersDataProvider.SaveAllAsync(Customers.Select(c=>c.Model).ToArray());
        }
    }
}
