using FreelanceDAL.DBContext;
using FreelanceDAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceDAL.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDBContext _context;
        private DbSet<T> table = null;

        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }
        public async Task Insert(T obj)
        {
            await table.AddAsync(obj);
        }

        public void Update(T obj)
        {
             table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task Delete(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
