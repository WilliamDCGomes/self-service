using SelfService.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfService.Services {
    public class ServicesDBOrder {
        SQLiteConnection connection;
        public ServicesDBOrder(string dbPath) {
            if (dbPath == null) {
                dbPath = App.DbPath;
            }
            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<ModelOrder>();
        }
        public bool Insert(ModelOrder order) {
            if (order.IdUser != 0 && order.IdProduct != 0 && order.StatusOrder != 0 &&  order.LocationClient != null && order.OrderDate != null) {
                int result = 0;
                result = connection.Insert(order);
                if (result != 0) {
                    return true;
                }
            }
            return false;
        }
    }
}
