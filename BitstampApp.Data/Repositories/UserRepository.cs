using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitstampApp.Core.Models;
using BitstampApp.Core.Repositories;
using BitstampApp.Data;
using BitstampApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace BitstampApp.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) 
            : base(context)
        { }

#pragma warning disable CS0108 // 'UserRepository.GetAllAsync()' hides inherited member 'Repository<User>.GetAllAsync()'. Use the new keyword if hiding was intended.
        public async Task<IEnumerable<User>> GetAllAsync()
#pragma warning restore CS0108 // 'UserRepository.GetAllAsync()' hides inherited member 'Repository<User>.GetAllAsync()'. Use the new keyword if hiding was intended.
        {
            return await ApplicationContext.Users
                .ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await ApplicationContext.Users
                .SingleOrDefaultAsync(m => m.Id == id);
        }


        private ApplicationContext ApplicationContext
        {
            get { return Context as ApplicationContext; }
        }
    }
}