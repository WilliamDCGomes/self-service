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
    public partial class Home : TabbedPage {
        ServicesDBProducts dbProducts = new ServicesDBProducts(App.DbPath);
        public Home() {
            InitializeComponent();
            RefreshList();
        }

        private void ProductSelected(object sender, EventArgs e) {
            
        }

        private void RefreshEvent(object sender, EventArgs e) {
            RefreshList();
            Products.IsRefreshing = false;
        }

        public void RefreshList() {
            Products.ItemsSource = dbProducts.ListProducts();
        }

        private void Products_ItemTapped(object sender, ItemTappedEventArgs e) {
            Ordering order = new Ordering();
            Navigation.PushAsync(order);
        }
    }
}