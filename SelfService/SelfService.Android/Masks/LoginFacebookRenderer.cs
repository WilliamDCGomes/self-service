using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Newtonsoft.Json;
using Xamarin.Auth;
using SelfService.Views;
using SelfService.Droid.Masks;
using System.Json;
using Newtonsoft.Json.Linq;
using SelfService.Models;
using SelfService.Services;

[assembly:ExportRenderer(typeof(LoginFacebook), typeof(LoginFacebookRenderer))]
namespace SelfService.Droid.Masks {
    public class LoginFacebookRenderer : PageRenderer{
        public LoginFacebookRenderer() {
            var oauth = new OAuth2Authenticator(
                clientId: "841136649785658",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html")
                );
            oauth.Completed += async (sender, args) => {
                if (args.IsAuthenticated) {
                    var token = args.Account.Properties["access_token"].ToString();
                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=id,name,first_name,last_name,email,birthday,hometown"), null, args.Account);
                    var answerd = await request.GetResponseAsync();
                    dynamic fbUser = JsonConvert.DeserializeObject(answerd.GetResponseText());
                    ModelUser user = new ModelUser();
                    user.Name = fbUser.first_name.ToString().Replace("\"", "");
                    user.Login = fbUser.name.ToString().Replace("\"", "");
                    user.Login = user.Login.Replace(" ", "");
                    user.Password = fbUser.id.ToString().Replace("\"", "");
                    user.EmailAdress = user.Login + "@facebook.com";
                    if (fbUser.last_name != null) {
                        user.LastName = fbUser.last_name.ToString().Replace("\"", "");
                    }
                    if (fbUser.email != null) {
                        user.EmailAdress = fbUser.email.ToString().Replace("\"", "");
                    }
                    if (fbUser.birthday != null) {
                        string[] date = fbUser.birthday.ToString().Replace("\"", "").Split('/');
                        if(date.Length == 1) {
                            user.BirthDay = date[0];
                        }
                        else if (date.Length == 2) {
                            user.BirthDay = date[1] + "/" + date[0];
                        }
                        else if (date.Length == 3) {
                            user.BirthDay = date[1] + "/" + date[0] + "/" + date[2];
                        }
                    }
                    if (fbUser.hometown != null) {
                        user.City = fbUser.hometown.ToString().Replace("\"", "");
                    }
                    ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
                    if (dbUser.Locale(user.Login)) {
                        dbUser.Insert(user);
                    } 
                    else {
                        user.Id = dbUser.LocaleByLogin(user.Login).Id;
                    }
                    App.Current.MainPage = new NavigationPage(new Home(user.Id));
                }
            };
            var activity = this.Context as Activity;
            activity.StartActivity(oauth.GetUI(activity));
        }
    }
}