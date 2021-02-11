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
                await DisplayAlert("Mensagem", "O campo do E-mail não pode estar vazio", "OK");
            } else {
                await DisplayAlert("Mensagem", "Você receberá um email de instruções", "OK");
                await Navigation.PopModalAsync();
            }
        }
    }
}