using JobSearch.App.Utility.Load;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Rg.Plugins.Popup.Extensions;
using SelfService.Droid.Masks;
using SelfService.Models;
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
    public partial class LoginPage : ContentPage {
        bool tried = false;
        ModelUser modelUser = null;
        ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
        public LoginPage() {
            InitializeComponent();
        }

        public LoginPage(bool firstAccess) {
            InitializeComponent();
            List<ModelUser> userSystem = dbUser.ListUsers();
            if (userSystem.Count > 0) {
                modelUser = dbUser.GetUser();
                if (modelUser != null && firstAccess) {
                    loginFingerPrint(modelUser.Id);
                }
            }
        }

        private async void loginFingerPrint(long id) {
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

        private async void SingInWithGoogle(object sender, EventArgs e) {
            await frameGoogle.FadeTo(0.5, 100, Easing.BounceIn);
            await frameGoogle.FadeTo(1, 100, Easing.BounceOut);
            LoginGoogle loginGoogle = new LoginGoogle();
            loginGoogle.TryLogin();
        }

        private async void SingInWithFacebook(object sender, EventArgs e) {
            await frameFacebook.FadeTo(0.5, 100, Easing.BounceIn);
            await frameFacebook.FadeTo(1, 100, Easing.BounceOut);
            App.Current.MainPage = new NavigationPage(new Views.LoginFacebook());
        }

        private async void SingInWithTwitter(object sender, EventArgs e) {
            await frameTwitter.FadeTo(0.5, 100, Easing.BounceIn);
            await frameTwitter.FadeTo(1, 100, Easing.BounceOut);
        }

        private void NewUser(object sender, EventArgs e) {
            Navigation.PushAsync(new Register());
        }

        private void EnterLogin(object sender, EventArgs e) {
            Navigation.PushAsync(new Login(tried));
        }
    }
}