

using MovieTicketingSystem.Model;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel
{
    public class AdminLoginViewModel : BaseViewModel
    {
        public Admin Admin { get; set; } = new Admin();
       // public ICommand LoginCommand => new Command(Login);

        private string _username;
        private string _password;


        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }


    }
}
