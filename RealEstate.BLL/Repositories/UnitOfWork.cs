﻿using RealEstate.BLL.Repositories;
using RealEstate.BLL.Interfaces;
using RealEstate.BLL.InterFaces;
using RealEstate.DAL;

namespace RealEstate.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RealEstateDdContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(RealEstateDdContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
