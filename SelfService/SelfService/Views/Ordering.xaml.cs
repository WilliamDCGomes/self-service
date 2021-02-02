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
        public Ordering() {
            InitializeComponent();
        }

        private void Scanneing(ZXing.Result result) {
            Device.BeginInvokeOnMainThread(() => {
                resultScanner.Text = result.Text;
            });
        }
    }
}