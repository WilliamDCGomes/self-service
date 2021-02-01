using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage {
        public Register() {
            InitializeComponent();
        }
        private void BackLogin(object sender, EventArgs e) {
            App.Current.MainPage = new NavigationPage(new Login());
        }

        private void ShowAndHidePassword(object sender, EventArgs e) {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            if (PasswordEntry.IsPassword) {
                ImagePasswordHideShow.Source = "PasswordTrue";
            } else {
                ImagePasswordHideShow.Source = "PasswordFalse";
            }
        }

        private void ShowAndHidePassword2(object sender, EventArgs e) {
            RepetPasswordEntry.IsPassword = !RepetPasswordEntry.IsPassword;
            if (RepetPasswordEntry.IsPassword) {
                ImagePasswordHideShow2.Source = "PasswordTrue";
            } else {
                ImagePasswordHideShow2.Source = "PasswordFalse";
            }
        }

    }
}