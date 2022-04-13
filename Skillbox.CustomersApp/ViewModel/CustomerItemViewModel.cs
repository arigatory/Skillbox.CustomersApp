using Skillbox.CustomersApp.Model;
using System;
using System.Text.RegularExpressions;

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
                if (string.IsNullOrEmpty(Model.PhoneNumber))
                {
                    AddError("Телефон обязателен");
                }
                else if (!Regex.Match(Model.PhoneNumber, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$").Success)
                {
                    AddError("Не похоже не номер телефона");
                }
                else
                {
                    ClearErrors();
                }
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


        public string? EditedBy
        {
            get { return Model.EditedBy; }
            set
            {
                Model.EditedBy = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Info));
            }
        }


        public string Info
        {
            get
            {
                if (string.IsNullOrEmpty(Model.EditedBy))
                {
                    return string.Empty;
                }
                var lastEdited = "неизвестно когда";
                if (Model.LastEdited.HasValue)
                {
                    lastEdited = Model.LastEdited.Value.ToString();
                }
                return $"Изменил {Model.EditedBy} {lastEdited}";
            }
        }


        private bool _haveChanges = false;

        public bool HaveChanges
        {
            get { return _haveChanges; }
            set
            {
                _haveChanges = value;
                RaisePropertyChanged();
            }
        }
    }
}
