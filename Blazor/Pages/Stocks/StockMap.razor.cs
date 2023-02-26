using Microsoft.AspNetCore.Components;
using Shared.Dto;

namespace Blazor.Pages.Stocks;

public partial class StockMap : ComponentBase
{
    private int rowsCount = 10;
    private int columnsCount = 10;

    private string[,] stocksArray = new string[10, 10];

    protected override async Task<Task> OnInitializedAsync()
    {
        var stocksMapDto = await _stockService.GetStocksMap();
        CreateStocksArray(stocksMapDto);

        StateHasChanged();

        return base.OnInitializedAsync();
    }

    private void CreateStocksArray(StocksMapDto map)
    {
        int y = 0;
        foreach (var row in map.Rows)
        {
            int x = 0;
            foreach (var col in row.Columns)
            {
                if (col != null)
                {
                    stocksArray[y, x] = col;
                }
                x++;
            }

            y++;
        }
    }

    private string GetStockInfo(int row, int column)
    {
        if (row < stocksArray.GetLength(0) && column < stocksArray.GetLength(1))
        {
            return stocksArray[row, column];
        }
        else
        {
            return "";
        }
    }
}
