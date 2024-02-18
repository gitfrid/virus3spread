using VirusSpreadLibrary.AppProperties.PropertyGridExt;
using VirusSpreadLibrary.AppProperties;

namespace VirusSpreadLibrary.SpreadModel
{
    internal class PersonInvalidIndexException : Exception
    {
        public PersonInvalidIndexException()
        {
        }
        public PersonInvalidIndexException(string StringToPass) : base(
            (String.Format("PersonMoveRateFrom and PersonMoveRateTo must have the same number of entries.\r\n" +
            "PersonMoveRateFrom values must be smaller as the related PersonMoveRateTo value!\r\n\r\n" +
            "PeronMoveRates will be reset to the default values!\r\n\r\n" +
            "Please enter the desired correct values in APP-Settings: Category -> Move Rate Person\r\n{0}", StringToPass)))
        {
            AppSettings.Config.PersonMoveRate.DoubleSeriesFrom = new DoubleSeries([1, 1, 1, 1, 1, 1, 1, 1, 1, 1]);
            AppSettings.Config.PersonMoveRate.DoubleSeriesTo = new DoubleSeries([2, 2, 2, 2, 2, 2, 2, 2, 2, 2]);
        }
        public PersonInvalidIndexException(string message, Exception inner) : base(message, inner) { }
    }
    public class VirusInvalidIndexException : Exception
    {
        public VirusInvalidIndexException()
        {
        }
        public VirusInvalidIndexException(string StringToPass) : base(
            (String.Format("VirusMoveRateFrom and VirusMoveRateTo must have the same number of entries.\r\n" +
            "VirusMoveRateFrom values must be smaller as the related VirusMoveRateTo value!\r\n\r\n" +
            "VirusMoveRates will be reset to the default values!\r\n\r\n" +
            "Please enter the desired correct values in APP-Settings: Category -> Move Rate Virus\r\n{0}", StringToPass)))
        { }
        public VirusInvalidIndexException(string message, Exception inner) : base(message, inner) { }
    }
    internal class PersonReinfectionRateInputException : Exception
    {
        public PersonReinfectionRateInputException()
        {
        }
        public PersonReinfectionRateInputException(string StringToPass) : base(
            (String.Format("PersonReinfectionRate must be a decimal value between 0-100 \r\n" +
            "PersonReinfectionRate  will be reset to the default value 11\r\n{0}", StringToPass)))
        {
            AppSettings.Config.PersonReinfectionRate = 11;
        }
        public PersonReinfectionRateInputException(string message, Exception inner) : base(message, inner) { }
    }

    internal class VirusReproductionRateInputException : Exception
    {
        public VirusReproductionRateInputException()
        {
        }
        public VirusReproductionRateInputException(string StringToPass) : base(
            (String.Format("VirusReproductionRate must be a decimal value between 0-100 \r\n" +
            "VirusReproductionRate will be reset to the default value 100\r\n{0}", StringToPass)))
        {
            AppSettings.Config.PersonReinfectionRate = 100;
        }
        public VirusReproductionRateInputException(string message, Exception inner) : base(message, inner) { }
    }
        
    internal class CellPersonsException : Exception
    {
        public CellPersonsException()
        {
        }
        public CellPersonsException(string StringToPass) : base(
            (String.Format("CellPersons number shouldn't be < 0, \r\n" +
            "Check the programmcode - number of Persons in a Cell " +
                "does not match with the current grid coordinate of a person? \r\n{0}", StringToPass)))
        {
            //
        }
        public CellPersonsException(string message, Exception inner) : base(message, inner) { }
    }

    internal class CellVirusesException : Exception
    {
        public CellVirusesException()
        {
        }
        public CellVirusesException(string StringToPass) : base(
            (String.Format("CellVirusesException number shouldn't be < 0, \r\n" +
            "Check the programmcode - number of Viruses in a Cell" +
                "does not match with the current grid coordinate of a virus? \r\n{0}", StringToPass)))
        {
            //
        }
        public CellVirusesException(string message, Exception inner) : base(message, inner) { }
    }

}
