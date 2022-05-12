using System.ComponentModel;

namespace BusStation.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [DisplayName("Имя")]
        public string? Name { get; set; }

        [DisplayName("Фамилия")]
        public string? Surname { get; set; }

        [DisplayName("Адрес")]
        public string? Address { get; set; }

        [DisplayName("Номер")]
        public int Phone { get; set; }

        [DisplayName("Должность")]
        public string? Position { get; set; }
    }
}
