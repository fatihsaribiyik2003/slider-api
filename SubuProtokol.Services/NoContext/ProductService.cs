using Microsoft.Extensions.Logging;
using SubuProtokol.Models;

namespace SubuProtokol.Services.NoContext
{
    public interface IProductService
    {
        ProductCreate Create(ProductCreate model);
    }

    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;

        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger;
        }

        public ProductCreate Create(ProductCreate model)
        {
            _logger.LogInformation("Product creating.. {Name} - {@Model}", model.Name, model);

            return model;
        }
    }
}
