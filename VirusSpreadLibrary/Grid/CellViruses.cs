using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.SpreadModel;

namespace VirusSpreadLibrary.Grid
{
    public class CellViruses
    {
        private int numViruses;
        public int NumViruses 
        { 
            get => numViruses; 
        
        }
        public List<Virus> Viruses { get; set; }
        public CellViruses()
        {
            Viruses = new List<Virus>();
        }
        public void Add(Virus AddVirus)
        {
            Viruses.Add(AddVirus);
            ++numViruses;

            // just for debug
            if (numViruses > Viruses.Count)
                throw new CellVirusesException("CellPersons - Add Person Error");
        }
        public void Remove(Virus RemoveVirus)
        {
            if (Viruses.Count != numViruses)
            {
                MessageBox.Show("vir count!");
            }
            Viruses.Remove(RemoveVirus);
               --numViruses;

            ////just for debug
            //if (numViruses < 0)
            //        throw new CellVirusesException("CellViruses - Remove Virus Error");
        }

    }
}
