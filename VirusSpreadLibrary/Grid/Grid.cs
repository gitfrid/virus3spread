
namespace VirusSpreadLibrary.Grid;

public class Grid
{
    private int maxX;
    private int maxY;
    public Grid()
    {
        ColorList = new ColorList();
        Cells = new GridCell[,] { };
    }
    public GridCell[,] Cells { get; set; }
    public ColorList ColorList { get; set; }
    
    public void SetNewEmptyGrid(int MaxX, int MaxY)
    {
        maxX = MaxX;
        maxY = MaxY;
        Cells = new GridCell[maxX, maxY];
        //Microsoft.Maui.Graphics.Color Color = ColorList.GetCellColor(CellState.EmptyCell, 0,0);

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                this.Cells[x, y] = new GridCell();
            }
        }
    }
    public int ReturnMaxX()
    {
        return maxX;
    }
    public int ReturnMaxY()
    {
        return maxY;
    }
}
