using System.ComponentModel;

namespace BusStation.Data.Entities
{
    public class Route
    {
        public int Id { get; set; }

        public int RouteDayId { get; set; }
        [DisplayName("День недели")]
        public RouteDay? RouteDay { get; set; }


        [DisplayName("Остановки")]
        public List<BusStop> BusStops { get; set; } = new List<BusStop>();

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
