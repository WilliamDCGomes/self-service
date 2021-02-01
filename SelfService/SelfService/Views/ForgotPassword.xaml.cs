using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : ContentPage {
        public ForgotPassword() {
            InitializeComponent();
        }

        private void CloseForgotPassword(object sender, EventArgs e) {
            Navigation.PopModalAsync();
        }
    }
}