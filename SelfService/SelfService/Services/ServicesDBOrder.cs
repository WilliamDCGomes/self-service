using SelfService.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfService.Services {
    public class ServicesDBOrder {
        SQLiteConnection connection;
        public string StatusMessage { get; set; }
        public ServicesDBOrder(string dbPath) {
            if (dbPath == null) {
                dbPath = App.DbPath;
            }
            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<ModelOrder>();
        }
        public void Insert(ModelOrder order) {
            if (order.IdUser != 0 && order.IdProduct != 0 && order.StatusOrder != 0 &&  order.LocationClient != null && order.OrderDate != null) {
                int result = connection.Insert(order);
                this.StatusMessage = string.Format("{0} registro(s) adicionado(s)", result);
            } else {
                this.StatusMessage = string.Format("0 registro(s) adicionado(s)");
            }
        }
    }
}
