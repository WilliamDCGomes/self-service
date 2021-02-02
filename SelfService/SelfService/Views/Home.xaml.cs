using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage {
        public Home() {
            InitializeComponent();
        }

        private void Lista01_ItemTapped(object sender, ItemTappedEventArgs e) {

        }

        private void Lista01_Refreshing(object sender, EventArgs e) {

        }

        private void MenuItem_Clicked(object sender, EventArgs e) {

        }
    }
}