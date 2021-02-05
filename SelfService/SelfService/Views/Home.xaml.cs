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
        public int IdUser { get; private set; }
        public Home(int idUser) {
            InitializeComponent();
            RefreshList();
            IdUser = idUser;
            IdUserShow.Text = idUser.ToString();
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

        private void DoLogout(object sender, EventArgs e) {
            App.Current.MainPage = new NavigationPage(new Login());
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
            Navigation.PushModalAsync(new Reservation());
        }

        private void ReservationSelected(object sender, SelectedItemChangedEventArgs e) {

        }

        private void DateSelectedReservation(object sender, DateChangedEventArgs e) {

        }
    }
}