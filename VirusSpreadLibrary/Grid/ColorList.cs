using VirusSpreadLibrary.Enum;
using VirusSpreadLibrary.AppProperties;
using SkiaSharp;


namespace VirusSpreadLibrary.Grid;


public static class CellStateExtions
{
    // Create a static dictionary to store the colors
    private static readonly Dictionary<CellState, SKColor> colorMap;

    // Initialize the dictionary in a static constructor
    static CellStateExtions()
    {
        colorMap = new Dictionary<CellState, SKColor>();
        colorMap.Add(CellState.PersonsHealthyOrRecoverd, SkiaSharp.SKColor.Parse(AppSettings.Config.PersonsHealthyOrRecoverdColor.ToArgb().ToString("X")));
        colorMap.Add(CellState.PersonsInfected, SkiaSharp.SKColor.Parse(AppSettings.Config.PersonInfectedColor.ToArgb().ToString("X")));
        colorMap.Add(CellState.PersonsInfectious, SkiaSharp.SKColor.Parse(AppSettings.Config.PersonInfectiousColor.ToArgb().ToString("X")));
        colorMap.Add(CellState.PersonsRecoverdImmuneNotInfectious, SkiaSharp.SKColor.Parse(AppSettings.Config.PersonsRecoverdImmuneNotInfectiousColor.ToArgb().ToString("X")));
        colorMap.Add(CellState.Virus, SkiaSharp.SKColor.Parse(AppSettings.Config.VirusColor.ToArgb().ToString("X")));
        colorMap.Add(CellState.EmptyCell, SkiaSharp.SKColor.Parse(AppSettings.Config.EmptyCellColor.ToArgb().ToString("X")));
    }
    public static SkiaSharp.SKColor ToTheColor(this CellState cellState)
    {
        // Try to get the color from the dictionary
        if (colorMap.TryGetValue(cellState, out SKColor color))
        {
            return color;
        }
        else
        {
            // Throw an exception if the cell state is not valid
            throw new ArgumentOutOfRangeException(nameof(cellState), cellState, null);
        }
    }
}


public class ColorList
{

    public static SkiaSharp.SKColor GetCellColor(CellState cellState, int personPopulation, int virusPopulation)
    {
        var cellColor = cellState.ToTheColor();

        if (AppSettings.Config.PopulationDensityColoring && cellState != CellState.EmptyCell)
        {
            var population = Math.Max(personPopulation, virusPopulation);
            if (population > 1)
            {
                cellColor = LightenColor(cellColor, 1 / (double)population);
            }
        }
        return cellColor;
    }

    public static SkiaSharp.SKColor LightenColor(SkiaSharp.SKColor Col, double percent)
    {
        // Clamp the percentage between 0 and 1
        percent = Math.Max(0, Math.Min(1, percent));

        // Convert the color to HSL format
        Col.ToHsl(out float h, out float s, out float l);

        // Increase the lightness by the percentage
        l += (1 - l) * (float)percent;

        // Convert the color back to SKColor format
        return SkiaSharp.SKColor.FromHsl(h, s, l);
    }

    public static System.Drawing.Color SkiaToSystemDrawingColor(SkiaSharp.SKColor Col)
    {
        // Get the alpha, red, green and blue components of the Skia color
        byte a = Col.Alpha;
        byte r = Col.Red;
        byte g = Col.Green;
        byte b = Col.Blue;

        // Create a System.Drawing.Color from the ARGB components
        return System.Drawing.Color.FromArgb(a, r, g, b);
    }

}

