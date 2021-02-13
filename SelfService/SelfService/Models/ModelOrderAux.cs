using System;
using System.Collections.Generic;
using System.Text;

namespace SelfService.Models {
    public class ModelOrderAux{
        public int Id { get; set; }
        public long IdUser { get; set; }
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public string StatusOrder { get; set; }
        public string OrderDate { get; set; }
        public bool AlreadyPayed { get; set; }
        public string LocationClient { get; set; }
        public double Price { get; set; }
        public string ImageAdress { get; set; }

        public ModelOrderAux() {
            StatusOrder = "Solicitado";
            OrderDate = DateTime.Now.ToString("dd/MM/yyyy");
            AlreadyPayed = false;
            Price = 0;
            ImageAdress = "iconMenu";
        }
    }
}
