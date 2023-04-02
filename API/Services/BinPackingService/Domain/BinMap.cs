namespace API.Services.BinPackingService.Domain;

public class BinMap
{
    public bool[,] Map { get; set; }

    public BinMap(int width, int depth, decimal ratio)
    {
        //todo
        //int mapWidth = (int)Math.Floor(width * ratio);
        //int mapDepth = (int)Math.Floor(depth * ratio);
        Map = new bool[depth, width];
    }
}
