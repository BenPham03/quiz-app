using DAL.Data;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.Infratructure
{
        public interface IUnitOfWork
        {
                DataDbContext Context { get; }

                //CategoryRepository Category { get; }
                // ReportRepository Report { get; }
                //ProductRepository Product { get; }
                IQuizRepository Quiz { get; }
                IQuestionRepository Question { get; }
                IAnswerRepository Answer { get; }
                IAttemptRepository Attempt { get; }
                IUserAnswerRepository UserAnswer { get; }
                IInteractionRepository Interaction { get; }
                ReportRepository Report { get; }
                AdminRepository Admin { get; }

                GenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;

                int SaveChanges();
                Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
                Task BeginTransactionAsync();
                Task CommitTransactionAsync();
                Task RollbackTransactionAsync();
                void Dispose();
        }
}
