using BitstampApp.Core;
using BitstampApp.Core.Repositories;
using BitstampApp.Data.Repositories;
using System.Threading.Tasks;
 
namespace BitstampApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private UserRepository _userRepository;
        public UnitOfWork(ApplicationContext context)
        {
            this._context = context;
        }

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
 
        public int CommitAsync()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}