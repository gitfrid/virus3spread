using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Creature.Rates;
using VirusSpreadLibrary.SpreadModel;
using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Creature;

public class Person
{

    private readonly Random rnd = new ();
    
    private readonly PersMoveDistanceProfile persMoveProfile = new(); 
    public int Age { get; set; } 
    public double PersonBirthRateByAge { get; set; }
    public double PersonDeathProbabilityByAge { get; set; }

    public bool IsDead { get; set; }
    public int ReinfectionImmunityPeriod 
    {
        get => reinfectionImmunityPeriod;
        set => reinfectionImmunityPeriod = value;
    }

    // move data
    public Enum.CreatureType CreatureType = Enum.CreatureType.Person;
    public Point StartGridCoordinate { get; private set; }
    public Point EndGridCoordinate { get; private set; }
    public Point HomeGridCoordinate { get; private set; }


    private int healthCounter = 0;
    private int infectionCounter = 0;
    private int reinfectionImmunityPeriod = AppSettings.Config.PersonReinfectionImmunityPeriod;

    private PersonState healthState = PersonState.PersonHealthy;

    public int HealthCounter
    {
        get
        {
            return healthCounter;
        }
        set
        {
            if (healthCounter != 0)
            {
                healthCounter = value;
            }
        }
    }

    public int MyProperty { get; set; }
    public int InfectionCounter
    {
        get => infectionCounter;
    }

    public PersonState HealthState 
    { 
        get => healthState;
        set => healthState = value;
    }
    public bool DoMove()
    {
        // move within PersonMoveActivityRnd percentage, 0=dont 100=always
        int moveActivity = AppSettings.Config.PersonMoveActivityRnd;
        if (moveActivity < 0) { moveActivity = 0; }
        if (moveActivity == 0 || rnd.Next(1, moveActivity + 1) > 1) 
            return false;
        return true;
    }
    public bool DoMoveHome()
    {
        // move home within PersonMoveHomeActivityRnd percentage, 0=dont 100=always
        int moveActivity = AppSettings.Config.PersonMoveHomeActivityRnd;
        if (moveActivity < 0) { moveActivity = 0; }
        if (moveActivity == 0 || rnd.Next(1, moveActivity + 1) > 1) return false;
        return true;
    }
   
    public static void ChildBirth()
    {
      //    
    }
    public static void SpreadVirus()
    {
      //
    }

    public void InfectPerson()
    {
        if (healthState == PersonState.PersonHealthyRecoverd || healthState == PersonState.PersonHealthy)
        {
            healthCounter=1;
            ++infectionCounter;
            //reinfectionImmunityPeriod = AppSettings.Config.PersonReinfectionImmunityPeriod;

            if (healthState == PersonState.PersonHealthyRecoverd) 
            {
                healthState = PersonState.PersonReinfected;
            }
            if (healthState == PersonState.PersonHealthy)
            {
                healthState = PersonState.PersonInfected;
            }
        }
        //if (healthState == PersonState.PersonRecoverdImmunePeriodNotInfectious)
        //{
        //    reinfectionImmunityPeriod = reinfectionImmunityPeriod + (reinfectionImmunityPeriod/100)*10;
        //}
    }

    public void SetPersonHealthState()
    {

        if (healthCounter == 0 )
        {
            // person healthy or recoverd
            if (infectionCounter < 1) 
            {
                healthState = PersonState.PersonHealthy;
            }
            else 
            {
                healthState = PersonState.PersonHealthyRecoverd;
            }
            return;
        }
        
        if (healthCounter <= AppSettings.Config.PersonLatencyPeriod)
        {
            // LatencyPeriod - person was infected
            if (infectionCounter < 2)
            {
                healthState = PersonState.PersonInfected;
            }
            else
            {
                healthState = PersonState.PersonReinfected;
            }
        }

        if (healthCounter > AppSettings.Config.PersonLatencyPeriod 
            && healthCounter <= AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod)
        {
            // InfectiousPeriod - person infectious
            healthState = PersonState.PersonInfectious;
        }

        if (healthCounter > AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod
            && healthCounter <= this.ReinfectionImmunityPeriod
            + AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod)
        {
            
        
                // ReinfectionImmunityPeriod - person was recoverd and is imune - immunity periode
                healthState = PersonState.PersonRecoverdImmunePeriodNotInfectious;
        
        }

        if (healthCounter > this.ReinfectionImmunityPeriod
            + AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod)
        {
            // PersonAfterImmunePeriode - person can get reinfected again depending on the PersonReinfectionRate
            healthState = PersonState.PersonAfterImmunePeriode;
        }
    }

    public void InitializePersonMoveToGrid(Grid.Grid Grid)
    {
        int maxX = Grid.ReturnMaxX();
        int maxY = Grid.ReturnMaxY();

        // random initial move endcoordiante = homeCoordinate
        EndGridCoordinate = new(rnd.Next(0,maxX ), rnd.Next(0, maxY));
        HomeGridCoordinate = EndGridCoordinate;

        do // must be differ from from end coordiante, otherwise creature does not move 
        {
           StartGridCoordinate = new(rnd.Next(0, maxX), rnd.Next(0, maxY));
        } while (StartGridCoordinate == EndGridCoordinate);
        
        // initalize move to the home cell, add the person at (home) endcoordiante
        // but dont remove from the start coordinate, as in the intitialize case there is no person
        SpreadModel.SetGridCellState.AddPersonToNewEndGridCoordinate(this, Grid);
        
        // save as new current coordinate
        StartGridCoordinate = EndGridCoordinate;
    }

    public void MoveToNewCoordinate(Grid.Grid Grid)
    {
        // get new random endpoint to move to
        // depending on the spcified range in settings of persMoveProfile and PersonMoveGlobal var
        if (AppSettings.Config.PersonMoveGlobal)
        {   
            // calculate next move from EndCoordinate of the last iteration, in the spcified range - moves over whole grid
            EndGridCoordinate = persMoveProfile.GetEndCoordinateToMove(StartGridCoordinate);
        } 
        else
        {   
            // calculate next move always from the Home Coordinate in the specified range - moves only within the range
            EndGridCoordinate = persMoveProfile.GetEndCoordinateToMove(HomeGridCoordinate);
        }

        // do move to endpoint end cell on grid and set cell state and population counter
        SpreadModel.SetGridCellState.PersonMoveState(this,Grid);

        // save as new current coordiante
        StartGridCoordinate = EndGridCoordinate;
    }
    public void MoveToHomeCoordinate(Grid.Grid Grid)
    {
        EndGridCoordinate = HomeGridCoordinate;

        // do move to home coordinate
        SpreadModel.SetGridCellState.PersonMoveState(this, Grid);

        // save as current endpoint (new StartGridCoordinate)
        StartGridCoordinate = EndGridCoordinate;
    }


    // 50% means that 1 in 2 people who after immunity period can potentially be infected again
    // 1 day = 1 iteration over the entire population
    // Is this model realistic, does it depend only on the person and the virus, or also on external factors?
    // Depending on how the reinfection model is implemented, it makes a big difference to the outcome
    public bool SetPercentPossibleReinfectionsInImmunityPhase()
    {
        if (AppSettings.Config.PersonReinfectionRate == 0)
        {
            return false;
        }
        if (AppSettings.Config.PersonReinfectionRate == 100)
        {
            return true;
        }

        double randomProbability = AppSettings.Config.PersonReinfectionRate;
        if (randomProbability > 0 && randomProbability < 100)
        {
            double randomNumber = rnd.NextDouble() * 100;
            if (randomNumber <= randomProbability)
            {
                return true;
            }
            else
            {
                // Console.Write("false;");
                return false;
            }
        }
        else
        {
            // wrong input from AppSettings - should not happen
            throw new PersonReinfectionRateInputException("");
        }
    }

}
