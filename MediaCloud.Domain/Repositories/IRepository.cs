﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MediaCloud.Domain.Repositories {

    public interface IRepository<TEntity> where TEntity : class {

        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
	    Task AddOrUpdateInclusive(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}