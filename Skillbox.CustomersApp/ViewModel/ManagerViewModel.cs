using Skillbox.CustomersApp.Data;
using Skillbox.CustomersApp.Model;

namespace Skillbox.CustomersApp.ViewModel
{

    public class ManagerViewModel : UserViewModel
    {
        public ManagerViewModel(ICustomersDataProvider customersDataProvider, Manager manager) 
            : base(customersDataProvider, manager)
        {
        }
    }
}
