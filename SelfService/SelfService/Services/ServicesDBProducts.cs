using System;
using System.Collections.Generic;
using System.Text;
using SelfService.Models;
using SQLite;

namespace SelfService.Services {
    public class ServicesDBProducts {
        SQLiteConnection connection;
        public string StatusMessage { get; set; }
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
                this.StatusMessage = string.Format("{0} registro(s) adicionado(s)", result);
            } else {
                this.StatusMessage = string.Format("0 registro(s) adicionado(s)");
            }
        }
    }
}
