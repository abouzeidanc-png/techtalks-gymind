using GYMIND.API.Entities;

namespace GYMIND.API.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(User user, IEnumerable<UserRole> userRoles);
    }
}
