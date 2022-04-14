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
    /// <summary>
    /// Класс реализующий основную логику приложения. У нас она одинаковая для Консультанта и Менеджера, 
    /// поэтому это базовый класс длы ManagerViewModel и ConsultantViewModel
    /// Основные различия описаны в классах View, задаваемых языком разметки XAML, поскольку этого достаточно для решения
    /// бизнес-задачи.
    /// </summary>
    public class UserViewModel : ValidationViewModelBase
    {
        private readonly ICustomersDataProvider _customersDataProvider;
        private readonly User _user;
        private CustomerItemViewModel? _selectedCustomer;

        /// <summary>
        /// Констрктор UserViewModel
        /// </summary>
        /// <param name="customersDataProvider">Поставщик данных</param>
        /// <param name="user">Пользователь, который работает в системе (будет подставлен либо Manager, либо Consultant)</param>
        public UserViewModel(ICustomersDataProvider customersDataProvider, User user)
        {
            _customersDataProvider = customersDataProvider;
            _user = user;
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            SaveAllCommand = new DelegateCommand(SaveAll, CanSaveAll);

        }

        /// <summary>
        /// Свойство для проверки, выбран ли клиент
        /// </summary>
        public bool IsCustomerSelected => SelectedCustomer is not null;

        /// <summary>
        /// Можно было бы добавить конвертер, но решил, что со свойством будет лаконичнее. Нужен, чтобы показывать/скрывать
        /// приглашение пользователю для выбора клиента при начале работы.
        /// </summary>
        public bool IsCustomerNotSelected => SelectedCustomer is null;

        /// <summary>
        /// Все клиенты, которые будут отображаться
        /// </summary>
        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();

        /// <summary>
        /// Выбранный клиент
        /// </summary>
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

        /// <summary>
        /// Команда добавления нового клиента
        /// </summary>
        public DelegateCommand AddCommand { get; }

        /// <summary>
        /// Команда удаления выбранного клиента
        /// </summary>
        public DelegateCommand DeleteCommand { get; }

        /// <summary>
        /// Команда сохранения всех клиентов
        /// </summary>
        public DelegateCommand SaveAllCommand { get; }

        /// <summary>
        /// Загружаем все, что нужно для работы при отображении View. 
        /// Клиенты будут "конвертированы" в CustomerItemViewModel, чтобы пользователь мог видеть изменения на лету.
        /// </summary>
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

        /// <summary>
        /// Метод вызывается при выполнении команды ДобавитьКлиента
        /// </summary>
        /// <param name="parameter">не используется, но требуется сигнатурой контракта</param>
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

        /// <summary>
        /// Метод вызывается при выполнении команды УдалитьКлиента
        /// </summary>
        /// <param name="parameter">не используется, но требуется сигнатурой контракта</param>
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

        /// <summary>
        /// Метод вызывается при проверке возможности выполнения команды УдалитьКлиента
        /// </summary>
        /// <param name="parameter">не используется, но требуется сигнатурой контракта</param>
        private bool CanDelete(object? parameter) => SelectedCustomer is not null;

        /// <summary>
        /// Метод вызывается при проверке возможности выполнения команды СохранитьВсе
        /// </summary>
        /// <param name="parameter">не используется, но требуется сигнатурой контракта</param>
        private bool CanSaveAll(object? parameter)
        {
            return Customers.All(c => !c.HasErrors);
        }

        /// <summary>
        /// Метод вызывается при выполнении команды СохранитьВсе
        /// </summary>
        /// <param name="parameter">не используется, но требуется сигнатурой контракта</param>
        private async void SaveAll(object? parameter)
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
