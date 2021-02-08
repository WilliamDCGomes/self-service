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
    public partial class ScannerPlaces : ContentPage {
        public Ordering Ordering = null;
        public Reservation Reservation = null;
        public EditOrder EditOrder = null;
        public EditReservation EditReservation = null;
        public ScannerPlaces(Ordering ordering) {
            InitializeComponent();
            Ordering = ordering;
        }
        public ScannerPlaces(Reservation reservation) {
            InitializeComponent();
            Reservation = reservation;
        }
        public ScannerPlaces(EditOrder editOrder) {
            InitializeComponent();
            EditOrder = editOrder;
        }
        public ScannerPlaces(EditReservation editReservation) {
            InitializeComponent();
            EditReservation = editReservation;
        }

        private async void Scanneing(ZXing.Result result) {
            Device.BeginInvokeOnMainThread(async () => {
                if (ValidateCode(result.Text)) {
                    if (Ordering != null) {
                        Ordering.SetLocation("1");
                    } else if (Reservation != null) {
                        Reservation.SetLocation("1");
                    } else if (EditOrder != null) {
                        EditOrder.SetLocation("1");
                    } else if (EditReservation != null) {
                        EditReservation.SetLocation("1");
                    }
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