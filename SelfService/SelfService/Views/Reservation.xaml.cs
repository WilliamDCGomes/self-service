using SelfService.Models;
using SelfService.SecundaryView;
using SelfService.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reservation : ContentPage {
        private long IdUser;
        public Reservation(ModelUser user) {
            InitializeComponent();
            OutputNameUser.Text = user.Name;
            IdUser = user.Id;
        }

        public async void InsertReservation() {
            ModelReservation reservation = new ModelReservation();
            reservation.IdUser = this.IdUser;
            reservation.NameUser = OutputNameUser.Text;
            reservation.ReservationDate = ReservationDate.Date.ToString("dd/MM/yyyy");
            reservation.LocationClient = InputLocation.Text;
            reservation.NumberSeats = int.Parse(InputQuantityPeople.Text);
            ServicesDBReservation dbReservation = new ServicesDBReservation(App.DbPath);
            bool Worked = dbReservation.Insert(reservation);
            if (Worked) {
                await DisplayAlert("Sucesso", "Reserva realizada com sucesso!", "OK");
                await Navigation.PopModalAsync();
            }
        }

        private void CloseReservation(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }

        private void ClickedPicker(object sender, FocusEventArgs e) {
            if (Picker.SelectedIndex == 0) {
                Picker.SelectedIndex = -1;
            } else if (Picker.SelectedIndex == 1) {
                Picker.SelectedIndex = -1;
            }
        }

        private void GetLocation(object sender, EventArgs e) {
            if (Picker.SelectedIndex == 0) {
                InputLocation.IsEnabled = false;
                Navigation.PushModalAsync(new ScannerPlaces(this));
            } else if (Picker.SelectedIndex == 1) {
                InputLocation.IsEnabled = true;
                InputLocation.Focus();
            }
        }

        public void SetLocation(string location) {
            InputLocation.Text = location;
        }

        private void OpenPicker(object sender, EventArgs e) {
            Picker.Focus();
        }

        private void FinishReservation(object sender, EventArgs e) {
            if (DataValidation()) {
                InsertReservation();
            }
        }

        private bool DataValidation() {
            if (ReservationDate.Date != null && !String.IsNullOrEmpty(InputLocation.Text) && !String.IsNullOrEmpty(InputQuantityPeople.Text)) {
                return true;
            } else {
                DisplayAlert("Aviso", "Por Favor, preencha todos os campos obrigatórios", "OK");
            }
            return false;
        }
    }
}