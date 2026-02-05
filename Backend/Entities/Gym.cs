namespace GYMIND.API.Entities
{
    public class Gym
    {
        public Guid GymId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
