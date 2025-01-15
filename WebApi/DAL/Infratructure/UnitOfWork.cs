﻿using DAL.Data;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Infratructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataDbContext _dbContext;
        private readonly ReportRepository _report;
        private readonly AdminRepository _admin;

        public DataDbContext Context => _dbContext;

        public ReportRepository Report => _report ?? new ReportRepository(_dbContext);
        public AdminRepository Admin=> _admin ?? new AdminRepository(_dbContext);

        public UnitOfWork(DataDbContext dbContext)
        {
            _dbContext = dbContext;
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

        GenericRepository<TEntity> IUnitOfWork.GenericRepository<TEntity>()
        {
            throw new NotImplementedException();
        }
    }
}
