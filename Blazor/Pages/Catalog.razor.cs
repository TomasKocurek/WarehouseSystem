using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Shared.Dto;

namespace Blazor.Pages;

public partial class Catalog : ComponentBase
{
    private Task<GridDataProviderResult<ProductDto>> GetGridData(GridDataProviderRequest<ProductDto> request)
    {
        //todo
        List<ProductDto> data = new()
        {
            new() { Id = Guid.NewGuid(), Name = "Test product 1" },
            new() { Id = Guid.NewGuid(), Name = "Test product 2" }
        };
        return Task.FromResult(new GridDataProviderResult<ProductDto>
        {
            Data = data,
            TotalCount = data.Count()
        });
    }
}