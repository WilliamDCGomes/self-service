using SelfService.Models;
using SelfService.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage {
        ServicesDBProducts dbProducts = new ServicesDBProducts(App.DbPath);
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
        }

        public void RefreshList() {
            Products.ItemsSource = dbProducts.ListProducts();
            PromotionProducts.ItemsSource = dbProducts.LocalePromotion(DPCalendar.Date);
        }

        private void ProductSelected(object sender, SelectedItemChangedEventArgs e) {
            ModelProducts product = (ModelProducts) Products.SelectedItem;
            Ordering order = new Ordering(product, IdUser);
            Navigation.PushAsync(order);
            RefreshList();
        }

        private void ProductPromotionSelected(object sender, SelectedItemChangedEventArgs e) {
            ModelProducts product = (ModelProducts)PromotionProducts.SelectedItem;
            Ordering order = new Ordering(product, IdUser);
            Navigation.PushAsync(order);
            RefreshList();
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
    }
}