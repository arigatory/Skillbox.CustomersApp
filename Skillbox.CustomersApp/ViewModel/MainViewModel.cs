using Skillbox.CustomersApp.Command;
using System.Threading.Tasks;
using System.Windows;

namespace Skillbox.CustomersApp.ViewModel
{
    /// <summary>
    /// ViewModel основного окна
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// ViewModel которая будет подставляться для менеджера или для консультанта в зависимости от того,
        /// кто работает в система. Не стал ограничивать UserViewModel, чтобы оставить возможность работы в этом же
        /// окне, но с другой задачей, например с настройками приложения, поэтому ViewModelBase
        /// </summary>
        private ViewModelBase? _selectedViewModel;

        /// <summary>
        /// Конструктор для создания ViewModel основного окна
        /// </summary>
        /// <param name="customersViewModel">ViewModel менеджера на случай работы в системе менеджера</param>
        /// <param name="userSelectionViewModel">ViewModel клиента на случай работы в системе консультанта</param>
        public MainViewModel(ManagerViewModel customersViewModel, ConsultantViewModel userSelectionViewModel)
        {
            CustomersViewModel = customersViewModel;
            ConsultantViewModel = userSelectionViewModel;
            SelectedViewModel = CustomersViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
            CloseWindowCommand = new DelegateCommand(CloseWindow);
        }

        /// <summary>
        /// Выбранная в настоящий момент ViewModel. ManagerViewModel или ConsultantViewModel, 
        /// но при дальнейшем развитии приложения может быть любая.
        /// </summary>
        public ViewModelBase? SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }
        public ManagerViewModel ManagerViewModel { get; }
        public ConsultantViewModel ConsultantViewModel { get; }
        
        /// <summary>
        /// Команда для переключения между режимом Менеджера и Консультанта
        /// </summary>
        public DelegateCommand SelectViewModelCommand { get; }
        
        /// <summary>
        /// Команда для выхода из приложения
        /// </summary>
        public DelegateCommand CloseWindowCommand { get; }

        /// <summary>
        /// Вызывается при загрузке окна. Переопределям, чтобы при запуске загрузить также ViewModel для выбранного режима работы.
        /// По умолчанию режим работы для менеджера.
        /// </summary>
        /// <returns></returns>
        public async override Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        /// <summary>
        /// Метод, который вызывается при выполнении команды выбора режима работы приложения: консультант или менеджер.
        /// </summary>
        /// <param name="parameter">Нужен, чтобы знать какую ViewModel мы будем использовать
        /// при переключении режима.</param>
        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
        
        /// <summary>
        /// Метод, который вызывается при выполнении команды закрытия окна. У нас одно окно, поэтому данный вызов
        /// завершает приложение.
        /// </summary>
        /// <param name="window">Окно, которое нужно закрыть</param>
        private void CloseWindow(object? window)
        {
            if (window is not null)
            {
                ((Window)window).Close();
            }
        }
    }
}
