using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IBranchRepository
    {
        int Create(Branch branch);
        Branch? Get(int id);
        List<Branch> GetAll();
        List<Branch> GetByUser(int userId);
        int Update(Branch branch);
        int Delete(Branch branch);
    }
}
