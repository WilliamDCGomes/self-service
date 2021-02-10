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
            bool Worked = dbProducts.Insert(modelProducts);
            if (Worked) {
                await DisplayAlert("SUCESSO", "Prato cadastrado com sucesso", "OK");
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