using Domain;

namespace Infrastructure.Interface
{
    public interface IUserRepository
    {
        int Create(User user);
        User? Get(int id);
        List<User> GetAll();
        User? GetUserByIdAndPassword(Login login);
        int Update(User user);
        int Delete(User user);
    }
}
