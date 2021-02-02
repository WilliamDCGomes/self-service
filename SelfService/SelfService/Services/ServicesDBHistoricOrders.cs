using SelfService.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfService.Services {
    public class ServicesDBHistoricOrders {
        SQLiteConnection connection;
        public ServicesDBHistoricOrders(string dbPath) {
            if (dbPath == null) {
                dbPath = App.DbPath;
            }
            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<ModelHistoricOrders>();
        }
        public void Insert(ModelHistoricOrders historicOrders) {
            if (historicOrders.IdUser != 0 && historicOrders.Titulo != null && historicOrders.Price != 0) {
                int result = connection.Insert(historicOrders);
            } 
        }
    }
}
