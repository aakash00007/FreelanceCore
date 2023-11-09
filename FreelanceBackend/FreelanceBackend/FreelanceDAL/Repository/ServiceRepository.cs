using FreelanceDAL.DBContext;
using FreelanceDAL.GenericRepository;
using FreelanceDAL.Models;
using FreelanceDAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceDAL.Repository
{
    public class ServiceRepository : GenericRepository<Service>,IServiceRepository
    {
        public ServiceRepository(ApplicationDBContext dbContext) : base(dbContext) { }
        public async Task<IEnumerable<Service>> GetServicesByUserId(string userId)
        {
            return _context.Services.Where(x=> x.UserId == userId);

        }
    }
}
