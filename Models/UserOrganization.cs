namespace MyTaskTracker.Models
{
    public class UserOrganization
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
