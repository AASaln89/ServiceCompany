using ServiceCompany.DbStuff.Models;

namespace ManagementCompany.DbStuff.ExternalEntities
{
    public class LocationEntity : BaseEntityAbstract
    {
        public int LocationId { get; set; }

        public double Elevation { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string PlaceName { get; set; }

        public string WeatherStation { get; set; }

        public virtual DocumentEntity Document { get; set; }
    }
}
