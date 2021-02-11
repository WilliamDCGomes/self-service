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
            user.Phone = InputPhoneNumber.Text;
            user.Cep = InputCEP.Text;
            user.Street = InputStreet.Text;
            user.Neighborhood = InputNeighborhood.Text;
            user.HouseNumber = InputHouseNumber.Text;
            user.City = InputCity.Text;
            user.State = PickerStates.SelectedItem.ToString();
            user.Login = InputLogin.Text;
            user.Password = InputPassword.Text;
            string[] emailCheck = InputEmailAdress.Text.Split('@');
            string[] emailCheckAux = emailCheck[1].Split('.');
            if (emailCheckAux[0].Equals("selfservice")) {
                user.IsAdmin = true;
            } 
            else {
                user.IsAdmin = false;
            }
            ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
            bool Worked = dbUser.Insert(user);
            if (Worked) {
                await DisplayAlert("Sucesso", "Cadastro realizado com sucesso", "OK");
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
                    DisplayAlert("Aviso", "A senha deve conter no mínimo 6 dígitos", "OK");
                }
                else if (dbUser.Locale(InputLogin.Text)) {
                    return true;
                } 
                else {
                    DisplayAlert("Aviso", "O Login que você está tentando cadastrar já está em uso", "OK");
                }
            } 
            else if (!(InputPassword.Text == RepetPasswordEntry.Text)) {
                DisplayAlert("Aviso", "As senhas não coincidem", "OK");
            }            
            else {
                DisplayAlert("Aviso", "Preencha todos os campos obrigatórios para se cadastrar", "OK");
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
                        await DisplayAlert("Erro", "Cep não localizado\n" + ex.Message, "OK");
                    }
                } 
                else {
                        await DisplayAlert("Erro", "O Cep deve ter 8 digítos", "OK");
                }
            }
        }

        private void OpenPicker(object sender, EventArgs e) {
            PickerStates.Focus();
        }
    }
}