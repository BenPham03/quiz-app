using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Base
{
    public interface IBaseService<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity, Func<T, Task> updateRelatedEntities = null);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAsync(T entity);
        Task<T?> GetByIdAsync(Guid id,string includeProperties = "");
        Task<IEnumerable<T>> GetAllAsync();
        Task<PaginatedResult<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 10);
    }
}
