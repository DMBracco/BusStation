using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusStation.Data.Entities
{
    public class BusSchedule
    {
        public int Id { get; set; }

        [DisplayName("Время отправления")]
        [DataType(DataType.Time)]
        public DateTime DepartureTime { get; set; }

        [DisplayName("Время прибытия")]
        [DataType(DataType.Time)]
        public DateTime ArrivalTime { get; set; }

        public int BusId { get; set; }
        [DisplayName("Автобус")]
        public Bus? Buses { get; set; }

        public int RouteId { get; set; }
        [DisplayName("Номер маршрута")]
        public Route? Routes { get; set; }

        public int PlatformId { get; set; }
        [DisplayName("Перон")]
        public Platform? Platforms { get; set; }
    }
}
