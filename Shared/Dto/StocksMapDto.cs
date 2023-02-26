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
    public string[] Columns { get; set; } = new string[10];
}
