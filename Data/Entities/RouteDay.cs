using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusStation.Data.Entities
{
    public class RouteDay
    {
        public int Id { get; set; }

        [DisplayName("День недели")]
        public string? Day { get; set; }

        public List<Route> Routes { get; set; } = new List<Route>();
    }
}
