using DAL.Data;
using DAL.Repositories;

namespace DAL.Infratructure
{
    public interface IUnitOfWork
    {
        DataDbContext Context { get; }

        QuizzesRepository Quizzes { get; }
        //ProductRepository Product { get; }

        GenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        void Dispose();
    }
}
