using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reservation : ContentPage {
        public Reservation() {
            InitializeComponent();
        }

        private void CloseReservation(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }

        private void ClickedPicker(object sender, FocusEventArgs e) {

        }

        private void GetLocation(object sender, EventArgs e) {

        }

        private void OpenPicker(object sender, EventArgs e) {

        }

        private void FinishOrder(object sender, EventArgs e) {

        }
    }
}