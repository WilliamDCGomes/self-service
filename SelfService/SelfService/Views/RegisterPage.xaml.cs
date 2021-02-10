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
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage {
        string imagePath = null;
        public RegisterPage() {
            InitializeComponent();
        }

        private void BackHome(object sender, EventArgs e) {
            Navigation.PopAsync();
        }

        public async void InsertProducts() {
            ModelProducts modelProducts = new ModelProducts();
            modelProducts.Title = InputNameProduct.Text;
            modelProducts.Price = Double.Parse(InputPrice.Text.Replace(",", "."));
            modelProducts.Description = InputDescription.Text;
            modelProducts.ImageAdress = imagePath;
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

        private void FinishCadastreProduct(object sender, EventArgs e) {
            if (DataValidation()) {
                InsertProducts();
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

        private void GetPhoto(object sender, EventArgs e) {
            GetPictureFromCamera(new object(), new EventArgs());
        }

        private async void GetPictureFromCamera(object sender, EventArgs e) {
            try {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                    await DisplayAlert("ERRO", "Sem acesso a câmera", "OK");
                    return;
                }
                var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (!cameraStatus.ToString().Equals("Granted") || !storageStatus.ToString().Equals("Granted")) {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                    cameraStatus = results[Permission.Camera];
                    storageStatus = results[Permission.Storage];
                }
                if (cameraStatus.ToString().Equals("Granted") && storageStatus.ToString().Equals("Granted")) {
                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions {
                        SaveToAlbum = true,
                        Directory = "SelfServicePictures",
                        CompressionQuality = 75,
                        CustomPhotoSize = 50,
                        PhotoSize = PhotoSize.MaxWidthHeight,
                        MaxWidthHeight = 2000,
                        DefaultCamera = CameraDevice.Front
                    });
                    if (file != null) {
                        ProductImage.Source = ImageSource.FromStream(() => {
                            var stream = file.GetStream();
                            file.Dispose();
                            return stream;
                        });
                        imagePath = file.Path.ToString();
                    }
                } 
                else {
                    CrossPermissions.Current.OpenAppSettings();
                }
            } catch (Exception ex) {
                await DisplayAlert("ERRO", "O sistema não tem os acessos necessários", "Ok");
            }
        }




        /*
        //Pega foto dos arquivos
        private async void GetPictureFromFiles(object sender, EventArgs e) {
            FileResult pickResult = await FilePicker.PickAsync(new PickOptions {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Selecione a Imagem que deseja"
            });
            if (pickResult != null) {
                var stream = await pickResult.OpenReadAsync();
                //resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }
        */
    }
}