using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Creature.Rates;

namespace VirusSpreadLibrary.Creature;

public class Virus
{
    private readonly Random rnd = new ();
 
    private readonly VirMoveDistanceProfile virMoveProfile = new();

    private VirusState virusState;
    public int Age { get; set; }
    public double VirusReproduceRateByAge { get; set; }
    public double VirusBrokenRateByAge { get; set; }
    public bool IsBroken { get; set; }


    // move data
    public Enum.CreatureType CreatureType = Enum.CreatureType.Virus;
    public Point StartGridCoordinate { get; private set; }
    public Point EndGridCoordinate { get; private set; }
    public Point HomeGridCoordinate { get; private set; }
    public VirusState VirusState
    {
        get => virusState;
        set => virusState = value;
    }

    public Virus()
    {
        virusState = new VirusState();
    }
    public bool DoMove()
    {
        int moveActivity = AppSettings.Config.VirusMoveActivityRnd;
        if (moveActivity == 0 || rnd.Next(1, moveActivity + 1) > 1) return false;
        return true;
    }
    public bool DoMoveHome()
    {
        int moveActivity = AppSettings.Config.VirusMoveHomeActivityRnd;
        if (moveActivity == 0 || rnd.Next(1, moveActivity + 1) > 1) return false;
        return true;
    }

    public void VirusRemove(Grid.Grid Grid)
    {
        SpreadModel.SetGridCellState.RemoveVirusFromGridCoordinate(this, Grid);
    }
    public void ReproduceToGrid(Point ReproduceCoordiante, Grid.Grid Grid)
    {
        int maxX = Grid.ReturnMaxX();
        int maxY = Grid.ReturnMaxY();


        // random initial move endcoordiante = homeCoordinate
        EndGridCoordinate = ReproduceCoordiante;
        HomeGridCoordinate = EndGridCoordinate;

        do // must be differ from from end coordiante, otherwise creature does not move 
        {
            StartGridCoordinate = new(rnd.Next(0, maxX), rnd.Next(0, maxY));
        } while (StartGridCoordinate == EndGridCoordinate);

        // initalize move to the home cell, add the virus at (home) endcoordiante
        // but dont remove from the start coordinate, as in the intitialize case there is no virus
        SpreadModel.SetGridCellState.AddVirusToNewEndGridCoordinate(this, Grid);

        // save as new current coordinate
        StartGridCoordinate = EndGridCoordinate;
    }


    public void InitializeVirusMoveToGrid(Grid.Grid Grid)
    {
        int maxX = Grid.ReturnMaxX();
        int maxY = Grid.ReturnMaxY();

        // random initial move endcoordiante = homeCoordinate
        EndGridCoordinate = new(rnd.Next(0, maxX), rnd.Next(0, maxY));
        HomeGridCoordinate = EndGridCoordinate;

        do // must be differ from from end coordiante, otherwise creature does not move 
        {
            StartGridCoordinate = new(rnd.Next(0, maxX), rnd.Next(0, maxY));
        } while (StartGridCoordinate == EndGridCoordinate);

        // initalize move to the home cell, add the virus at (home) endcoordiante
        // but dont remove from the start coordinate, as in the intitialize case there is no virus
        SpreadModel.SetGridCellState.AddVirusToNewEndGridCoordinate(this, Grid);

        // save as new current coordinate
        StartGridCoordinate = EndGridCoordinate;
    }

    public void MoveToNewCoordinate(Grid.Grid Grid)
    {
        // get new random endpoint to move to
        // depending on the spcified range in settings of persMoveProfile and PersonMoveGlobal var
        if (AppSettings.Config.VirusMoveGlobal)
        {
            // calculate next move from EndCoordinate of the last iteration, in the spcified range - moves over whole grid
            EndGridCoordinate = virMoveProfile.GetEndCoordinateToMove(StartGridCoordinate);
        }
        else
        {
            // calculate next move always from the Home Coordinate in the specified range - moves only within the range
            EndGridCoordinate = virMoveProfile.GetEndCoordinateToMove(HomeGridCoordinate);
        }

        // do move to endpoint
        SpreadModel.SetGridCellState.VirusMoveState(this, Grid);

        // save as new current coordinate
        StartGridCoordinate = EndGridCoordinate;
    }

    public void MoveToHomeCoordinate(Grid.Grid Grid)
    {
        EndGridCoordinate = HomeGridCoordinate;
        // do move to endpoint
        SpreadModel.SetGridCellState.VirusMoveState(this, Grid);

        // save end coordinate as new current coordinate
        StartGridCoordinate = EndGridCoordinate;
    }

}
