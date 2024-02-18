
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;
public class GridCell
{
    private SkiaSharp.SKColor cellColor;   
    private readonly CellViruses virusPopulation;
    private readonly CellPersons personPopulation;
    private CellState cellState = Enum.CellState.EmptyCell;

    public GridCell()
    {
        // set Cell Population and Color!
        virusPopulation = new CellViruses();
        personPopulation = new CellPersons();
        cellColor = new SkiaSharp.SKColor();         
    }
    public SkiaSharp.SKColor CellColor
    {
        get => cellColor!;
        set => cellColor = value;
    }
    public CellViruses VirusPopulation
    {
        get => virusPopulation;
        //set => virusPopulation = value;
    }
    public CellPersons PersonPopulation
    {
        get => personPopulation;
        //set => personPopulation = value;
    }
    public CellState CellState
    {
        get => cellState;
        set => cellState = value;
    }
    public void AddVirus(Virus AddVirus )
    {
        VirusPopulation.Add(AddVirus);
    }
    public void AddPerson(Person AddPerson)
    {
        PersonPopulation.Add(AddPerson);
    }
    public void RemoveVirus(Virus RemoveVirus)
    {
        VirusPopulation.Remove(RemoveVirus);
    }
    public void RemovePerson(Person RemovePerson)
    {
        personPopulation.Remove(RemovePerson);
    }
    public int NumViruses()
    {
        if (VirusPopulation == null)
        {
            return 0;
        }
        else
        {
            return VirusPopulation.NumViruses;
        }
    }
    public int NumPersons()
    {
        if (PersonPopulation == null) 
        { 
            return 0;
        }
        else 
        {
            return PersonPopulation.NumPersons;
        }        
    }

}
