using SelfService.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.SecundaryView {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetPictureFromCameraOrFiles : ContentPage {
        RegisterPage NewProduct = null;
        EditProduct EditProduct = null;
        public GetPictureFromCameraOrFiles(RegisterPage newProduct) {
            InitializeComponent();
            NewProduct = newProduct;
        }
        public GetPictureFromCameraOrFiles(EditProduct editProduct) {
            InitializeComponent();
            EditProduct = editProduct;
        }

        private void CloseForgotPassword(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }

        private async void GetPictureFromCamera(object sender, EventArgs e) {
            if (NewProduct != null) {
                NewProduct.choose = 1;
                NewProduct.SetPath();
            } 
            else {
                EditProduct.choose = 1;
                EditProduct.SetPath();
            }
            await Navigation.PopModalAsync();
        }

        private async void GetPictureFromFiles(object sender, EventArgs e) {
            if (NewProduct != null) {
                NewProduct.choose = 2;
                NewProduct.SetPath();
            } 
            else {
                EditProduct.choose = 2;
                EditProduct.SetPath();
            }
            await Navigation.PopModalAsync();
        }

        private void Close(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }
    }
}