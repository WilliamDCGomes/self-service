using SelfService.Models;
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
    public partial class EditProduct : ContentPage {
        ModelProducts Product;
        private int IdUser;
        public EditProduct(ModelProducts product, int idUser) {
            InitializeComponent();
            IdUser = idUser;
            Product = product;
            InputNameProduct.Text = product.Title;
            InputPrice.Text = product.Price.ToString().Replace(".", ",");
            InputDescription.Text = product.Description;
            if (product.DescontPercent != 0) {
                InputDescont.Text = product.DescontPercent.ToString().Replace(".", ",");
            }
            if (!product.DateDescont.Equals("30/01/1998") && !product.DateDescont.Equals("30-01-1998")) {
                string[] newdate = product.DateDescont.Split('/', '-');
                DPCalendar.Date = DateTime.Parse(newdate[1] + "/" + newdate[0] + "/" + newdate[2]);
            } 
            else {
                DPCalendar.Date = DateTime.Now;
            }
        }

        private void Back(object sender, EventArgs e) {
            Navigation.PopAsync();
        }

        public async void UpdateProduct() {
            ModelProducts modelProducts = new ModelProducts();
            modelProducts.Id = Product.Id;
            modelProducts.Title = InputNameProduct.Text;
            modelProducts.Price = Double.Parse(InputPrice.Text.Replace(",", "."));
            modelProducts.Description = InputDescription.Text;
            if (String.IsNullOrEmpty(InputDescont.Text) || double.Parse(InputDescont.Text) <= 0.0) {
                modelProducts.InDescont = false;
                modelProducts.DescontPercent = 0;
                modelProducts.NewValue = 0;
                modelProducts.DateDescont = "30/01/1998";
            } else {
                modelProducts.InDescont = true;
                modelProducts.DescontPercent = Double.Parse(InputDescont.Text.Replace(",", "."));
                string value = (Double.Parse(InputPrice.Text.Replace(",", ".")) - ((Double.Parse(InputPrice.Text.Replace(",", ".")) * Double.Parse(InputDescont.Text.Replace(",", "."))) / 100)).ToString("F");
                modelProducts.NewValue = Double.Parse(value.Replace(",", "."));
                modelProducts.DateDescont = DPCalendar.Date.ToString("dd/MM/yyyy");
            }
            ServicesDBProducts dbProducts = new ServicesDBProducts(App.DbPath);
            bool Worked = dbProducts.Update(modelProducts);
            if (Worked) {
                await DisplayAlert("SUCESSO", "Prato atualizado com sucesso", "OK");
                App.Current.MainPage = new NavigationPage(new Home(IdUser));
            }
        }

        private void FinishProduct(object sender, EventArgs e) {
            if (DataValidation()) {
                UpdateProduct();
            }
        }

        private bool DataValidation() {
            if (!String.IsNullOrEmpty(InputNameProduct.Text) && !String.IsNullOrEmpty(InputPrice.Text)) {
                return true;
            }
            DisplayAlert("ERRO", "Por Favor preencha todos os campos obrigatórios", "OK");
            return false;
        }

        private void GetDate(object sender, FocusEventArgs e) {
            if (InputDescont.Text.Length > 0) {
                DPCalendar.IsEnabled = true;
            } else {
                DPCalendar.IsEnabled = false;
            }
        }

        private async void DeleteProduct(object sender, EventArgs e) {
            var cancel = await DisplayAlert("AVISO", "Deseja mesmo excluir o prato?", "SIM", "NÃO");
            if (cancel) {
                ServicesDBProducts dbProducts = new ServicesDBProducts(App.DbPath);
                bool Worked = dbProducts.Delete(Product);
                if (Worked) {
                    await DisplayAlert("SUCESSO", "Prato excluido com sucesso", "OK");
                    App.Current.MainPage = new NavigationPage(new Home(IdUser));
                }
            }
        }
    }
}