using Skillbox.CustomersApp.Data;

namespace Skillbox.CustomersApp.ViewModel
{

    public class ManagerViewModel : UserViewModel
    {
        public ManagerViewModel(ICustomersDataProvider customersDataProvider) : base(customersDataProvider)
        {
        }
    }
}
