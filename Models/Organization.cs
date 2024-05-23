namespace MyTaskTracker.Models
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; }
    
        public string Description { get; set; }

        public virtual ICollection<User> Employees { get; set; }

    }
}
