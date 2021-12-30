using BitstampApp.Core.Models;
using BitstampApp.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitstampApp.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(int id);
        void Remove(User model);
    }
}