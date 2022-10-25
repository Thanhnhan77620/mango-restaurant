using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mongo.Services.ProductAPI.DbContexts;
using Mongo.Services.ProductAPI.Models;
using Mongo.Services.ProductAPI.Models.Dto;

namespace Mongo.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
           var product=_mapper.Map<Product>(productDto);
            if (product.ProductId>0)
            {
                _db.Products.Update(product);
            }
            else {
                _db.Products.Add(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);

        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product =await _db.Products.FindAsync(productId);
                if (product != null)
                {
                    _db.Products.Remove(product);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _db.Products.FindAsync(productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
