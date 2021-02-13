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
        public long IdUser { get; set; }
        public double Price { get; set; }
        ModelProducts Product;
        ModelOrder Order;
        public Ordering() {
            InitializeComponent();
        }
        public Ordering(ModelProducts product, long idUser) {
            InitializeComponent();
            Product = product;
            IdUser = idUser;
            OutputNameProduct.Text = product.Title;
            OutputDescription.Text = product.Description;
            if (product.ImageAdress != null) {
                ImageProduct.Source = product.ImageAdress;
            }
            if (product.InDescont) {
                OutputPrice.Text = product.NewValue.ToString("F").Replace(".", ",");
                Price = product.NewValue;
            } else {
                OutputPrice.Text = product.Price.ToString("F").Replace(".", ",");
                Price = product.Price;
            }
            OutputOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            checkUser();
        }
        public Ordering(ModelOrderAux order) {
            InitializeComponent();
            ModelOrder OrderAux = new ModelOrder();
            OrderAux.Id = order.Id;
            OrderAux.IdUser = order.IdUser;
            OrderAux.IdProduct = order.IdProduct;
            OrderAux.ProductName = order.ProductName;
            OrderAux.StatusOrder = order.StatusOrder;
            OrderAux.OrderDate = order.OrderDate;
            OrderAux.AlreadyPayed = order.AlreadyPayed;
            OrderAux.LocationClient = order.LocationClient;
            OrderAux.Price = order.Price;
            Order = OrderAux;
            OutputNameProduct.Text = order.ProductName;
            OutputPrice.Text = order.Price.ToString("F").Replace(".", ",");
            OutputDescription.Text = getDescriptionProduct(order.IdProduct);
            OutputOrderDate.Text = order.OrderDate;
            OutputStatus.Text = order.StatusOrder;
            Picker.IsEnabled = false;
            LocationOutput.Text = "Mesa";
            Picker.Title = order.LocationClient;
            Picker.IsEnabled = false;
            FinalizeOrder.Text = "EDITAR";
            LocationSelectImage.IsVisible = false;
            LocationWrite.IsVisible = false;
            FrameEntry.IsVisible = false;
            InputLocation.IsVisible = false;
            EditProductImage.IsVisible = false;
            getImageProduct(order.IdProduct);
        }

        public void checkUser() {
            ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
            ModelUser user = dbUser.LocaleByID(IdUser);
            if (!user.IsAdmin) {
                EditProductImage.IsVisible = false;
            }
        }

        private void getImageProduct(int id) {
            ServicesDBProducts productGet = new ServicesDBProducts(App.DbPath);
            ModelProducts product = productGet.GetProductById(id);
            if (product.ImageAdress != null) {
                ImageProduct.Source = product.ImageAdress;
            }
        }

        private string getDescriptionProduct(int id) {
            ServicesDBProducts productGet = new ServicesDBProducts(App.DbPath);
            ModelProducts product = productGet.GetProductById(id);
            return product.Description;
        }

        private void BackHome(object sender, EventArgs e) {
            Navigation.PopAsync();
        }

        public async void InsertOrder() {
            ModelOrder order = new ModelOrder();
            order.IdUser = this.IdUser;
            order.IdProduct = Product.Id;
            order.ProductName = OutputNameProduct.Text;
            order.StatusOrder = "Solicitado";
            order.OrderDate = OutputOrderDate.Text;
            order.AlreadyPayed = false;
            order.Price = Price;
            order.LocationClient = InputLocation.Text;
            ServicesDBOrder dbOrder = new ServicesDBOrder(App.DbPath);
            bool Worked = dbOrder.Insert(order);
            if (Worked) {
                await DisplayAlert("Sucesso", "Pedido realizado com sucesso!\nPor Favor, aguarde. Logo você o receberá em sua mesa", "OK");
                await Navigation.PopAsync();
            }
        }

        private void FinishOrder(object sender, EventArgs e) {
            if (FinalizeOrder.Text.Equals("FINALIZAR")) {
                if (DataValidation()) {
                    InsertOrder();
                }
            } else if (FinalizeOrder.Text.Equals("EDITAR")) {
                if (DateTime.Now.ToString("dd/MM/yyyy").Equals(Order.OrderDate)) {
                    Navigation.PushAsync(new EditOrder(Order));
                } else {
                    DisplayAlert("Erro", "Você não pode editar o pedido de outro dia!", "OK");
                }
            }
        }

        private bool DataValidation() {
            if (!String.IsNullOrEmpty(InputLocation.Text)) {
                return true;
            }
            DisplayAlert("Erro", "Por Favor informe a dua mesa antes de finalizar o pedido", "OK");
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

        private void EditProduct(object sender, EventArgs e) {
            Navigation.PushAsync(new EditProduct(Product, IdUser));
        }
    }
}