﻿using DAL.Data;
using DAL.Repositories;

namespace DAL.Infratructure
{
    public interface IUnitOfWork
    {
        DataDbContext Context { get; }

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
