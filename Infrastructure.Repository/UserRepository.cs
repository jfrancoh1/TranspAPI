using Domain;
using Infrastructure.Interface;
using Infrastructure.Data.DbContextEntity;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Create(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user.Id;
        }


        public User? Get(int id)
        {
            return _context.User.FirstOrDefault(c => c.Id == id);
        }

        public List<User> GetAll()
        {
            return _context.User.ToList();
        }

        public User? GetUserByIdAndPassword(Login login)
        {
            return _context.User.FirstOrDefault(c => c.Id == login.Id && c.Password == login.Password);
        }

        public int Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return user.Id;
        }


        public int Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
            return user.Id;
        }
    }
}



