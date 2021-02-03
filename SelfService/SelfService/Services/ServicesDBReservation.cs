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
    }
}
