using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage {
        public RegisterPage() {
            InitializeComponent();
        }

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
    }
}