using FreelanceDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceDAL.Repository.Interfaces
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task <IEnumerable<Service>> GetServicesByUserId (string userId);
    }
}
