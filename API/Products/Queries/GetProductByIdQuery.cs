using Domain.Entities;
using Infrastructure.Repositories.ProductRepositories;
using MediatR;

namespace API.Products.Queries
{
    public record class GetProductByIdQuery(string Id) : IRequest<Product?>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return _productRepository.Get(new(request.Id));
        }
    }
}
