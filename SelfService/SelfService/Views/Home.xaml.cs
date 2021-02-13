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
        public long IdUser { get; private set; }
        public Home(long idUser) {
            InitializeComponent();
            RefreshList();
            IdUser = idUser;
            setUser();
        }

        public Home() {
        }

        private void RefreshEvent(object sender, EventArgs e) {
            RefreshList();
            Products.IsRefreshing = false;
            PromotionProducts.IsRefreshing = false;
            Orders.IsRefreshing = false;
            Reservation.IsRefreshing = false;
        }

        public void RefreshList() {
            List<ModelProducts> products = dbProducts.ListProducts(DateTime.Now.ToString("dd/MM/yyyy"));
            foreach(ModelProducts i in products) {
                if (i.ImageAdress == null) {
                    i.ImageAdress = "iconMenu";
                }
            }
            Products.ItemsSource = products;
            List<ModelProducts> promotionProducts = dbProducts.LocalePromotion(DPCalendar.Date);
            foreach (ModelProducts i in promotionProducts) {
                if (i.ImageAdress == null) {
                    i.ImageAdress = "iconMenu";
                }
            }
            PromotionProducts.ItemsSource = promotionProducts;
            List<ModelOrderAux> orderedProducts = new List<ModelOrderAux>();
            List<ModelOrder> orderedProduct = dbOrder.Locale(DPCalendarOrder.Date.ToString("dd/MM/yyyy"));
            foreach (ModelOrder i in orderedProduct) {
                ModelOrderAux aux = new ModelOrderAux();
                aux.Id = i.Id;
                aux.IdUser = i.IdUser;
                aux.IdProduct = i.IdProduct;
                aux.ProductName = i.ProductName;
                aux.StatusOrder = i.StatusOrder;
                aux.OrderDate = i.OrderDate;
                aux.AlreadyPayed = i.AlreadyPayed;
                aux.LocationClient = i.LocationClient;
                aux.Price = i.Price;
                aux.ImageAdress = getImageProduct(i.IdProduct);
                orderedProducts.Add(aux);
            }
            Orders.ItemsSource = orderedProducts;
            Reservation.ItemsSource = dbReservation.LocalePerDate(DPCalendarReservation.Date.ToString("dd/MM/yyyy"));
        }

        private string getImageProduct(int id) {
            ServicesDBProducts productGet = new ServicesDBProducts(App.DbPath);
            ModelProducts product = productGet.GetProductById(id);
            if (product.ImageAdress != null) {
                return product.ImageAdress;
            }
            return "iconMenu";
        }

        public void setUser() {
            ModelUser user = dbUser.LocaleByID(IdUser);
            if (!user.IsAdmin) {
                buttonNewProduct.IsVisible = false;
            }
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
            var item = e.SelectedItem as ModelProducts;
            if (item == null) {
                return;
            }
            (sender as Xamarin.Forms.ListView).SelectedItem = null;
            Ordering order = new Ordering(item, IdUser);
            Navigation.PushAsync(order);
        }

        private void ProductPromotionSelected(object sender, SelectedItemChangedEventArgs e) {
            var item = e.SelectedItem as ModelProducts;
            if (item == null) {
                return;
            }
            (sender as Xamarin.Forms.ListView).SelectedItem = null;
            if (DateTime.Now.ToString("dd/MM/yyyy").Equals(item.DateDescont)) {
                Ordering order = new Ordering(item, IdUser);
                Navigation.PushAsync(order);
            } else {
                DisplayAlert("Aviso", "O prato não está em oferta. Verifique as ofertas de hpje, ou vá até o início para solicitar esse prato", "OK");
            }
        }

        private async void DoLogout(object sender, EventArgs e) {
            var logout = await DisplayAlert("Logout", "Deseja fazer Logout?", "SIM", "NÃO");
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
            var item = e.SelectedItem as ModelOrderAux;
            if (item == null) {
                return;
            }
            (sender as Xamarin.Forms.ListView).SelectedItem = null;
            Ordering ordering = new Ordering(item);
            Navigation.PushAsync(ordering);
        }

        private void DateSelectedOrder(object sender, DateChangedEventArgs e) {
            RefreshList();
        }

        private void CreateReservation(object sender, EventArgs e) {
            Navigation.PushModalAsync(new Reservation(dbUser.LocaleByID(IdUser)));
        }

        private void ReservationSelected(object sender, SelectedItemChangedEventArgs e) {
            var item = e.SelectedItem as ModelReservation;
            if (item == null) {
                return;
            }
            (sender as Xamarin.Forms.ListView).SelectedItem = null;
            Navigation.PushAsync(new ReservationScreen(item));
        }

        private void DateSelectedReservation(object sender, DateChangedEventArgs e) {
            RefreshList();
        }

        private void EditAccount(object sender, EventArgs e) {
            Navigation.PushAsync(new EditAccount(IdUser));
        }
    }
}