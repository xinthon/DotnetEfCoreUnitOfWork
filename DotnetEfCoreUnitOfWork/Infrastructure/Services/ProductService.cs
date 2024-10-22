using DotnetEfCoreUnitOfWork.Common.Abstractions;
using DotnetEfCoreUnitOfWork.Infrastructure.Persistence;

namespace DotnetEfCoreUnitOfWork.Infrastructure.Services;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _unitOfWork.Repository<Product>().GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        return await _unitOfWork.Repository<Product>()
            .GetByIdAsync(productId);
    }

    public async Task AddProductAsync(Product product)
    {
        await _unitOfWork.Repository<Product>().AddAsync(product);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _unitOfWork.Repository<Product>().Update(product);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        var product = await _unitOfWork
            .Repository<Product>()
            .GetByIdAsync(productId);

        if (product != null)
        {
            _unitOfWork.Repository<Product>()
                .Remove(product);
            await _unitOfWork.CompleteAsync();
        }
    }
}
