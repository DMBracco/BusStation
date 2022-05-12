using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusStation.Data.Entities
{
    public class Passenger
    {
        public int Id { get; set; }

        [DisplayName("ФИО")]
        public string? FIO { get; set; }

        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [DisplayName("Номер паспорта")]
        public int NumberPasssword { get; set; }

        [DisplayName("Серия паспорта")]
        public int SeriesPassword { get; set; }

        [DisplayName("Телефон")]
        public int Phone { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
