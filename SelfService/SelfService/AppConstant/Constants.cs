using System;
namespace SelfService.AppConstant
{
    public class Constants
    {
		public static string AppName = "OAuthNativeFlow";

		public static string iOSClientId = "";
		public static string AndroidClientId = "806383440072-ppo06ni35et5gtksrabsq11a3mh89ek1.apps.googleusercontent.com";

		public static string Scope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/plus.login";
		public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
		public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
		public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

		public static string iOSRedirectUrl = "";
		public static string AndroidRedirectUrl = "com.googleusercontent.apps.806383440072-ppo06ni35et5gtksrabsq11a3mh89ek1:/oauth2redirect";
	}
}
