using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfService.Models {
    [Table("Order")]
    public class ModelOrder {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int IdUser { get; set; }

        [NotNull]
        public int IdProduct { get; set; }

        [NotNull]
        public int StatusOrder { get; set; }

        [NotNull]
        public DateTime OrderDate { get; set; }

        [NotNull]
        public bool AlreadyPayed { get; set; }

        [NotNull]
        public string LocationClient { get; set; }

        [NotNull]
        public double Price { get; set; }

        public ModelOrder() {
            StatusOrder = 1;
            OrderDate = DateTime.Now;
            AlreadyPayed = false;
            Price = 0;
        }
    }
}
