using Skillbox.CustomersApp.Data;

namespace Skillbox.CustomersApp.ViewModel
{
    public class ConsultantViewModel : UserViewModel
    {
        public ConsultantViewModel(ICustomersDataProvider customersDataProvider) : base(customersDataProvider)
        {
        }
    }
}
