using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : ContentPage {
        public ForgotPassword() {
            InitializeComponent();
        }

        private void CloseForgotPassword(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }

        private void Close(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }

        private async void SendEmail(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(InputEmail.Text)) {
                await DisplayAlert("MENSAGEM", "O CAMPO DO E-MAIL NÃO PODE ESTAR VAZIO", "OK");
            } else {
                await DisplayAlert("MENSAGEM", "VOCÊ RECEBERÁ UM EMAIL DE INSTRUÇÕES", "OK");
                await Navigation.PopModalAsync();
            }
        }
    }
}