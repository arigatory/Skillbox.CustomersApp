using Skillbox.CustomersApp.Model;

namespace Skillbox.CustomersApp.ViewModel
{
    public class CustomerItemViewModel : ValidationViewModelBase
    {
        public Customer Model { get; private set; }

        public CustomerItemViewModel(Customer model)
        {
            Model = model;
        }

        public int Id => Model.Id;

        public string? FirstName
        {
            get { return Model.FirstName; }
            set
            {
                Model.FirstName = value;
                RaisePropertyChanged();
            }
        }

        public string? MiddleName
        {
            get { return Model.MiddleName; }
            set
            {
                Model.MiddleName = value;
                RaisePropertyChanged();
            }
        }

        public string? LastName
        {
            get { return Model.LastName; }
            set
            {
                Model.LastName = value;
                RaisePropertyChanged();
                if (string.IsNullOrEmpty(Model.LastName))
                {
                    AddError("Фамилия обязательна");
                }
                else
                {
                    ClearErrors();
                }
            }
        }

        public string? PhoneNumber
        {
            get { return Model.PhoneNumber; }
            set
            {
                Model.PhoneNumber = value;
                RaisePropertyChanged();
            }
        }

        public string? PassportNumber
        {
            get { return Model.PassportNumber; }
            set
            {
                Model.PassportNumber = value;
                RaisePropertyChanged();
            }
        }
    }
}
