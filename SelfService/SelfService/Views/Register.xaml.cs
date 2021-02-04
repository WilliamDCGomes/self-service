using SelfService.Models;
using SelfService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Correios;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using JobSearch.App.Utility.Load;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage {
        public Register() {
            InitializeComponent();
        }
        private void BackLogin(object sender, EventArgs e) {
            App.Current.MainPage = new NavigationPage(new Login());
        }

        private void ShowAndHidePassword(object sender, EventArgs e) {
            InputPassword.IsPassword = !InputPassword.IsPassword;
            if (InputPassword.IsPassword) {
                ImagePasswordHideShow.Source = "PasswordTrue";
            } else {
                ImagePasswordHideShow.Source = "PasswordFalse";
            }
        }

        private void ShowAndHidePassword2(object sender, EventArgs e) {
            RepetPasswordEntry.IsPassword = !RepetPasswordEntry.IsPassword;
            if (RepetPasswordEntry.IsPassword) {
                ImagePasswordHideShow2.Source = "PasswordTrue";
            } else {
                ImagePasswordHideShow2.Source = "PasswordFalse";
            }
        }

        public async void InsertUser() {
            ModelUser user = new ModelUser();
            user.Name = InputName.Text;
            user.LastName = InputLastName.Text;
            user.EmailAdress = InputEmailAdress.Text;
            user.Phone = InputPhoneDDD.Text + InputPhoneNumber.Text;
            user.Cep = InputCEP.Text;
            user.Street = InputStreet.Text;
            user.Neighborhood = InputNeighborhood.Text;
            user.HouseNumber = InputHouseNumber.Text;
            user.Login = InputLogin.Text;
            user.Password = InputPassword.Text;
            if (InputLogin.Text.Equals("Admin")) {
                user.IsAdmin = true;
            }
            ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
            bool Worked = dbUser.Insert(user);
            if (Worked) {
                await DisplayAlert("SUCESSO", "CADASTRO REALIZADO COM SUCESSO", "OK");
                await Navigation.PopAsync();
            }
        }

        private void CreateAccount(object sender, EventArgs e) {
            if (DataValidation()) {
                InsertUser();
            } 
        }
        private bool DataValidation() {
            ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
            if (InputName.Text != null && InputLastName.Text != null && InputEmailAdress.Text != null && InputCEP.Text != null && InputStreet.Text != null && InputNeighborhood.Text != null && InputHouseNumber.Text != null && InputLogin.Text != null && InputPassword.Text != null && RepetPasswordEntry.Text != null && InputPassword.Text.Equals(RepetPasswordEntry.Text)) {
                if (InputPassword.Text.Length < 6) {
                    DisplayAlert("AVISO", "A SENHA DEVE CONTER NO MÍNIMO 6 DÍGITOS", "OK");
                }
                else if (dbUser.Locale(InputLogin.Text)) {
                    return true;
                } 
                else {
                    DisplayAlert("AVISO", "O LOGIN QUE VOCÊ ESTÁ TENTANDO CADASTRAR JÁ ESTÁ EM USO", "OK");
                }
            } 
            else if (!(InputPassword.Text == RepetPasswordEntry.Text)) {
                DisplayAlert("AVISO", "AS SENHAS NÃO COINCIDEM", "OK");
            }            
            else {
                DisplayAlert("AVISO", "PREENCHA TODOS OS CAMPOS OBRIGATÓRIOS PARA SE CADASTRAR", "OK");
            }
            return false;
        }

        private async void GetCep(object sender, FocusEventArgs e) {
            if (!string.IsNullOrEmpty(InputCEP.Text)) {
                if (InputCEP.Text.Length > 7 && InputCEP.Text.Length < 9) {
                    try {
                        await Navigation.PushPopupAsync(new Loading());
                        CorreiosApi deliveryApi = new CorreiosApi();
                        var data = deliveryApi.consultaCEP(InputCEP.Text);
                        InputStreet.Text = data.end;
                        InputNeighborhood.Text = data.bairro;
                        InputCity.Text = data.cidade;
                        PickerStates.SelectedItem = data.uf;
                        await Navigation.PopAllPopupAsync();
                    } catch (Exception ex) {
                        await DisplayAlert("ERRO", "CEP NÃO LOCALIZADO\n" + ex.Message, "OK");
                    }
                } 
                else {
                        await DisplayAlert("ERRO", "O CEP DEVE TER 8 DIGÍTOS", "OK");
                }
            }
        }

        private void OpenPicker(object sender, EventArgs e) {
            PickerStates.Focus();
        }
    }
}