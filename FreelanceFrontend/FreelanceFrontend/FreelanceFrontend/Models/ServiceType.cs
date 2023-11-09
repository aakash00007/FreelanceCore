namespace FreelanceFrontend.Models
{
    public class ServiceType
    {
        public int ServiceTypeId { get; set; }
        public string ServiceName { get; set; }
        public virtual ICollection<Service>? Service { get; set; }
    }
}
