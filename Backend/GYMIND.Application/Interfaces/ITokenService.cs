namespace GYMIND.API.GYMIND.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Entities.User user, IList<Entities.UserRole> userRoles);
    }
}
