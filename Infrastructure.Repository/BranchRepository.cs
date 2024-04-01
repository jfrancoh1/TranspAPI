using Domain;
using Infrastructure.Data.DbContextEntity;
using Infrastructure.Interface;

namespace Infrastructure.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Create(Branch branch)
        {
            _context.Add(branch);
            _context.SaveChanges();
            return branch.Id;
        }


        public Branch? Get(int id)
        {
            return _context.Branch.FirstOrDefault(c => c.Id == id);
        }

        public List<Branch> GetAll()
        {
            return _context.Branch.ToList();
        }

        public List<Branch> GetByUser(int userId)
        {
            return _context.Branch.Where(c => c.UserId == userId).ToList();
        }

        public int Update(Branch branch)
        {
            _context.Update(branch);
            _context.SaveChanges();
            return branch.Id;
        }


        public int Delete(Branch branch)
        {
            _context.Remove(branch);
            _context.SaveChanges();
            return branch.Id;
        }
    }
}
