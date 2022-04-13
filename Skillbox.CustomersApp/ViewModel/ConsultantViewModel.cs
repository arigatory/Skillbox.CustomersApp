using Skillbox.CustomersApp.Data;
using Skillbox.CustomersApp.Model;

namespace Skillbox.CustomersApp.ViewModel
{
    public class ConsultantViewModel : UserViewModel
    {
        public ConsultantViewModel(ICustomersDataProvider customersDataProvider, Consultant consultant) 
            : base(customersDataProvider, consultant)
        {
        }
    }
}
