
namespace VirusSpreadLibrary.Grid;

public class ColorTranslation
{
    public int CellState { get; set; }
    public SkiaSharp.SKColor CellColor { get; set; }
    public ColorTranslation()
    {
        CellState = 0;
        CellColor = new SkiaSharp.SKColor();
    }
    public ColorTranslation(int StateOfCell, SkiaSharp.SKColor ColorOfCell)
    {
        CellState = StateOfCell;
        CellColor = ColorOfCell;
    }
}
