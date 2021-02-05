using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfService.Models {
    [Table("Reservation")]
    public class ModelReservation {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int IdUser { get; set; }
        
        [NotNull]
        public string NameUser { get; set; }

        [NotNull]
        public DateTime ReservationDate { get; set; }

        [NotNull]
        public string LocationClient { get; set; }

        [NotNull]
        public int NumberSeats { get; set; }

        public ModelReservation() {
            NumberSeats = 1;
            ReservationDate = DateTime.Now;
        }
    }
}
