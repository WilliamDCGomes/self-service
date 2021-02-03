using System;
using System.Collections.Generic;
using System.Text;
using SelfService.Models;
using SQLite;

namespace SelfService.Services {
    public class ServicesDBUser {
        SQLiteConnection connection;
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

        public List<ModelUser> Locale(ModelUser users) {
            List<ModelUser> list = new List<ModelUser>();
            try {
                var data = from p in connection.Table<ModelUser>() where p.Name.ToLower().Contains(users.Name.ToLower()) select p;
                list = data.ToList();
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return list;
        }

        public ModelUser GetUser(ModelUser users) {
            ModelUser user = new ModelUser();
            try {
                user = connection.Table<ModelUser>().First(n => n.Id == users.Id);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return user;
        }
    }
}
