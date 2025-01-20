using DAL.Infratructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BLL.Services.Base
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(T entity)
        {
            if (entity != null)
            {
                _unitOfWork.GenericRepository<T>().Add(entity);
                return await _unitOfWork.SaveChangesAsync();
            }
            throw new ArgumentNullException(nameof(entity));
        }

        public bool Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                _unitOfWork.GenericRepository<T>().Delete(id);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
            throw new ArgumentNullException(nameof(id));
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id != Guid.Empty)
            {
                _unitOfWork.GenericRepository<T>().Delete(id);
                return await _unitOfWork.SaveChangesAsync() > 0 ? true : false;
            }
            throw new ArgumentNullException(nameof(id));
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            if (entity != null)
            {
                _unitOfWork.GenericRepository<T>().Delete(entity);
                return await _unitOfWork.SaveChangesAsync() >= 0 ? true : false;
            }
            throw new ArgumentNullException(nameof(entity));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _unitOfWork.GenericRepository<T>().GetAllAsync();
        }

        public async Task<PaginatedResult<T>> GetAsync(Expression<Func<T, bool>>? filter = null,Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,string includeProperties = "",int pageIndex = 1,int pageSize = 10)
        {
            // Truy vấn dữ liệu ban đầu
            var query = _unitOfWork.GenericRepository<T>().Get(filter, orderBy);

            // Xử lý Include các bảng liên quan
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            // Tạo kết quả phân trang
            return await PaginatedResult<T>.CreateAsync(query, pageIndex, pageSize);
        }


        public async Task<T?> GetByIdAsync(Guid id, string includeProperties = "")
        {
            // Lấy dữ liệu từ repository với `includeProperties`
            var query = _unitOfWork.GenericRepository<T>().Get(
                filter: x => EF.Property<Guid>(x, "Id") == id // Sử dụng EF.Property để truy vấn
                
            );
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.FirstOrDefaultAsync();
        }




        public async Task<bool> UpdateAsync(T entity, Func<T, Task> updateRelatedEntities = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // Cập nhật các thực thể liên quan trước khi lưu thay đổi
            if (updateRelatedEntities != null)
            {
                await updateRelatedEntities(entity);
            }

            // Cập nhật thực thể chính
            _unitOfWork.GenericRepository<T>().Update(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

    }
}
