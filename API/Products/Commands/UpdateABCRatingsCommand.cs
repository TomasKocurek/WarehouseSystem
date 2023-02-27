using API.Services;
using MediatR;

namespace API.Products.Commands;

public record UpdateABCRatingsCommand : IRequest<Unit>;

public class UpdateABCRatingsCommandHandler : IRequestHandler<UpdateABCRatingsCommand, Unit>
{
    private readonly ABCService _abcService;

    public UpdateABCRatingsCommandHandler(ABCService abcService)
    {
        _abcService = abcService;
    }

    public async Task<Unit> Handle(UpdateABCRatingsCommand request, CancellationToken cancellationToken)
    {
        await _abcService.UpdateAllProductsRating();
        return Unit.Value;
    }
}
