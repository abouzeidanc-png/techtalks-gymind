namespace GYMIND.API.DTOs
{
    public class GymDto
    {
        public Guid GymId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}