namespace GYMIND.API.GYMIND.API.DTOs
{
    public class CreateUserDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;

        public string? Location { get; set; }
        public DateTime? DateOfBirth { get; set; }


    }
}
