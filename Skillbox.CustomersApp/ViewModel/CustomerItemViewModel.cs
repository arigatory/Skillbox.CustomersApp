using Skillbox.CustomersApp.Model;

namespace Skillbox.CustomersApp.ViewModel
{
    public class CustomerItemViewModel : ValidationViewModelBase
    {
        private readonly Customer _model;

        public CustomerItemViewModel(Customer model)
        {
            _model = model;
        }

        public int Id => _model.Id;

        public string? FirstName
        {
            get { return _model.FirstName; }
            set
            {
                _model.FirstName = value;
                RaisePropertyChanged();
            }
        }

        public string? MiddleName
        {
            get { return _model.MiddleName; }
            set
            {
                _model.MiddleName = value;
                RaisePropertyChanged();
            }
        }

        public string? LastName
        {
            get { return _model.LastName; }
            set
            {
                _model.LastName = value;
                RaisePropertyChanged();
                if (string.IsNullOrEmpty(_model.LastName))
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
            get { return _model.PhoneNumber; }
            set
            {
                _model.PhoneNumber = value;
                RaisePropertyChanged();
            }
        }

        public string? PassportNumber
        {
            get { return _model.PassportNumber; }
            set
            {
                _model.PassportNumber = value;
                RaisePropertyChanged();
            }
        }
    }
}
