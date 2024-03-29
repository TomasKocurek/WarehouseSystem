﻿using Domain.Entities;

namespace Shared.Dto;
public class StockDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<StockItemDto> StockItems { get; set; } = new();
    public decimal Capacity { get; set; }
    public decimal CapacityPercentage { get; set; }
    public Position Position { get; set; }
}
