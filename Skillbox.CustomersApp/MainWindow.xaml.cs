using Skillbox.CustomersApp.Data;
using Skillbox.CustomersApp.ViewModel;
using System.Windows;

namespace Skillbox.CustomersApp
{
    /// <summary>
    /// Главное (и единственное) окно
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        /// <summary>
        /// При загрузке окна, загружаем ViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
