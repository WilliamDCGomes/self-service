using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfService.Models {
    [Table("Client")]
    public class ModelUser {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string LastName { get; set; }

        [NotNull]
        public string EmailAdress { get; set; }

        public string Phone { get; set; }

        public string Cep { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public string HouseNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [NotNull]
        public string Login { get; set; }

        [NotNull]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public ModelUser() {
            IsAdmin = false;
        }
    }
}
