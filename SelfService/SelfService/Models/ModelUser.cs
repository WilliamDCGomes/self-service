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

        [NotNull]
        public string LastName { get; set; }

        [NotNull]
        public string EmailAdress { get; set; }

        public string Phone { get; set; }

        [NotNull]
        public string Cep { get; set; }

        [NotNull]
        public string Street { get; set; }

        [NotNull]
        public string Neighborhood { get; set; }

        [NotNull]
        public string HouseNumber { get; set; }

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
