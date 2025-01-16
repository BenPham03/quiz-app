using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using System.Collections.Concurrent;

namespace DAL.Infratructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataDbContext _dbContext;

        // Sử dụng ConcurrentDictionary để quản lý các repository chung
        private readonly ConcurrentDictionary<Type, object> _repositories = new();
        public DataDbContext Context => _dbContext;

        public QuizzesRepository Quizzes => new QuizzesRepository(_dbContext);

        public UnitOfWork(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public GenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            // Kiểm tra nếu repository đã tồn tại
            if (!_repositories.ContainsKey(typeof(TEntity)))
            {
                var repositoryInstance = new GenericRepository<TEntity>(_dbContext);
                _repositories[typeof(TEntity)] = repositoryInstance;
            }

            // Trả về repository từ dictionary
            return (GenericRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        // Sửa lại phương thức giao diện
        GenericRepository<TEntity> IUnitOfWork.GenericRepository<TEntity>()
        {
            return GenericRepository<TEntity>();
        }
    }
}
