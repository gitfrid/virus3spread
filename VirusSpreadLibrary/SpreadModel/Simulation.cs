
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Grid;
using VirusSpreadLibrary.Plott;
using SkiaSharp;
using VirusSpreadLibrary.Enum;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{

    private readonly Grid.Grid grid;
    private readonly PersonList personList = new();
    private readonly VirusList virusList = new();
    private int iteration;    
    private readonly Random rnd = new();

    readonly private PlotData plotData = new();
    // public prop to access the queue

    public PlotData PlotData
    {
        get => plotData;
    }

    private bool stopIteration;
    public Simulation()
    {
        stopIteration = true;
        MaxX = AppSettings.Config.GridMaxX;
        MaxY = AppSettings.Config.GridMaxY;
        grid = new Grid.Grid();
        grid.SetNewEmptyGrid(MaxX, MaxY);
        personList.SetInitialPopulation(AppSettings.Config.InitialPersonPopulation, grid);
        virusList.SetInitialPopulation(AppSettings.Config.InitialVirusPopulation, grid);
    }

    public int MaxX { get; set; }
    public int MaxY { get; set; }
    public int Iteration { get => iteration; }

    public bool IterationRunning { get => !stopIteration; }

    public void StartIteration()
    {
        stopIteration = false;
    }
    public void StopIteration()
    {
        stopIteration = true;
    }
    public void NextIteration()
    {
        if (stopIteration == true) { return; }

        //Log.Logger = Logging.GetInstance();        
        //Log.Logger.Information("Nr: {A} iteration", iteration);
        iteration++;
        plotData.IterationNumber = iteration;

        long personAgeCum = 0;
        double personsMoveDistanceCum = 0;

        plotData.PersonPopulation = personList.Persons.Count;
        foreach (Person person in personList.Persons)
        {
            person.Age++;
            personAgeCum += person.Age;

            // increase health state counter if person is infected
            person.HealthCounter++;
            person.SetPersonHealthState();

            // move person and change health state if get infected
            if (person.DoMove())
            {
                System.Drawing.Point startPoint = person.StartGridCoordinate;
                if (person.DoMoveHome())
                {
                    person.MoveToHomeCoordinate(grid);
                }
                else
                {
                    person.MoveToNewCoordinate(grid);
                }
                // calc move distance
                System.Drawing.Point endPoint = person.EndGridCoordinate;
                int dx = endPoint.X - startPoint.X;
                int dy = endPoint.Y - startPoint.Y;
                double SE = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                personsMoveDistanceCum += SE;
                // set new health state for plotting
                person.SetPersonHealthState();
            }

            // infectous people breed virus
            if (person.HealthState == PersonState.PersonInfectious)
            {
                // Probability of reproduction 100% = one virus every day/iteration is reproduced during infectious phase of a person
                // 0% = no virus reproduction 
                double randomProbability = AppSettings.Config.VirusReproductionRate;
                if (randomProbability >= 0 && randomProbability <= 100)
                {
                    double randomNumber = rnd.NextDouble() * 100;
                    if (randomNumber <= randomProbability)
                    {
                        // VirusNumberReproducedPerIteration means number of viruses reproduced per iteration
                        for (int i = 0 ; i < AppSettings.Config.VirusNumberReproducedPerIteration; i++) 
                        { 
                            Virus virusNew = new();
                            virusNew.ReproduceToGrid(person.EndGridCoordinate, grid);
                            virusList.AddVirus(virusNew);
                        }
                    };
                }
                else
                {
                    // wrong input from AppSettings - should not happen
                    throw new VirusReproductionRateInputException("");
                }
            }

            if (person.HealthState == PersonState.PersonAfterImmunePeriode)
            {
                // person was recoverd and immune, but now can get reinfected again
                // depending on PersonReinfectionRate 
                // 50% means that 1 in 2 people who after immunity period can potentially be infected again
                if (person.SetPercentPossibleReinfectionsInImmunityPhase())
                {
                    person.HealthState = PersonState.PersonHealthyRecoverd;
                    person.HealthCounter = 0;
                }
            }


            plotData.SetPlotHealthState(person);
        };



        // Parallel.ForEach(VirusList.Viruses, virus =>}); -> takes longer
        long virusAgeCum = 0;
        double virusesMoveDistanceCum = 0;
        plotData.VirusPopulation = virusList.Viruses.Count;

        //foreach (Virus virus in virusList.Viruses)
        for (int x = virusList.Viruses.Count - 1; x > -1; x--)
        {
            Virus virus = virusList.Viruses[x];
            virus.Age++;
            virusAgeCum += virus.Age;

            if (virus.DoMove())
            {
                System.Drawing.Point startPoint = virus.StartGridCoordinate;

                if (virus.DoMoveHome())
                {
                    virus.MoveToHomeCoordinate(grid);
                }
                else
                {
                    virus.MoveToNewCoordinate(grid);
                }
                System.Drawing.Point endPoint = virus.EndGridCoordinate;
                int dx = endPoint.X - startPoint.X;
                int dy = endPoint.Y - startPoint.Y;
                double SE = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                virusesMoveDistanceCum += SE;
            }

            //remove decayed virus if VirusInfectionDurationDays were reached
            if ((virus.Age > AppSettings.Config.VirusInfectionDurationDays) && (AppSettings.Config.VirusInfectionDurationDays != 0))
            {
                virus.VirusRemove(grid);
                virusList.Viruses.RemoveAt(x);
            }
        };

        if (plotData.PersonPopulation > 0)
        {
            plotData.PersonsAge = personAgeCum / plotData.PersonPopulation;
            plotData.PersonsMoveDistance = personsMoveDistanceCum / plotData.PersonPopulation;
            plotData.PersonsInfectionCounter /= plotData.PersonPopulation;
        }

        if (plotData.VirusPopulation > 0)
        {
            plotData.VirusesAge = virusAgeCum / plotData.VirusPopulation; //<- cumulated age
            plotData.VirusesMoveDistance = virusesMoveDistanceCum / plotData.VirusPopulation; //<- cumulated move distance
        }

        //// debug
        //plotData.PersonPopulation = plotData.PersonsInfected + plotData.PersonsHealthy + plotData.PersonsInfectious + plotData.PersonsRecoverd + PlotData.PersonsRecoverdImmuneNotinfectious + plotData.PersonsReinfected + plotData.PersonAfterImmunePeriode;

        // fix if person reinfecton rate <> 100, because a percentage of persons reach the status PersonAfterImmunePeriode 
        // which i am counting (adding) to PersonsRecoverdImmuneNotinfectious but not plotting or showing as separate status
        plotData.PersonsRecoverdImmuneNotinfectious += plotData.PersonAfterImmunePeriode;
        // write data to queue for plotting and reset queue
        plotData.WriteToQueue();
        plotData.ResetCounter();

    }

    // first initialize grid!
    public void DrawGrid(SKCanvas canvas, float coordinateFactX, float coordinateFactY, float rectangleX, float rectangleY)
    {
        for (int y = 0; y < MaxY; y++)
        {
            for (int x = 0; x < MaxX; x++)
            {
                GridCell Cell = grid.Cells[x, y];
                SKPaint paint = new()
                {
                    Color = Cell.CellColor
                };
                canvas.DrawRect(x * coordinateFactX, y * coordinateFactY, rectangleX, rectangleY, paint);
            }
        }
    }

}