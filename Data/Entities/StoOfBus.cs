using System.ComponentModel;

namespace BusStation.Data.Entities
{
    public class StoOfBus
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string? Name { get; set; }

        [DisplayName("Адрес")]
        public string? Address { get; set; }

        [DisplayName("Телефон")]
        public int Phone { get; set; }
    }
}
