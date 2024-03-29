﻿using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Pages.Stocks;

public partial class StockDetail : ComponentBase
{
    private FormModel model = new();

    private async Task SaveStock()
    {
        var command = new
        {
            model.Name,
            model.Description,
            model.Capacity,
            Position = new
            {
                X = model.PositionX,
                Y = model.PositionY,
            }
        };

        await _stocksService.CreateNewStock(command);
        GoBack();
    }

    private void GoBack()
    {
        _navManager.NavigateTo("stocks");
    }
}

internal class FormModel
{
    [Required(ErrorMessage = "Enter name")]
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    [Range(1, int.MaxValue, ErrorMessage = "Enter number bigger then 0")]
    public decimal Capacity { get; set; }
    [Required(ErrorMessage = "Enter X position")]
    public int PositionX { get; set; }
    [Required(ErrorMessage = "Enter Y position")]
    public int PositionY { get; set; }

    public FormModel()
    {
        Description = string.Empty;
    }

    public FormModel(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
