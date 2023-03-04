namespace Shared.Dto;
public class StocksMapDto
{
    public StockRow[] Rows { get; set; }

    public StocksMapDto()
    {
        Rows = new StockRow[10];

        for (int i = 0; i < Rows.Length; i++)
        {
            Rows[i] = new StockRow();
        }
    }
}

public class StockRow
{
    public Cell[] Cells { get; set; }

    public StockRow()
    {
        Cells = new Cell[10];

        for (int i = 0; i < Cells.Length; i++)
        {
            Cells[i] = new Cell();
        }
    }
}

public class Cell
{
    public string Name { get; set; }
    public decimal CapacityPercentage { get; set; }
}