namespace ManagementAPI.Data.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Intersection> Intersections { get; set; }
    }
}
