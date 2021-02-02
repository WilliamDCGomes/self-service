using System;
using System.Collections.Generic;
using System.Text;
using SelfService.Models;
using SQLite;

namespace SelfService.Services {
    public class ServicesDBProducts {
        SQLiteConnection connection;
        public ServicesDBProducts(string dbPath) {
            if(dbPath == null) {
                dbPath = App.DbPath;
            }
            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<ModelProducts>();
        }
        public void Insert(ModelProducts product) {
            if (product.Titulo != null && product.Price != 0) {
                int result = connection.Insert(product);
            } 
        }
    }
}
