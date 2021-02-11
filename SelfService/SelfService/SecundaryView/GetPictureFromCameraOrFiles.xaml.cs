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
        RegisterPage NewProduct;
        public GetPictureFromCameraOrFiles(RegisterPage newProduct) {
            InitializeComponent();
            NewProduct = newProduct;
        }

        private void CloseForgotPassword(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }

        private async void GetPictureFromCamera(object sender, EventArgs e) {
            NewProduct.choose = 1;
            NewProduct.SetPath();
            await Navigation.PopModalAsync();
        }

        private async void GetPictureFromFiles(object sender, EventArgs e) {
            NewProduct.choose = 2;
            NewProduct.SetPath();
            await Navigation.PopModalAsync();
        }

        private void Close(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }
    }
}