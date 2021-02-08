using SelfService.Models;
using SelfService.SecundaryView;
using SelfService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditReservation : ContentPage {
        ModelReservation Reservation;
        public EditReservation(ModelReservation reservation) {
            InitializeComponent();
            Reservation = reservation;
            OutputNameUser.Text = reservation.NameUser;
            string[] newdate = reservation.ReservationDate.Split('/');
            ReservationDate.Date = DateTime.Parse(newdate[1] + "/" + newdate[0] + "/" + newdate[2]);
            InputLocation.Text = reservation.LocationClient;
            InputQuantityPeople.Text = reservation.LocationClient;
        }

        public async void UpdateReservation() {
            ModelReservation reservation = new ModelReservation();
            reservation.Id = Reservation.Id;
            reservation.IdUser = Reservation.IdUser;
            reservation.NameUser = OutputNameUser.Text;
            reservation.ReservationDate = ReservationDate.Date.ToString("dd/MM/yyyy");
            reservation.LocationClient = InputLocation.Text;
            reservation.NumberSeats = int.Parse(InputQuantityPeople.Text);
            ServicesDBReservation dbReservation = new ServicesDBReservation(App.DbPath);
            bool Worked = dbReservation.Update(reservation);
            if (Worked) {
                await DisplayAlert("SUCESSO", "RESERVA ATUALIZADA COM SUCESSO!", "OK");
                App.Current.MainPage = new NavigationPage(new Home(Reservation.IdUser)); ;
            }
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
                UpdateReservation();
            }
        }

        private bool DataValidation() {
            if (ReservationDate.Date != null && !String.IsNullOrEmpty(InputLocation.Text) && !String.IsNullOrEmpty(InputQuantityPeople.Text)) {
                return true;
            } else {
                DisplayAlert("AVISO", "POR FAVOR, PREENCHA TODOS OS CAMPOS OBRIGATÓRIOS", "OK");
            }
            return false;
        }

        private void Back(object sender, EventArgs e) {
            Navigation.PopAsync();
        }

        private async void DeleteReservation(object sender, EventArgs e) {
            var cancel = await DisplayAlert("AVISO", "DESEJA MESMO CANCELAR A RESERVA?", "SIM", "NÃO");
            if (cancel) {
                ServicesDBReservation dbReservation = new ServicesDBReservation(App.DbPath);
                bool Worked = dbReservation.Delete(Reservation);
                if (Worked) {
                    await DisplayAlert("SUCESSO", "RESERVA CANCELADA COM SUCESSO", "OK");
                    App.Current.MainPage = new NavigationPage(new Home(Reservation.IdUser));
                }
            }
        }
    }
}