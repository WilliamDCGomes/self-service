using SelfService.Models;
using SelfService.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage {
        ServicesDBProducts dbProducts = new ServicesDBProducts(App.DbPath);
        ServicesDBOrder dbOrder = new ServicesDBOrder(App.DbPath);
        ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
        ServicesDBReservation dbReservation = new ServicesDBReservation(App.DbPath);
        public int IdUser { get; private set; }
        public Home(int idUser) {
            InitializeComponent();
            RefreshList();
            IdUser = idUser;
            setUser();
        }

        private void RefreshEvent(object sender, EventArgs e) {
            RefreshList();
            Products.IsRefreshing = false;
            PromotionProducts.IsRefreshing = false;
            Orders.IsRefreshing = false;
            Reservation.IsRefreshing = false;
        }

        public void RefreshList() {
            Products.ItemsSource = dbProducts.ListProducts(DateTime.Now.ToString("dd/MM/yyyy"));
            PromotionProducts.ItemsSource = dbProducts.LocalePromotion(DPCalendar.Date);
            Orders.ItemsSource = dbOrder.Locale(DPCalendarOrder.Date.ToString("dd/MM/yyyy"));
            Reservation.ItemsSource = dbReservation.LocalePerDate(DPCalendarReservation.Date.ToString("dd/MM/yyyy"));
        }

        public void setUser() {
            ModelUser user = dbUser.LocaleByID(IdUser);
            OutputName.Text = user.Name;
            OutputLastName.Text = user.LastName;
            OutputEmailAdress.Text = user.EmailAdress;
            OutputPhoneNumber.Text = user.Phone;
            OutputCEP.Text = user.Cep;
            OutputStreet.Text = user.Street;
            OutputHouseNumber.Text = user.HouseNumber;
            OutputNeighborhood.Text = user.Neighborhood;
            OutputCity.Text = user.City;
            PickerStates.SelectedItem = user.State;
            OutputLogin.Text = user.Login;
        }

        private void ProductSelected(object sender, SelectedItemChangedEventArgs e) {
            ModelProducts product = (ModelProducts) Products.SelectedItem;
            Ordering order = new Ordering(product, IdUser);
            Navigation.PushAsync(order);
            RefreshList();
        }

        private void ProductPromotionSelected(object sender, SelectedItemChangedEventArgs e) {
            ModelProducts product = (ModelProducts)PromotionProducts.SelectedItem;
            if (DateTime.Now.ToString("dd/MM/yyyy").Equals(product.DateDescont)) {
                Ordering order = new Ordering(product, IdUser);
                Navigation.PushAsync(order);
                RefreshList();
            } else {
                DisplayAlert("AVISO", "O PRATO NÃO ESTÁ EM OFERTA. VERIFIQUE AS OFERTAS DE HOJE, OU VÁ ATÉ O INÍCIO PARA SOLICITAR ESSE PRATO", "OK");
            }
        }

        private async void DoLogout(object sender, EventArgs e) {
            var logout = await DisplayAlert("LOGOUT", "DESEJA FAZER LOGOUT?", "SIM", "NÃO");
            if (logout) {
                App.Current.MainPage = new NavigationPage(new Login(true));
            }
        }

        private void AddProduct(object sender, EventArgs e) {
            Navigation.PushAsync(new RegisterPage());
        }

        private void DateSelectedAction(object sender, DateChangedEventArgs e) {
            RefreshList();
        }

        private void OrderSelected(object sender, SelectedItemChangedEventArgs e) {
            ModelOrder order = (ModelOrder)Orders.SelectedItem;
            Ordering ordering = new Ordering(order);
            Navigation.PushAsync(ordering);
            RefreshList();
        }

        private void DateSelectedOrder(object sender, DateChangedEventArgs e) {
            RefreshList();
        }

        private void CreateReservation(object sender, EventArgs e) {
            Navigation.PushModalAsync(new Reservation(dbUser.LocaleByID(IdUser)));
        }

        private void ReservationSelected(object sender, SelectedItemChangedEventArgs e) {
            ModelReservation reservation = (ModelReservation)Reservation.SelectedItem;
            Navigation.PushAsync(new ReservationScreen(reservation));
            RefreshList();
        }

        private void DateSelectedReservation(object sender, DateChangedEventArgs e) {
            RefreshList();
        }

        private void EditAccount(object sender, EventArgs e) {
            Navigation.PushAsync(new EditAccount(IdUser));
        }
    }
}