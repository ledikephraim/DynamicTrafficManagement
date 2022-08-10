namespace ManagementAPI.Data.Models
{
    public class Intersection
    {
        public int Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public virtual Region Region { get; set; }

    }
}
