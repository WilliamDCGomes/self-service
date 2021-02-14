using Newtonsoft.Json;
using SelfService.AppConstant;
using SelfService.Services;
using SelfService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Auth;
using Xamarin.Forms;
using SelfService.Views;

namespace SelfService.Droid.Masks {
    public class LoginGoogle {
		Account account;
		AccountStore store;

		public LoginGoogle() {
			store = AccountStore.Create();
		}

        public void TryLogin() {
			string clientId = null;
			string redirectUri = null;
			switch (Device.RuntimePlatform) {
				case Device.iOS:
					clientId = Constants.iOSClientId;
					redirectUri = Constants.iOSRedirectUrl;
					break;

				case Device.Android:
					clientId = Constants.AndroidClientId;
					redirectUri = Constants.AndroidRedirectUrl;
					break;
			}
			account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
			var authenticator = new OAuth2Authenticator(
				clientId,
				null,
				Constants.Scope,
				new Uri(Constants.AuthorizeUrl),
				new Uri(redirectUri),
				new Uri(Constants.AccessTokenUrl),
				null,
				true);
			authenticator.Completed += OnAuthCompleted;
			authenticator.Error += OnAuthError;
			AuthenticationState.Authenticator = authenticator;
			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);
		}

		private async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e) {
			var authenticator = sender as OAuth2Authenticator;
			if (authenticator != null) {
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}
			ModelUserGoogle user = null;
			if (e.IsAuthenticated) {
				var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
				var response = await request.GetResponseAsync();
				if (response != null) {
					string userJson = await response.GetResponseTextAsync();
					user = JsonConvert.DeserializeObject<ModelUserGoogle>(userJson);
				}
				if (user != null) {
					ModelUser modelUser = new ModelUser();
					modelUser.Name = user.GivenName;
					modelUser.Login = user.Name.Replace(" ", "");
					modelUser.Password = user.Id;
					modelUser.EmailAdress = user.Email;
					modelUser.LastName = user.FamilyName;
					ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
					if (dbUser.Locale(modelUser.Login)) {
						dbUser.Insert(modelUser);
					} else {
						modelUser.Id = dbUser.LocaleByLogin(modelUser.Login).Id;
					}
					App.Current.MainPage = new NavigationPage(new Home(modelUser.Id));
				}
			} 
			else {

            }
		}

		private void OnAuthError(object sender, AuthenticatorErrorEventArgs e) {
			var authenticator = sender as OAuth2Authenticator;
			if (authenticator != null) {
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}
			//App.Current.MainPage = new NavigationPage(new Login());
		}
	}
}