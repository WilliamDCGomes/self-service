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
        public long IdUser { get; set; }

        [NotNull]
        public int IdProduct { get; set; }

        public string ProductName { get; set; }

        [NotNull]
        public string StatusOrder { get; set; }

        [NotNull]
        public string OrderDate { get; set; }

        [NotNull]
        public bool AlreadyPayed { get; set; }

        [NotNull]
        public string LocationClient { get; set; }

        [NotNull]
        public double Price { get; set; }

        public ModelOrder() {
            StatusOrder = "Solicitado";
            OrderDate = DateTime.Now.ToString("dd/MM/yyyy");
            AlreadyPayed = false;
            Price = 0;
        }
    }
}
