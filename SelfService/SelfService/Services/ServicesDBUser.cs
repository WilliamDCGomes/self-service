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
    }
}
