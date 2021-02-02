using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfService.Models {
    [Table("Historic")]
    public class ModelHistoricOrders {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int IdUser { get; set; }

        [NotNull]
        public string Titulo { get; set; }

        [NotNull]
        public double Price { get; set; }

        public DateTime OrderDate { get; set; }

        public string LocationClient { get; set; }

        public string ImageAdress { get; set; }

        public ModelHistoricOrders() {
            OrderDate = DateTime.Now;
        }
    }
}
