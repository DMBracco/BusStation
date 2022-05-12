using System.ComponentModel;

namespace BusStation.Data.Entities
{
    public class Platform
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string? Name { get; set; }
    }
}
