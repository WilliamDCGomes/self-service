using SelfService.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.SecundaryView {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderLocation : ContentPage {
        public Ordering Ordering;
        public OrderLocation(Ordering ordering) {
            InitializeComponent();
            Ordering = ordering;
        }

        private async void Scanneing(ZXing.Result result) {
            Device.BeginInvokeOnMainThread(async () => {
                if (ValidateCode(result.Text)) {
                    Ordering.SetLocation(result.Text);
                    await Navigation.PopModalAsync();
                } else {
                    await DisplayAlert("ERRO", "O CÓDIGO DA MESA NÃO É VÁLIDO\nVERIFIQUE SE ESTÁ SCANEANDO O QR CODE CORRETO\nCASO O ERRO CONTINUE, COMUNIQUE ALGUM FUNCIONÁRIO PRÓXIMO!", "OK");
                }
            });
        }

        private void Cancel(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }

        private bool ValidateCode(string code) {
            if (code.Equals("12345678")) {
                return true;
            }
            return false;
        }
    }
}