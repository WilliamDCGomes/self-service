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
    public partial class EditOrder : ContentPage {
        public int IdUser { get; set; }
        public double Price { get; set; }
        ModelOrder Order;
        public EditOrder(ModelOrder order) {
            InitializeComponent();
            Order = order;
            IdUser = order.IdUser;
            OutputNameProduct.Text = order.ProductName;
            OutputPrice.Text = order.Price.ToString("F").Replace(".", ",");
            Price = order.Price;
            string[] newdate = order.OrderDate.Split('/', '-');
            OutputOrderDate.Date = DateTime.Parse(newdate[1] + "/" + newdate[0] + "/" + newdate[2]);
            InputLocation.Text = order.LocationClient;
        }

        private void Back(object sender, EventArgs e) {
            Navigation.PopAsync();
        }

        public async void UpdateOrder() {
            ModelOrder order = new ModelOrder();
            order.Id = Order.Id;
            order.IdUser = this.IdUser;
            order.IdProduct = Order.IdProduct;
            order.ProductName = OutputNameProduct.Text;
            order.StatusOrder = "Solicitado";
            order.OrderDate = OutputOrderDate.Date.ToString("dd/MM/yyyy");
            order.AlreadyPayed = false;
            order.Price = Price;
            order.LocationClient = InputLocation.Text;
            ServicesDBOrder dbOrder = new ServicesDBOrder(App.DbPath);
            bool Worked = dbOrder.Update(order);
            if (Worked) {
                await DisplayAlert("SUCESSO", "PEDIDO ATUALIZADO COM SUCESSO!\nPOR FAVOR, AGUARDE. LOGO VOCÊ O RECEBERÁ EM SUA MESA", "OK");
                App.Current.MainPage = new NavigationPage(new Home(IdUser));
            }
        }

        private void FinishOrder(object sender, EventArgs e) {
            if (DataValidation()) {
                UpdateOrder();
            }
        }

        private bool DataValidation() {
            if (!String.IsNullOrEmpty(InputLocation.Text)) {
                return true;
            }
            DisplayAlert("ERRO", "POR FAVOR INFORME A SUA MESA ANTES DE ATUALIZAR O PEDIDO", "OK");
            return false;
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

        private void ClickedPicker(object sender, FocusEventArgs e) {
            if (Picker.SelectedIndex == 0) {
                Picker.SelectedIndex = -1;
            } else if (Picker.SelectedIndex == 1) {
                Picker.SelectedIndex = -1;
            }
        }

        private void OpenPicker(object sender, EventArgs e) {
            Picker.Focus();
        }

        private async void CancelOrder(object sender, EventArgs e) {
            var cancel = await DisplayAlert("AVISO", "DESEJA MESMO CANCELAR O PEDIDO?", "SIM", "NÃO");
            if (cancel) {
                ServicesDBOrder dbOrders = new ServicesDBOrder(App.DbPath);
                bool Worked = dbOrders.Delete(Order);
                if (Worked) {
                    await DisplayAlert("SUCESSO", "PEDIDO CANCELADO COM SUCESSO", "OK");
                    App.Current.MainPage = new NavigationPage(new Home(IdUser));
                }
            }
        }
    }
}