using System;
using System.Collections.Generic;
using System.Text;
using SelfService.Models;
using SQLite;

namespace SelfService.Services {
    public class ServicesDBReservation {
        SQLiteConnection connection;
        public ServicesDBReservation(string dbPath) {
            if (dbPath == null) {
                dbPath = App.DbPath;
            }
            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<ModelReservation>();
        }
        public bool Insert(ModelReservation reservation) {
            if (reservation.IdUser != 0 && reservation.LocationClient != null && reservation.NumberSeats != 0 && reservation.ReservationDate != null) {
                int result = 0;
                result = connection.Insert(reservation);
                if (result != 0) {
                    return true;
                }
            }
            return false;
        }

        public List<ModelReservation> LocalePerDate(string date) {
            List<ModelReservation> list = new List<ModelReservation>();
            try {
                var data = from p in connection.Table<ModelReservation>() where p.ReservationDate.Equals(date) select p;
                list = data.ToList();
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return list;
        }

        public List<ModelReservation> ListReservation() {
            List<ModelReservation> reservations = new List<ModelReservation>();
            try {
                reservations = connection.Table<ModelReservation>().ToList();
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return reservations;
        }

        public bool Update(ModelReservation reservations) {
            try {
                int result = 0;
                result = connection.Update(reservations);
                if (result != 0) {
                    return true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }

        public bool Delete(ModelReservation reservations) {
            try {
                int result = 0;
                result = connection.Table<ModelReservation>().Delete(data => data.Id == reservations.Id);
                if (result != 0) {
                    return true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return false;
        }

        public List<ModelReservation> Locale(ModelReservation reservation) {
            List<ModelReservation> list = new List<ModelReservation>();
            try {
                var data = from p in connection.Table<ModelReservation>() where p.IdUser == reservation.IdUser select p;
                list = data.ToList();
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return list;
        }

        public ModelReservation GetReservation(ModelReservation reservations) {
            ModelReservation reservation = new ModelReservation();
            try {
                reservation = connection.Table<ModelReservation>().First(n => n.IdUser == reservations.IdUser);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return reservation;
        }
    }
}
