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
    public partial class Ordering : ContentPage {
        public int IdUser { get; set; }
        public Ordering() {
            InitializeComponent();
        }

        

        private void BackHome(object sender, EventArgs e) {
            Navigation.PopAsync();
        }

        public async void InsertOrder() {
            ModelOrder order = new ModelOrder();
            order.IdUser = 1;//this.IdUser;
            order.IdProduct = 1;//int.Parse(OutputNameProduct.Text);
            order.StatusOrder = 1;
            order.OrderDate = DateTime.Now;//DateTime.Parse(OutputOrderDate.Text);
            order.AlreadyPayed = false;
            order.LocationClient = InputLocation.Text;
            ServicesDBOrder dbOrder = new ServicesDBOrder(App.DbPath);
            bool Worked = dbOrder.Insert(order);
            if (Worked) {
                await DisplayAlert("SUCESSO", "PEDIDO REALIZADO COM SUCESSO!\nPOR FAVOR, AGUARDE. LOGO VOCÊ O RECEBERÁ EM SUA MESA", "OK");
                await Navigation.PopAsync();
            }
        }

        private void FinishOrder(object sender, EventArgs e) {
            if (DataValidation()) {
                InsertOrder();
            }
        }

        private bool DataValidation() {
            if(!String.IsNullOrEmpty(InputLocation.Text)) {
                return true;
            }
            DisplayAlert("ERRO", "POR FAVOR INFORME A SUA MESA ANTES DE FINALIZAR O PEDIDO", "OK");
            return false;
        }

        private void GetLocation(object sender, EventArgs e) {
            if (Picker.SelectedIndex == 0) {
                InputLocation.IsEnabled = false;
                Navigation.PushModalAsync(new OrderLocation(this));
            }
            else if(Picker.SelectedIndex == 1) {
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
            }
            else if (Picker.SelectedIndex == 1) {
                Picker.SelectedIndex = -1;
            }
        }

        private void OpenPicker(object sender, EventArgs e) {
            Picker.Focus();
        }
    }
}