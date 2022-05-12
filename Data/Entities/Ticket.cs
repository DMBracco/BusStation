using System.ComponentModel;

namespace BusStation.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        [DisplayName("Место")]
        public int Seat { get; set; }

        public int PassengerId { get; set; }
        [DisplayName("Пассажир")]
        public Passenger? Passengers { get; set; }

        public int RouteId { get; set; }
        [DisplayName("Маршрут")]
        public Route? Routes { get; set; }
    }
}
