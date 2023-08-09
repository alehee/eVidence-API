namespace eVidence_API.Models
{
    public class Account
    {
        public int Id { get; init; }
        public Entity Entity { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public Department? Department { get; init; }
    }
}
