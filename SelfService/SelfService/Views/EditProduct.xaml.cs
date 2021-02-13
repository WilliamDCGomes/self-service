using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
    public partial class EditProduct : ContentPage {
        ModelProducts Product;
        private long IdUser;
        string imagePath = null;
        public int choose = 0;
        public EditProduct(ModelProducts product, long idUser) {
            InitializeComponent();
            IdUser = idUser;
            Product = product;
            InputNameProduct.Text = product.Title;
            InputPrice.Text = product.Price.ToString().Replace(".", ",");
            InputDescription.Text = product.Description;
            if (product.ImageAdress != null) {
                ProductImage.Source = product.ImageAdress;
                imagePath = product.ImageAdress;
            }
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
            if(imagePath != null) {
                modelProducts.ImageAdress = imagePath;
            }
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
                await DisplayAlert("Sucesso", "Prato atualizado com sucesso", "OK");
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
            DisplayAlert("Erro", "Por Favor preencha todos os campos obrigatórios", "OK");
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
            var cancel = await DisplayAlert("Aviso", "Deseja mesmo excluir o prato?", "SIM", "NÃO");
            if (cancel) {
                ServicesDBProducts dbProducts = new ServicesDBProducts(App.DbPath);
                bool Worked = dbProducts.Delete(Product);
                if (Worked) {
                    await DisplayAlert("Sucesso", "Prato excluido com sucesso", "OK");
                    App.Current.MainPage = new NavigationPage(new Home(IdUser));
                }
            }
        }

        private async void GetPhoto(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new GetPictureFromCameraOrFiles(this));
        }

        public void SetPath() {
            if (choose == 1) {
                GetPictureFromCamera(new object(), new EventArgs());
            } else if (choose == 2) {
                GePictureFromFiles(new object(), new EventArgs());
            }
            choose = 0;
        }

        private async void GetPictureFromCamera(object sender, EventArgs e) {
            try {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                    await DisplayAlert("Erro", "Sem acesso a câmera", "OK");
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
                            imagePath = file.Path.ToString();
                            file.Dispose();
                            return stream;
                        });
                    }
                } else {
                    CrossPermissions.Current.OpenAppSettings();
                }
            } catch (Exception ex) {
                await DisplayAlert("Erro", "O sistema não tem os acessos necessários", "Ok");
            }
        }
        private async void GePictureFromFiles(object sender, EventArgs e) {
            try {
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (!storageStatus.ToString().Equals("Granted")) {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                    storageStatus = results[Permission.Storage];
                }
                if (storageStatus.ToString().Equals("Granted")) {
                    if (!CrossMedia.Current.IsPickPhotoSupported) {
                        await DisplayAlert("Erro", "Não suportado", "OK");
                        return;
                    }
                    var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions {
                        PhotoSize = PhotoSize.Full,
                        SaveMetaData = true
                    });
                    if (file == null) {
                        return;
                    }
                    ProductImage.Source = ImageSource.FromStream(() => {
                        var stream = file.GetStream();
                        imagePath = file.Path.ToString();
                        file.Dispose();
                        return stream;
                    });
                } else {
                    CrossPermissions.Current.OpenAppSettings();
                }
            } catch (Exception ex) {
                await DisplayAlert("Erro", "O sistema não tem os acessos necessários", "Ok");
            }
        }
    }
}