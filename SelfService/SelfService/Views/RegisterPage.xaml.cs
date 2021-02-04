using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.IO;
using SelfService.Models;
using SelfService.Services;
using SelfService.SecundaryView;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage {
        public RegisterPage() {
            InitializeComponent();
        }

        private void BackHome(object sender, EventArgs e) {
            Navigation.PopAsync();
        }

        public async void InsertOrder() {
            ModelProducts modelProducts = new ModelProducts();
            modelProducts.Title = InputNameProduct.Text;
            modelProducts.Price = Double.Parse(InputPrice.Text.Replace(",", "."));
            modelProducts.Description = InputDescription.Text;
            if (String.IsNullOrEmpty(InputDescont.Text)) {
                modelProducts.InDescont = false;
            } else {
                modelProducts.InDescont = true;
                modelProducts.DescontPercent = Double.Parse(InputDescont.Text.Replace(",", "."));
                string value = (Double.Parse(InputPrice.Text.Replace(",", ".")) - ((Double.Parse(InputPrice.Text.Replace(",", ".")) * Double.Parse(InputDescont.Text.Replace(",", "."))) / 100)).ToString("F");
                modelProducts.NewValue = Double.Parse(value.Replace(",", "."));
                modelProducts.DateDescont = DPCalendar.Date;
            }
            ServicesDBProducts dbProducts = new ServicesDBProducts(App.DbPath);
            bool Worked = dbProducts.Insert(modelProducts);
            if (Worked) {
                await DisplayAlert("SUCESSO", "PRATO CADASTRADO COM SUCESSO", "OK");
                await Navigation.PopAsync();
            }
        }

        private void FinishOrder(object sender, EventArgs e) {
            if (DataValidation()) {
                InsertOrder();
            }
        }

        private bool DataValidation() {
            if (!String.IsNullOrEmpty(InputNameProduct.Text) && !String.IsNullOrEmpty(InputPrice.Text)) {
                return true;
            }
            DisplayAlert("ERRO", "POR FAVOR PREENCHA TODOS OS CAMPOS OBRIGATÓRIOS", "OK");
            return false;
        }


        /*
* Pega foto dos arquivos
private async void GetPicture(object sender, EventArgs e) {
FileResult pickResult = await FilePicker.PickAsync(new PickOptions {
FileTypes = FilePickerFileType.Images,
PickerTitle = "Selecione a Imagem que deseja"
});
if(pickResult != null) {
var stream = await pickResult.OpenReadAsync();
resultImage.Source =  ImageSource.FromStream(() => stream);
}
}


* Tira foto, mas não salva nos arquivos
* 
* private async void GetPictureFromCamera(object sender, EventArgs e) {
var result = await MediaPicker.CapturePhotoAsync();
if (result != null) {
var stream = await result.OpenReadAsync();
resultImagePicture.Source = ImageSource.FromStream(() => stream);
buttonTakePicture.Text = ImageSource.FromStream(() => stream).ToString();
}
}*/
    }
}