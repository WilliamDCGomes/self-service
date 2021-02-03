using SelfService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage {
        public Login() {
            InitializeComponent();
        }

        private void DoForgetPassword(object sender, EventArgs e) {
            Navigation.PushModalAsync(new ForgotPassword());
        }

        private void ShowAndHidePassword(object sender, EventArgs e) {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            if (PasswordEntry.IsPassword) {
                ImagePasswordHideShow.Source = "PasswordTrue";
            } else {
                ImagePasswordHideShow.Source = "PasswordFalse";
            }
        }

        private void NewUser(object sender, EventArgs e) {
            Navigation.PushAsync(new Register());
        }

        private void EnterMenu(object sender, EventArgs e) {
            ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
            if (dbUser.checkUserExist(LoginInput.Text, PasswordEntry.Text)) {
                App.Current.MainPage = new NavigationPage(new Home(dbUser.IdUser));
            } else {
                DisplayAlert("ERRO", "LOGIN OU SENHA INCORRETO, REVISE OS CAMPOS", "OK");
            }
        }
    }
}