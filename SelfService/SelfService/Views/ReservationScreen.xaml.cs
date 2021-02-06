using SelfService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationScreen : ContentPage {
        public ReservationScreen(ModelReservation reservation) {
            InitializeComponent();
            OutputNameUser.Text = reservation.NameUser;
            ReservationDate.Text = reservation.ReservationDate;
            OutputLocation.Text = reservation.LocationClient;
            OutputQuantityPeople.Text = reservation.NumberSeats.ToString();
        }
    }
}