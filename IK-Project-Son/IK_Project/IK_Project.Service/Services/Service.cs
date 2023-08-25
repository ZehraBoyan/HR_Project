using IK_Project.Core.Repositories;
using IK_Project.Core.Services;
using IK_Project.Core.UnitOfWorks;
using IK_Project.Repository.UnitOfWork;
using IK_Project.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Service.Services
{
	public class Service<T> : IService<T> where T : class
	{
		private readonly IGenericRepository<T> _repository;
		private readonly IUnitOfWorks _unitOfWork;

		public Service(IGenericRepository<T> repository, IUnitOfWorks unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}
		public async Task<T> AddAsync(T entity)
		{

			await _repository.AddAsync(entity);
			await _unitOfWork.CommitAsync();
			return entity;
		}

		public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
		{
			await _repository.AddRangeAsync(entities);
			await _unitOfWork.CommitAsync();
			return entities;
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
		{
			return await _repository.AnyAsync(expression);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _repository.GetAll().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			var hasPersonel=await _repository.GetByIdAsync(id);
			if (hasPersonel==null)
			{
				throw new NotFoundException($"{typeof(T).Name} {id} not found");
			}
			return hasPersonel;
		}

		public ICollection<T> GetDefault(Expression<Func<T, bool>> predicate)
		{
			return _repository.GetDefault(predicate);
		}

		public T GetDefaultSingle(Expression<Func<T, bool>> predicate)
		{
			return _repository.GetDefaultSingle(predicate);
		}

		public async Task RemoveAsync(T entity)
		{
			_repository.Remove(entity);
			await _unitOfWork.CommitAsync();
		}


		public async Task RemoveRangeAsync(IEnumerable<T> entities)
		{
			_repository.RemoveRange(entities);
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_repository.Update(entity);
			await _unitOfWork.CommitAsync();
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			return _repository.Where(expression);
		}
	}
}