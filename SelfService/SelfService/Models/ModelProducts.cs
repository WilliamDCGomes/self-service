using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfService.Models {
    [Table("Products")]
    public class ModelProducts {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Titulo { get; set; }

        [NotNull]
        public double Price { get; set; }

        public string Description { get; set; }

        public bool InDescont { get; set; }

        public double DescontPercent { get; set; }

        public string ImageAdress { get; set; }

        public ModelProducts() {
            InDescont = false;
            DescontPercent = 0;
        }
    }
}
