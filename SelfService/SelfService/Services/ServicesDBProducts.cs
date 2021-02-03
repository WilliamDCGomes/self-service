using System;
using System.Collections.Generic;
using System.Text;
using SelfService.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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

        public bool Insert(ModelProducts product) {
            if (product.Title != null && product.Price != 0) {
                int result = 0;
                result = connection.Insert(product);
                if (result != 0) {
                    return true;
                }
            }
            return false;
        }

        public List<ModelProducts> ListProducts() {
            List<ModelProducts> products = new List<ModelProducts>();
            try {
                products = connection.Table<ModelProducts>().ToList();
            }
            catch(Exception e) {
                throw new Exception(e.Message);
            }
            return products;
        }

        public bool Update(ModelProducts products) {
            try {
                int result = 0;
                result = connection.Update(products);
                if (result != 0) {
                    return true;
                }
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }

        public bool Delete(ModelProducts products) {
            try {
                int result = 0;
                result = connection.Table<ModelProducts>().Delete( data => data.Id == products.Id );
                if (result != 0) {
                    return true;
                }
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }
    
        public List<ModelProducts> Locale(ModelProducts products) {
            List<ModelProducts> list = new List<ModelProducts>();
            try {
                var data = from p in connection.Table<ModelProducts>() where p.Title.ToLower().Contains(products.Title.ToLower()) select p;
                list = data.ToList();
            }
            catch(Exception e) {
                throw new Exception(e.Message);
            }
            return list;
        }

        public ModelProducts GetProduct(ModelProducts products) {
            ModelProducts product = new ModelProducts();
            try {
                product = connection.Table<ModelProducts>().First(n => n.Id == products.Id);
            }
            catch(Exception e) {
                throw new Exception(e.Message);
            }
            return product;
        }
    
    }
}
