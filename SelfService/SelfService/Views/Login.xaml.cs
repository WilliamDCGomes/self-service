using JobSearch.App.Utility.Load;
using Rg.Plugins.Popup.Extensions;
using SelfService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using SelfService.Models;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage {
        bool tried = false;
        bool fromLogout = false;
        ModelUser modelUser = null;
        ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
        public Login() {
            InitializeComponent();
            modelUser = dbUser.GetUser();
            if (modelUser != null) {
                LoginInput.Text = modelUser.Login;
                PasswordEntry.Text = modelUser.Password;
                loginFingerPrint();
            }
        }

        public Login(bool validation) {
            InitializeComponent();
            fromLogout = validation;
        }

        private async void loginFingerPrint() {
            if (!tried) {
                var availability = await CrossFingerprint.Current.IsAvailableAsync(true);
                if (availability) {
                    var authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Autentificação", "Posicione o dedo no leitor de digitais para fazer o login"));
                    if (authResult.Authenticated) {
                        await Navigation.PushPopupAsync(new Loading());
                        ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
                        App.Current.MainPage = new NavigationPage(new Home(modelUser.Id));
                        await Navigation.PopAllPopupAsync();
                    }
                }
                tried = true;
            }
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
                await DisplayAlert("ERRO", "Preencha o campo do login e da senha para logar", "OK");
            } else {
                await Navigation.PushPopupAsync(new Loading());
                ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
                if (dbUser.checkUserExist(LoginInput.Text, PasswordEntry.Text)) {
                    App.Current.MainPage = new NavigationPage(new Home(dbUser.IdUser));
                } else {
                    await DisplayAlert("ERRO", "Login ou Senha incorreto, Revise os campos", "OK");
                }
                await Navigation.PopAllPopupAsync();
            }
        }

        private void TryLoginBiometric(object sender, FocusEventArgs e) {
            if (fromLogout) {
                loginFingerPrint();
            }
        }
    }
}