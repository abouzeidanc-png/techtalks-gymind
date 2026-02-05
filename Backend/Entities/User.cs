namespace GYMIND.API.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

public class User
{
    public Guid UserID { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string PasswordHash { get; set; } = null!;
    public int RoleID { get; set; }
    public string? Location { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Guid? MembershipID { get; set; }
    public string? Gender { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }

    public Role Role { get; set; } = null!;
}



