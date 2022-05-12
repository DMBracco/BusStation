using System.ComponentModel;

namespace BusStation.Data.Entities
{
    public class BusStop
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string? Name { get; set; }
    }
}
