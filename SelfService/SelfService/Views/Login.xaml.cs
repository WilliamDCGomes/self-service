using JobSearch.App.Utility.Load;
using Rg.Plugins.Popup.Extensions;
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

        private async void EnterMenu(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(LoginInput.Text) || String.IsNullOrEmpty(PasswordEntry.Text)) {
                await DisplayAlert("ERRO", "PREENCHA O CAMPO DO LOGIN E DA SENHA PARA LOGAR", "OK");
            } else {
                await Navigation.PushPopupAsync(new Loading());
                ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
                if (dbUser.checkUserExist(LoginInput.Text, PasswordEntry.Text)) {
                    App.Current.MainPage = new NavigationPage(new Home(dbUser.IdUser));
                } else {
                    await DisplayAlert("ERRO", "LOGIN OU SENHA INCORRETO, REVISE OS CAMPOS", "OK");
                }
                await Navigation.PopAllPopupAsync();
            }
        }
    }
}