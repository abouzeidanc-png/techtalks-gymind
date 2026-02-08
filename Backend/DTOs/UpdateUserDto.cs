namespace GYMIND.API.DTOs
{
    public class UpdateUserDto
    {
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public List<int>? RoleIDs { get; set; }
    }

}
