using Correios;
using JobSearch.App.Utility.Load;
using Rg.Plugins.Popup.Extensions;
using SelfService.Models;
using SelfService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAccount : ContentPage {
        ModelUser User;
        ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
        public EditAccount(long idUser) {
            InitializeComponent();
            User = dbUser.LocaleByID(idUser);
            InputName.Text = User.Name;
            InputLastName.Text = User.LastName;
            InputEmailAdress.Text = User.EmailAdress;
            InputPhoneNumber.Text = User.Phone;
            InputCEP.Text = User.Cep;
            InputStreet.Text = User.Street;
            InputHouseNumber.Text = User.HouseNumber;
            InputNeighborhood.Text = User.Neighborhood;
            InputCity.Text = User.City;
            PickerStates.SelectedItem = User.State;
            InputLogin.Text = User.Login;
        }

        private void Back(object sender, EventArgs e) {
            Navigation.PopAsync();
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

        public async void Update() {
            ModelUser user = new ModelUser();
            user.Id = User.Id;
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
            if (InputLogin.Text.Equals("Admin")) {
                user.IsAdmin = true;
            }
            ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
            bool Worked = dbUser.Update(user);
            if (Worked) {
                await DisplayAlert("Sucesso", "Atualização resalizada com sucesso", "OK");
                await Navigation.PopAsync();
            }
        }

        private void UpdateAccount(object sender, EventArgs e) {
            if (DataValidation()) {
                Update();
            }
        }

        public static bool IsValidEmailAddress(string s) {
            var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            return regex.IsMatch(s);
        }

        private bool DataValidation() {
            ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
            if (InputName.Text != null && InputLastName.Text != null && InputEmailAdress.Text != null && InputCEP.Text != null && InputStreet.Text != null && InputNeighborhood.Text != null && InputHouseNumber.Text != null && InputLogin.Text != null && InputPassword.Text != null && RepetPasswordEntry.Text != null && InputPassword.Text.Equals(RepetPasswordEntry.Text)) {
                if (InputPassword.Text.Length < 6) {
                    DisplayAlert("Aviso", "A senha deve conter no mínimo 6 dígitos", "OK");
                } 
                else if (!IsValidEmailAddress(InputEmailAdress.Text)) {
                    DisplayAlert("Aviso", "O E-mail que você digitou não é válido!", "OK");
                }
                else if (dbUser.Locale(InputLogin.Text)) {
                    return true;
                } else {
                    if (User.Login.Equals(InputLogin.Text)) {
                        return true;
                    }
                    DisplayAlert("Aviso", "O login que você está tentando cadastrar já está em uso", "OK");
                }
            } else if (!(InputPassword.Text == RepetPasswordEntry.Text)) {
                DisplayAlert("Aviso", "AS SENHAS NÃO COINCIDEM", "OK");
            } else {
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
                } else {
                    await DisplayAlert("Erro", "O Cep deve ter 8 digítos", "OK");
                }
            }
        }

        private void OpenPicker(object sender, EventArgs e) {
            PickerStates.Focus();
        }

        private async void DeleteAccount(object sender, EventArgs e) {
            var delete = await DisplayAlert("Aviso", "Deseja mesmo excluir sua conta?", "SIM", "NÃO");
            if (delete) {
                ServicesDBUser dbUser = new ServicesDBUser(App.DbPath);
                bool Worked = dbUser.Delete(User);
                if (Worked) {
                    await DisplayAlert("Sucesso", "Conta excluida com sucesso", "OK");
                    App.Current.MainPage = new NavigationPage(new Login());
                }
            }
        }
    }
}