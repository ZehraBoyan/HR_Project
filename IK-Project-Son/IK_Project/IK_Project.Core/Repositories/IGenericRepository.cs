using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.Repositories
{
	public interface IGenericRepository<T> where T: class
	{
		Task<T> GetByIdAsync(int id);
		IQueryable<T> GetAll();
		//productRepository.where(x=>x.id>5).OrderBy.ToListAsync();
		//Eğer IQueryable yazmazsak where sorgusundan hemen sonra db ye gider memory ye alır sonrasında order by ve listeleme yapar. ama Queryable kullanırsak hem order by hem list döner.

		IQueryable<T> Where(Expression<Func<T,bool>> expression);
		Task<bool> AnyAsync(Expression<Func<T,bool>> expression);
		Task AddAsync(T entity);

		//Birden fazla ekleme yapabilirim.
		Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
		void Update(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T>  entities);
		ICollection<T> GetDefault(Expression<Func<T, bool>> predicate);
		T GetDefaultSingle(Expression<Func<T, bool>> predicate);
	}
}
