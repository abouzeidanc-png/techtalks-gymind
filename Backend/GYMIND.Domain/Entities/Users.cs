namespace GYMIND.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Users
{
    public Guid userID { get; set; }  // matches your table PK
    public string fullName { get; set; }
    public string email { get; set; }

    public ICollection<UserAppMetadata> AppMetadata { get; set; } = new List<UserAppMetadata>();
}