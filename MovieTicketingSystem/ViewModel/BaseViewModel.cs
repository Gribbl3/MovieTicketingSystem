using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieTicketingSystem.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        //validation
        protected bool IsEmptyOrNull(string value, string fieldName)
        {
            if (string.IsNullOrEmpty(value))
            {
                Shell.Current.DisplayAlert("Error", $"{fieldName} cannot be empty", "OK");
                return true;
            }
            return false;
        }

        protected bool IsInvalidEmail(string value)
        {
            if (!value.Contains("@"))
            {
                Shell.Current.DisplayAlert("Error", "Invalid email", "OK");
                return true;
            }
            return false;
        }

        protected bool IsInvalidBirthdate(DateTime birthdate)
        {
            if (birthdate >= DateTime.Now)
            {
                Shell.Current.DisplayAlert("Register", "Birthdate must not be today or succeeding dates", "OK");
                return true;
            }
            return false;
        }
    }
}
