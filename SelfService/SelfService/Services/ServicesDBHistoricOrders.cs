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
        public bool Insert(ModelHistoricOrders historicOrders) {
            if (historicOrders.IdUser != 0 && historicOrders.Titulo != null && historicOrders.Price != 0) {
                int result = 0;
                result = connection.Insert(historicOrders);
                if (result != 0) {
                    return true;
                }
            }
            return false;
        }

        public List<ModelHistoricOrders> ListHistoricOrder() {
            List<ModelHistoricOrders> historicOrders = new List<ModelHistoricOrders>();
            try {
                historicOrders = connection.Table<ModelHistoricOrders>().ToList();
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return historicOrders;
        }

        public bool Update(ModelHistoricOrders historicOrders) {
            try {
                int result = 0;
                result = connection.Update(historicOrders);
                if (result != 0) {
                    return true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }

        public bool Delete(ModelHistoricOrders historicOrders) {
            try {
                int result = 0;
                result = connection.Table<ModelHistoricOrders>().Delete(data => data.Id == historicOrders.Id);
                if (result != 0) {
                    return true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }

        public List<ModelHistoricOrders> Locale(ModelHistoricOrders historicOrder) {
            List<ModelHistoricOrders> list = new List<ModelHistoricOrders>();
            try {
                var data = from p in connection.Table<ModelHistoricOrders>() where p.IdUser == historicOrder.IdUser select p;
                list = data.ToList();
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return list;
        }

        public ModelHistoricOrders GetHistoricOrder(ModelHistoricOrders historicOrders) {
            ModelHistoricOrders historicOrder = new ModelHistoricOrders();
            try {
                historicOrder = connection.Table<ModelHistoricOrders>().First(n => n.IdUser == historicOrders.IdUser);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return historicOrder;
        }
    }
}
