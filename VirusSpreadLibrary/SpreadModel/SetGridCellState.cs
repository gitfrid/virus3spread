using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Enum;
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Grid;


namespace VirusSpreadLibrary.SpreadModel;

public static class SetGridCellState
{
    private static GridCell cell = new();

    //private readonly ColorList colorList;

    private static void SetNewCellState(GridCell Cell)
    {
        // evaluate new state of grid cell
        int numPersons = Cell.NumPersons();
        int numViruses = Cell.NumViruses();     
        
        if (numPersons > 0)
        {
            bool personsInfectious = false;
            bool personsInfected = false;
            bool personRecoverdImmuneNotInfectious = false;

            // get current state of the grid Cell  
            for (int i = 0; i < numPersons; i++)
            {
                if (PersonState.PersonReinfected == Cell.PersonPopulation.Persons[i].HealthState || PersonState.PersonInfected == Cell.PersonPopulation.Persons[i].HealthState)
                {
                    personsInfected = true;
                }

                if (PersonState.PersonInfectious == Cell.PersonPopulation.Persons[i].HealthState)
                {
                    personsInfectious = true;
                }

                if (PersonState.PersonRecoverdImmunePeriodNotInfectious == Cell.PersonPopulation.Persons[i].HealthState)
                {
                    personRecoverdImmuneNotInfectious = true;
                }
            }

            // set new sate of the grid Cel -> represented as Grid color in following ranking order
            Cell.CellState = CellState.PersonsHealthyOrRecoverd;

            if (personRecoverdImmuneNotInfectious)
            {
                Cell.CellState = CellState.PersonsRecoverdImmuneNotInfectious;
            }
                        
            if (personsInfected) // numViruses > 0<- all persons must be infected if a virus is on same cell
            {
                Cell.CellState = CellState.PersonsInfected;
            }
 
            if (personsInfectious)
            {
                Cell.CellState = CellState.PersonsInfectious;
            } 
        }

        // if no persons on a cell, two possible Grid colors exist
        if (numViruses < 1 && numPersons < 1)
        {
            Cell.CellState = CellState.EmptyCell;
        }

        if (numViruses > 0 && numPersons < 1) 
        {
            Cell.CellState = CellState.Virus;
        }       
    }

    public static bool AddPersonToNewEndGridCoordinate(Person MovingPerson, Grid.Grid Grid)
    {
        // add, moving virus or person to the end grid coordiante
        int xEnd = MovingPerson.EndGridCoordinate.X;
        int yEnd = MovingPerson.EndGridCoordinate.Y;

        // exit if not moved
        int xStart = MovingPerson.StartGridCoordinate.X;
        int yStart = MovingPerson.StartGridCoordinate.Y;
        if (xStart == xEnd && yStart == yEnd) { return true; }

        // get the end grid cell moving to 
        cell = Grid.Cells[xEnd, yEnd];

        // add Person to the end grid Cell and increase population counter 
        cell.AddPerson(MovingPerson);

        // set new end cell sate
        SetNewCellState(cell);

        // if cell contains infectious person or virus, then infect all Persons on this cell
        int numPersons = cell.NumPersons();
        int numViruses = cell.NumViruses();

        if (cell.CellState == CellState.PersonsInfectious || numViruses > 0)
        {
            for (int i = 0; i < numPersons; i++)
            {
                cell.PersonPopulation.Persons[i].InfectPerson();
            }
            SetNewCellState(cell);
        }

        // set new end cell color depending on cell state
        cell.CellColor = ColorList.GetCellColor(cell.CellState, cell.NumPersons(), cell.NumViruses());
        return false;
    }

    public static void PersonMoveState(Person MovingPerson, Grid.Grid Grid)
    {
        // add person to new end grid cell
        bool notMoved = AddPersonToNewEndGridCoordinate(MovingPerson, Grid);

        // exit if not moved
        if (notMoved) { return; } 

        // remove virus or person from start grid coordiante
        // after it has moved to end coordinate
        int xStart = MovingPerson.StartGridCoordinate.X;
        int yStart = MovingPerson.StartGridCoordinate.Y;

        // delete person from start grid coordinate
        GridCell cellStart = Grid.Cells[xStart, yStart];

        // remove Person from the sart grid Cell and decrease population counter 
        // don't apply this for initialization move, as there is no person to delete then
        cellStart.RemovePerson(MovingPerson); 

        // set new sart cell sate
        SetNewCellState(cellStart);

        // if TrackMovment true leave old color, else set new sart cell color          
        if (AppSettings.Config.TrackMovment == false)
        {
            cellStart.CellColor = ColorList.GetCellColor(cellStart.CellState, cellStart.NumPersons(), cellStart.NumViruses());
        }
    }

    public static bool AddVirusToNewEndGridCoordinate(Virus MovingVirus, Grid.Grid Grid)
    {
        // add, moving virus  to the end grid coordiante
        int xEnd = MovingVirus.EndGridCoordinate.X;
        int yEnd = MovingVirus.EndGridCoordinate.Y;

        // remove virus from start grid coordiante
        // after it has moved to end coordinate
        int xStart = MovingVirus.StartGridCoordinate.X;
        int yStart = MovingVirus.StartGridCoordinate.Y;

        cell = Grid.Cells[xEnd, yEnd];

        // exit if not moved
        if (xStart == xEnd && yStart == yEnd) { return true; }

        // add Virus to the end grid Cell and increase population counter 
        cell.AddVirus(MovingVirus);

        // set new end cell sate
        SetNewCellState(cell);

        // if cell contains infectious person or virus, then infect all Persons on this cell
        int numPersons = cell.NumPersons();
        if (cell.CellState == CellState.PersonsInfectious)
        {
            for (int i = 0; i < numPersons; i++)
            {
                cell.PersonPopulation.Persons[i].InfectPerson();
            }
            SetNewCellState(cell);
        }

        // set new end cell color depending on cell state
        cell.CellColor = ColorList.GetCellColor(cell.CellState, cell.NumPersons(), cell.NumViruses());
        return false;
    }

    public static void VirusMoveState(Virus MovingVirus, Grid.Grid Grid)
    {
        // add virus to new end grid cell
        bool notMoved = AddVirusToNewEndGridCoordinate(MovingVirus, Grid);

        // exit if not moved
        if (notMoved) { return; }

        // remove virus from start grid coordiante
        // after it has moved to end coordinate
        int xStart = MovingVirus.StartGridCoordinate.X;
        int yStart = MovingVirus.StartGridCoordinate.Y;

        // delete virus from start grid coordinate
        GridCell cellStart = Grid.Cells[xStart, yStart];

        // remove Virus from the start grid Cell and decrease population counter 
        cellStart.RemoveVirus(MovingVirus);

        // set new sart cell sate
        SetNewCellState(cellStart);
      
        // if TrackMovment true leave old color, else set new sart cell color          
        if (AppSettings.Config.TrackMovment == false)
        {
            cellStart.CellColor = ColorList.GetCellColor(cellStart.CellState, cellStart.NumPersons(), cellStart.NumViruses());
        }
    }
    public static void RemoveVirusFromGridCoordinate(Virus RemovingVirus, Grid.Grid Grid)
    {

        // remove virus from start grid coordiante
        // after it has moved to end coordinate
        int xStart = RemovingVirus.StartGridCoordinate.X;
        int yStart = RemovingVirus.StartGridCoordinate.Y;

        // delete virus from start grid coordinate
        GridCell cellStart = Grid.Cells[xStart, yStart];

        // remove Virus from the start grid Cell and decrease population counter 
        cellStart.RemoveVirus(RemovingVirus);

        // set new sart cell sate
        SetNewCellState(cellStart);

        // set new sart cell color          
        cellStart.CellColor = ColorList.GetCellColor(cellStart.CellState, cellStart.NumPersons(), cellStart.NumViruses());
    }
}
