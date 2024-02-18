using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.SpreadModel;

namespace VirusSpreadLibrary.Grid
{
    public class CellPersons
    {
        private int numPersons;
        public List<Person> Persons { get; set; }
        public int NumPersons 
        {
            get => numPersons;
        }
        public CellPersons()
        {
            Persons = new List<Person>();        
        }
        public void Add(Person AddPerson)
        {
            Persons.Add(AddPerson);
            numPersons++;

            // just for debug
            if (numPersons > Persons.Count) 
                throw new CellPersonsException("CellPersons - Add Person Error");
        }
        public void Remove(Person RemovePerson)
        {          
            Persons.Remove(RemovePerson);
            numPersons--;

            //// just for debug
            //if (numPersons < 0) 
            //    throw new CellPersonsException("CellPersons - Remove Person Error");
        }

    }
}
