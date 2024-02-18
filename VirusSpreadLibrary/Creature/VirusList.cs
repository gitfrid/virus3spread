
namespace VirusSpreadLibrary.Creature;

public class VirusList
{
    public List<Virus> Viruses { get; set; }
    public VirusList()
    {
        Viruses = new List<Virus>();
    }
    public void SetInitialPopulation(long InitialVirusPopulation, Grid.Grid Grid)
    {
        Viruses = new List<Virus>();

        // create initial virus list at random grid coordinates
        for (int i = 0; i < InitialVirusPopulation; i++)
        {
            Virus virus = new() { };
            // add to list
            Viruses.Add(virus);
            // add Virus to a random home Grid Cell,
            // increase population counter and set new Cell status
            // but dont delete from start cell, as in case of initilaize ther is no virus
            virus.InitializeVirusMoveToGrid(Grid);
        }
    }
    public void RemoveVirus(Virus Virus)
    {
        Viruses.Remove(Virus);
    }

    public void AddVirus(Virus Virus)
    {
        Viruses.Add(Virus);
    }
}