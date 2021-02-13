using JobSearch.App.Utility.Load;
using Rg.Plugins.Popup.Extensions;
using SelfService.Services;
using System;
using System.Collections.Generic;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelfService.Models;
using Xamarin.Essentials;
using Xamarin.Auth;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage {
        bool tried = false;
        ModelUser modelUser = null;
        string logintemp = null;
        ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
        public Login() {
            InitializeComponent();
            List<ModelUser> userSystem = dbUser.ListUsers();
            if (userSystem.Count > 0) {
                modelUser = dbUser.GetUser();
                if (modelUser != null) {
                    LoginInput.Text = modelUser.Login;
                    logintemp = modelUser.Login;
                    loginFingerPrint(modelUser.Id);
                }
            }
        }

        public Login(bool validation) {
            InitializeComponent();
            List<ModelUser> userSystem = dbUser.ListUsers();
            if (userSystem.Count > 0) {
                modelUser = dbUser.GetUser();
                if (modelUser != null) {
                    LoginInput.Text = modelUser.Login;
                    logintemp = modelUser.Login;
                }
            }
        }

        private async void loginFingerPrint(long id) {
            if (!tried) {
                var availability = await CrossFingerprint.Current.IsAvailableAsync(true);
                if (availability) {
                    var authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Autentificação", "Posicione o dedo no leitor de digitais para fazer o login"));
                    if (authResult.Authenticated) {
                        await Navigation.PushPopupAsync(new Loading());
                        ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
                        App.Current.MainPage = new NavigationPage(new Home(id));
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
                await DisplayAlert("Erro", "Preencha o campo do login e da senha para logar", "OK");
            } else {
                await Navigation.PushPopupAsync(new Loading());
                ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
                if (dbUser.checkUserExist(LoginInput.Text, PasswordEntry.Text)) {
                    App.Current.MainPage = new NavigationPage(new Home(dbUser.IdUser));
                } else {
                    await DisplayAlert("Erro", "Login ou Senha incorreto, Revise os campos", "OK");
                }
                await Navigation.PopAllPopupAsync();
            }
        }

        private void TryLoginBiometric(object sender, FocusEventArgs e) {
            if (LoginInput.Text.Equals(logintemp)) {
                if (modelUser != null) {
                    loginFingerPrint(modelUser.Id);
                }
            }
        }

        private void SingInWithGoogle(object sender, EventArgs e) {

        }

        private async void SingInWithFacebook(object sender, EventArgs e) {
            App.Current.MainPage = new NavigationPage(new Views.LoginFacebook());
        }
    }
}