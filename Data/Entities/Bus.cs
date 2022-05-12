using System.ComponentModel;

namespace BusStation.Data.Entities
{
    public class Bus
    {
        public int Id { get; set; }

        [DisplayName("Марка")]
        public string? Marka { get; set; }

        [DisplayName("Гос. номер")]
        public string? GosNumber { get; set; }

        [DisplayName("Количество мест")]
        public int NumberOfPlaces { get; set; }

        public int EmployeeId { get; set; }
        [DisplayName("Водитель")]
        public Employee? Employee { get; set; }

        public int StoOfBusId { get; set; }
        [DisplayName("Сто")]
        public StoOfBus? StoOfBus { get; set; }
    }
}
