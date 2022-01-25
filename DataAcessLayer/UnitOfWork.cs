using DataAcessLayer.Interfaces;
using DataAcessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GroceryCloudContext _dbContext;

        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<Category> CategoryRepository { get; }

        public UnitOfWork(GroceryCloudContext dbContext,
            IGenericRepository<Product> productRepository,
            IGenericRepository<Category> categoryRepository
            )
        {
            _dbContext = dbContext;

            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
        }

        public GroceryCloudContext Context()
        {
            return _dbContext;
        }

        //public DatabaseFacade Database()
        //{
        //    return _dbContext.Database;
        //}

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
