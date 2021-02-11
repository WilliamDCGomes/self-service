using System;
using System.Collections.Generic;
using System.Text;
using SelfService.Models;
using SQLite;

namespace SelfService.Services {
    public class ServicesDBUser {
        SQLiteConnection connection;
        public int IdUser { get; set; }
        public ServicesDBUser(string dbPath) {
            if (dbPath == null) {
                dbPath = App.DbPath;
            }
            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<ModelUser>();

        }
        public bool Insert(ModelUser user) {
            if (user.Name != null && user.LastName != null && user.EmailAdress != null && user.Cep != null && user.Street != null && user.Neighborhood != null && user.HouseNumber != null && user.Login != null && user.Password != null) {
                int result = 0;
                result = connection.Insert(user);
                if(result != 0) {
                    return true;
                }
            }
            return false;
        }

        public List<ModelUser> ListUsers() {
            List<ModelUser> users = new List<ModelUser>();
            try {
                users = connection.Table<ModelUser>().ToList();
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return users;
        }

        public ModelUser GetUser() {
            List<ModelUser> users = new List<ModelUser>();
            var user = new ModelUser();
            try {
                users = connection.Table<ModelUser>().ToList();
                user = users[0];
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return user;
        }

        public bool Update(ModelUser users) {
            try {
                int result = 0;
                result = connection.Update(users);
                if (result != 0) {
                    return true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }

        public bool Delete(ModelUser users) {
            try {
                int result = 0;
                result = connection.Table<ModelUser>().Delete(data => data.Id == users.Id);
                if (result != 0) {
                    return true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }

        public ModelUser LocaleByID(int id) {
            List<ModelUser> list = new List<ModelUser>();
            ModelUser user = new ModelUser();
            try {
                var data = from p in connection.Table<ModelUser>() where p.Id == id select p;
                list = data.ToList();
                foreach(ModelUser i in list) {
                    user = i;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return user;
        }
        

        public bool Locale(string login) {
            List<ModelUser> list = new List<ModelUser>();
            try {
                var data = from p in connection.Table<ModelUser>() where p.Login.ToLower().Contains(login.ToLower()) select p;
                list = data.ToList();
                if(list.Count == 0) {
                    return true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }

        public bool checkUserExist(string login, string password) {
            List<ModelUser> loginAccount = new List<ModelUser>();
            List<ModelUser> passwordAccount = new List<ModelUser>();
            try {
                var dataLogin = from p in connection.Table<ModelUser>() where p.Login.ToLower().Contains(login.ToLower()) select p;
                loginAccount = dataLogin.ToList();
                var dataPassword = from p in connection.Table<ModelUser>() where p.Password.Contains(password) select p;
                passwordAccount = dataPassword.ToList();
                if (loginAccount.Count != 0 && passwordAccount.Count != 0) {
                    foreach (ModelUser i in loginAccount) {
                        IdUser = i.Id;
                        return true;
                    }
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }
    }
}
