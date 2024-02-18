using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.AppProperties.PropertyGridExt;
using VirusSpreadLibrary.SpreadModel;
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature.Rates;

// Movement profile
// simulates travel behavior of frequent short and long distance movements of a person
// random select one of the 10 distance ranges
// Determine a random distance within the selected distance range
// Determines a random direction of 365° 
// returns the EndGridCoordinate for the movement

public class PersMoveDistanceProfile
{
    private readonly int maxX = 0;
    private readonly int maxY = 0;
    
    private readonly Random rnd = new ();
    
    private readonly Point[] moveDistance = [];
    public double HomeMoveActivity { get; set; }

    public PersMoveDistanceProfile()
    {
        maxX = AppSettings.Config.GridMaxX;
        maxY = AppSettings.Config.GridMaxY;
        
        DoubleSeries seriesFrom = AppSettings.Config.PersonMoveRate.DoubleSeriesFrom;
        DoubleSeries seriesTo = AppSettings.Config.PersonMoveRate.DoubleSeriesTo;
        if (seriesFrom.DoubleArray.Length == seriesTo.DoubleArray.Length)
        {
            moveDistance = new Point[seriesFrom.DoubleArray.Length];
            for (int i = 0; i < seriesFrom.DoubleArray.Length; i++)
            {
                moveDistance[i] = new Point(((int)seriesFrom.DoubleArray[i]), ((int)seriesTo.DoubleArray[i]));
                if ((int) seriesFrom.DoubleArray[i] > (int)seriesTo.DoubleArray[i]) 
                {
                    throw new 
                        ("");
                }
            }
        }
        else
        {
            throw new PersonInvalidIndexException("");
        }
    }
    private Point GetMoveDistanceByIndex(int Index)
    {
           return moveDistance[Index];
    }
    public Point GetEndCoordinateToMove(Point StartCoordiante)
    {
        int beta = rnd.Next(0, 91); // get X Y coordinate by the random distance and a random move angel between 0-90
        Point pnt = GetMoveDistanceByIndex(rnd.Next(0, moveDistance.Length));
        int a = rnd.Next(pnt.X, pnt.Y);
        double b = a / Math.Tan(90 - beta);
        double c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        double q = Math.Pow(a, 2) / c;
        double p = c - q;
        int Y = (int)Math.Round(Math.Sqrt(p * q));
        int X = (int)Math.Round(q);
        // choose a random quadrant (direction) in coordinate system -> multiply X,Y random with 1 or -1 to  
        X = StartCoordiante.X + (rnd.Next(0, 2) * 2 - 1) * X;
        Y = StartCoordiante.Y + (rnd.Next(0, 2) * 2 - 1) * Y;
        // check for grid boundaries 
        if (Y >= maxY | Y < 0)
        {
            Y = StartCoordiante.Y;
        };
        if (X >= maxX | X < 0)
        {
            X = StartCoordiante.X;
        };

        return new Point(X, Y); ;
    }

}
