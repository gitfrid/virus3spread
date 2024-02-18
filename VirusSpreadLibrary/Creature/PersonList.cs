
namespace VirusSpreadLibrary.Creature;

public class PersonList
{
    public  List<Person> Persons { get; set; }
    public PersonList()
    {
        Persons = new List<Person>();
    }
    public void SetInitialPopulation(long InitialPersonPopulation, Grid.Grid Grid)
    {
        Persons = new List<Person>();

        // create initial person list at random grid coordinates
        for (int i = 0; i < InitialPersonPopulation; i++)
        {
            Person person = new() { };
            // add to list
            Persons.Add(person);
            // add Virus to a random home Grid Cell,
            // increase population counter and set new Cell status
            // but dont delete from start cell, as in case of initilaize ther is no person
            person.InitializePersonMoveToGrid(Grid);
        }
    }

}
