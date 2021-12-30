using System;
using System.Threading.Tasks;
using BitstampApp.Core.Repositories;
namespace BitstampApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int CommitAsync();
    }
}