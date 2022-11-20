using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using SelfService.Services;

namespace SelfService.Droid {
	[Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
	[IntentFilter(
	new[] { Intent.ActionView },
	Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
	DataSchemes = new[] { "com.googleusercontent.apps.806383440072-ppo06ni35et5gtksrabsq11a3mh89ek1" },
	DataPath = "/oauth2redirect")]
	public class CustomUrlSchemeInterceptorActivity : Activity {
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			var uri = new Uri(Intent.Data.ToString());
			AuthenticationState.Authenticator.OnPageLoading(uri);
			Finish();
		}
	}
}