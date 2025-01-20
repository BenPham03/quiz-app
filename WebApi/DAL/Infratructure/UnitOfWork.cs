using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace DAL.Infratructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataDbContext _dbContext;
        //private readonly CategoryRepository _category;
        private readonly ReportRepository _report;
        private readonly AdminRepository _admin;

        // Sử dụng ConcurrentDictionary để quản lý các repository chung
        private readonly ConcurrentDictionary<Type, object> _repositories = new();
        public DataDbContext Context => _dbContext;

        //public CategoryRepository Category => _category ?? new CategoryRepository(_dbContext);
        public ReportRepository Report => _report ?? new ReportRepository(_dbContext);

        public IQuizRepository Quiz { get; private set; }

        public IQuestionRepository Question { get; private set; }

        public IAnswerRepository Answer { get; private set; }

        public IAttemptRepository Attempt { get; private set; }

        public IUserAnswerRepository UserAnswer { get; private set; }

        public IInteractionRepository Interaction { get; private set; }

        //public ProductRepository Product => _product ?? new ProductRepository(_dbContext);
        public AdminRepository Admin => _admin ?? new AdminRepository(_dbContext);
        public QuizzesRepository Quizzes => new QuizzesRepository(_dbContext);
        private AttemptRepository _attempts;

        public UnitOfWork(DataDbContext dbContext)
        {
            _dbContext = dbContext;
            Quiz = new QuizRepository(_dbContext);
            Question = new QuestionRepository(_dbContext);
            Answer = new AnswerRepository(_dbContext);
            Attempt = new AttemptRepository(_dbContext);
            UserAnswer = new UserAnswerRepository(_dbContext);
            Interaction = new InteractionRepository(_dbContext);
        }

        public AttemptRepository Attempts
        {
            get { return _attempts ??= new AttemptRepository(_dbContext); }
        }

        public GenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_dbContext);
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
            return new GenericRepository<TEntity>(_dbContext);
        }
    }
}

