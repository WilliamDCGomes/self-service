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
            if (order.IdUser != 0 && order.IdProduct != 0 && order.ProductName != null && order.StatusOrder != null &&  order.LocationClient != null && order.OrderDate != null) {
                int result = 0;
                result = connection.Insert(order);
                if (result != 0) {
                    return true;
                }
            }
            return false;
        }

        public List<ModelOrder> ListOrder() {
            List<ModelOrder> orders = new List<ModelOrder>();
            try {
                orders = connection.Table<ModelOrder>().ToList();
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return orders;
        }

        public bool Update(ModelOrder orders) {
            try {
                int result = 0;
                result = connection.Update(orders);
                if (result != 0) {
                    return true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }

        public bool Delete(ModelOrder orders) {
            try {
                int result = 0;
                result = connection.Table<ModelOrder>().Delete(data => data.Id == orders.Id);
                if (result != 0) {
                    return true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }

        public List<ModelOrder> Locale(string date) {
            List<ModelOrder> list = new List<ModelOrder>();
            try {
                var data = from p in connection.Table<ModelOrder>() where p.OrderDate.Equals(date) select p;
                list = data.ToList();
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return list;
        }

        public ModelOrder GetOrder(ModelOrder orders) {
            ModelOrder order = new ModelOrder();
            try {
                order = connection.Table<ModelOrder>().First(n => n.IdUser == orders.IdUser);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return order;
        }
    }
}
