using RIG.ProductModule.Domain.Repositories;
using RIG.ProductModule.Infrastructure.Db.Repositories;
using RIG.Shared.Infrastructure.Db;

namespace RIG.ProductModule.Infrastructure.Db
{
    public class ProductDbContext : IProductDbContext
    {
        private readonly EfAppDbContext _appDbContext;

        public ProductDbContext(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IProductRepository ProductRepository => new ProductRepository(_appDbContext);
    }
}