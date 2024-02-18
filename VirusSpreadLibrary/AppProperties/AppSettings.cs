using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Xml.Serialization;
using Polenter.Serialization;
using VirusSpreadLibrary.AppProperties.PropertyGridExt;


namespace VirusSpreadLibrary.AppProperties;

// This contaier class holds all the app configuration settings
// similar to the built in visual Studio properties.settings 
// 
// it serialize and deserialize all properties of this contaier class 
// into bin\..\AppProperties.XML using the Nuget Package: SharpSerializer.Core
//
// property of the objects are then shown at runtime in the property grid UI 
// together with a property description 
// by binding - as selected object - to the propertygrid in the MainForm
//
// This is a Workaround as Visual Studio properties.settings Designer could not save a description for properties
// it deleted all manually inserted descriptions, if a new property was saved
// It also can't handle complex property classes with .Net 6-8
// it can only provides this Feature with the .Net Framwork Standard 

// depends on Nuget: SharpSerializer.Core 1.0.0 !!
//
// !! Using Nuget "SharpSerializer for the .Net Desktop Framework" !!
// !! did not work stable with .Net 6,8 !!
//
// Classes must have a constructor for xml deserialisation and serialisation
// therfore unsightly workaround for the color types and fonts are required




[Serializable]
[DefaultPropertyAttribute("AppVersion")]

public  class AppSettings
{


    private string about = "";
    private readonly string appVersion = GetAppVersion();
    private int gridMaxX = 800;
    private int gridMaxY = 400;
    private long initialPersonPopulation = 5000;
    private long initialVirusPopulation = 5000;
    private long maxIterations = 1000000;
    private Point form_Config_WindowLocation = new(0, 0);
    private Size form_Config_WindowSize = new(2056, 1096);
    private Point plotForm_WindowLocation = new(0, 0);
    private Size plotForm_WindowSize = new(1888, 1122);
    private Point phaseChartForm_WindowLocation = new(0, 0);
    private Size phaseChartForm_WindowSize = new(1888, 1122);


    private bool virusMoveGlobal = false;
    private bool personMoveGlobal = false;
    private int gridFormTimer = 1;
    private bool trackMovment = false;
    private bool populationDensityColoring = true;
    private bool showHelperSettings = false;
    private bool showSimulationGridWindow = true;
    private Color virusColor = Color.WhiteSmoke;
    private string xmlVirusColor = "WhiteSmoke";
    private Color personsHealthyOrRecoverdColor = Color.LawnGreen;
    private string xmlPersonsHealthyOrRecoverdColor = "LawnGreen";
    private Color personsInfectedColor = Color.DeepSkyBlue;
    private string xmlPersonsInfectedColor = "DeepSkyBlue";
    private Color emptyCellColor = Color.Black;
    private string xmlEmptyCellColor = "Black";
    private Color personsInfectiousColor = Color.Red;
    private string xmlPersonsInfectiousColor = "Red";
    private Color personsRecoverdImmuneNotInfectiousColor = Color.Plum;
    private string xmlPersonsRecoverdImmuneNotInfectiousColor = "Plum";


    private String configFilePath = string.Concat(Path.GetDirectoryName(Application.ExecutablePath),
                                    "\\", Path.GetFileNameWithoutExtension(Application.ExecutablePath), ".XML");

    private String csvFilePath = string.Concat(Path.GetDirectoryName(Application.ExecutablePath),
                                    "\\", Path.GetFileNameWithoutExtension(Application.ExecutablePath), ".CSV");

    private DoubleSeriesClass personMoveRate = new();
    private string personMoveRateFrom = "";
    private string personMoveRateTo = "";
    private int personMoveActivityRnd = 1;
    private int personMoveHomeActivityRnd = 2;
    private int personLatencyPeriod = 2;
    private int personInfectiousPeriod = 9;
    private int personReinfectionImmunityPeriod = 155;
    private double personReinfectionRate = 11;

    private DoubleSeriesClass virusMoveRate = new();
    private string virusMoveRateFrom = "";
    private string virusMoveRateTo = "";
    private int virusMoveActivityRnd = 1;
    private int virusMoveHomeActivityRnd = 0;
    private int virusInfectionDurationDays = 24;
    private double virusReproductionRate=100;
    private int virusNumberReproducedPerIteration = 1;

    // plot
    private LegendVisability legendVisability = new();
    private int phaseChartXSelectedIndex = 0;
    private int phaseChartYSelectedIndex = 0;


    private string? dummy;

    // setting helper for de/serializing the AppSettings
    // and to save and load from xml file
    private Setting setting = new();

    // global static config object
    // did not work in constructor or as property!
    #pragma warning disable CA2211
    public static AppSettings Config = new();
    #pragma warning restore CA2211
    public AppSettings()
    {
        
        PersonMoveRate.DoubleSeriesFrom = new DoubleSeries([1,1,1,1,1,1,1,1,1,1]);
        PersonMoveRate.DoubleSeriesTo = new DoubleSeries([2,2,2,2,2,2,2,100,100,200]);
        VirusMoveRate.DoubleSeriesFrom = new DoubleSeries([1,1,1,1,1,1,1,1,1,1]);
        VirusMoveRate.DoubleSeriesTo = new DoubleSeries([2,2,2,2,2,2,2,100,100,200]);
        // in main Form you can fill it like this
        // AppSettings.Config.VirusMoveRate.DoubleSeriesFrom = new DoubleSeries([1,1,1,1,1,1,1,1,1,1]);
        // AppSettings.Config.VirusMoveRate.DoubleSeriesTo = new DoubleSeries([2,2,2,2,2,2,2,2,2,2]);
    }

    // get app Version
    private static string GetAppVersion()
    {
        Assembly? appAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        if (appAssembly == null)
        {
            return "error Assembly version not found";
        }
        else
        {
            return appAssembly.GetName()?.Version?.ToString() ?? "error Assembly name not found";
        }
    }

    [ExcludeFromSerialization]
    [Browsable(false)]
    public Setting Setting
    {
        get => setting;
        set => setting = value;
    }


    #region properties settings


    //-> category in grid
    [CategoryAttribute("Internal Settings")]     
    // -> grid value is editable
    [ReadOnlyAttribute(false)]
    // -> show description in grid  
    [Description("Hide internal used properties in PropertyGrid")]
    public bool ShowHelperSettings
    {
        get => showHelperSettings;
        set => showHelperSettings = value;
    }

    [CategoryAttribute("Internal Settings")]
    [Description("Internal use - to save actual MainForm size and position")] 
    [Browsable(false)] //-> hide from Grid if Property showHelperSettings is false
    public Point Form_Config_WindowLocation
    {
        get => form_Config_WindowLocation;
        set => form_Config_WindowLocation = value;
    }
    
    [CategoryAttribute("Internal Settings")]
    [Description("Internal use - to save actual MainForm size and position")]
    [Browsable(false)]
    public Size Form_Config_WindowSize
    {
        get => form_Config_WindowSize;
        set => form_Config_WindowSize = value;
    }

    [CategoryAttribute("Internal Settings")]
    [Description("Internal use - to save actual PlotForm size and position")]
    [Browsable(false)] //-> hide from Grid if Property showHelperSettings is false
    public Point PlotForm_WindowLocation
    {
        get => plotForm_WindowLocation;
        set => plotForm_WindowLocation = value;
    }

    [CategoryAttribute("Internal Settings")]
    [Description("Internal use - to save actual PlotForm size and position")]
    [Browsable(false)]

    public Size PlotForm_WindowSize
    {
        get => plotForm_WindowSize;
        set => plotForm_WindowSize = value;
    }

    [CategoryAttribute("Internal Settings")]
    [Description("Internal use - to save actual PhaseChartForm size and position")]
    [Browsable(false)] //-> hide from Grid if Property showHelperSettings is false
    public Point PhaseChartForm_WindowLocation
    {
        get => phaseChartForm_WindowLocation;
        set => phaseChartForm_WindowLocation = value;
    }

    [CategoryAttribute("Internal Settings")]
    [Description("Internal use - to save actual PahseChartFomr size and position")]
    [Browsable(false)]

    public Size PhaseChartForm_WindowSize
    {
        get => phaseChartForm_WindowSize;
        set => phaseChartForm_WindowSize = value;
    }

    [CategoryAttribute("App Info"), ReadOnlyAttribute(true)]
    [Description("Dedicated to a lighthouse in stormy seas\r\nMIT License" +
        "\r\nAttention! Some configuration settings such as the colors require a restart of the application" +
        "\r\nSome settings only take effect after the simulation-chart forms are closed and reopened")]
    [ExcludeFromSerialization]  // -> XmlSerializer will not serialize the object
    public string About
    {
        get => about;
        set => about = value;
    }

    [CategoryAttribute("App Info"), ReadOnlyAttribute(true)]
    public string AppVersion
    {
        get => appVersion;
        set => dummy = value;  // don't overrwrite with value from xml
    }

    [CategoryAttribute("Grid Settings")]
    [Description("Width of the Grid Filed - pixel")]
    public int GridMaxX
    {
        get => gridMaxX;
        set => gridMaxX = value;
    }

    [CategoryAttribute("Grid Settings")]
    [Description("Higth of the Grid Filed - pixel")]
    public int GridMaxY
    {
        get => gridMaxY;
        set => gridMaxY = value;
    }

    [CategoryAttribute("Grid Settings")]
    [Description("Timer in milli seconds : standard 1 ms - bigger values slows down the iterations and the redraw of grid field form")]
    // return ((int)(this["GridFormTimer"])) this["GridFormTimer"] = value;
    public int GridFormTimer
    {
        get => gridFormTimer;
        set => gridFormTimer = value;
    }


    [CategoryAttribute("Grid Settings")]
    [Description("false by default: If true, the historical movement can be tracked over the entire time.\n\n" +
        "This means that the color of the grid coordinate (representing the state) is not deleted or refreshed after a person or virus has moved." +
        "\n\nIt is only updated again after the grid cell is re-entered.")]
    public bool TrackMovment
    {
        get => trackMovment;
        set => trackMovment = value;
    }

    [CategoryAttribute("Grid Settings")]
    [Description("default false: slows down iterations. True -> shading cell color depending on population count. Higher population numbers are shown in darker color")]
    public bool PopulationDensityColoring
    {
        get => populationDensityColoring;
        set => populationDensityColoring = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("Start poulation for Persons - long")]
    public long InitialPersonPopulation
    {
        get => initialPersonPopulation;
        set => initialPersonPopulation = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("allow person global moovment - true : movement within the distance limit only from the home coordinate.\r\nfalse: movement within the distance limit from the new current coordinate, therefore over the entire grid field possible")]
    public bool PersonMoveGlobal
    {
        get => personMoveGlobal;
        set => personMoveGlobal = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("integer 0 to X % \r\n0 = don't move, 1 = move every iteration, 2 = move random average every 2nd iteration, X = move average every Xd iteration")]
    public int PersonMoveActivityRnd
    {
        get => personMoveActivityRnd;
        set 
        {
            if (value >= 0 )personMoveActivityRnd = value;
            else personMoveActivityRnd = 1;
        } 
    }

    [CategoryAttribute("Person Settings")]
    [Description("integer 0 to X % \r\n0 = don't move back home, 1 = move back home every iteration, 2 = move back home random average every 2nd iteration, X = move back home random average every Xd iteration")]
    public int PersonMoveHomeActivityRnd
    {
        get => personMoveHomeActivityRnd;
        set
        {
            //(value >= 0 && value <= 1) -> use for doubles between 0-1
            if (value >= 0 ) personMoveHomeActivityRnd = value; 
            else personMoveHomeActivityRnd = 1;
        }
    }

    [CategoryAttribute("Person Settings")]
    [Description("Period from the start of the infection during which a person is asymptomatic but not yet infectious. Only then does it become infectious" +
        "\r\nStandard value 2 days (iterations)")]
    public int PersonLatencyPeriod
    {
        get => personLatencyPeriod;
        set => personLatencyPeriod = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("Period during which a person is infectious, after it ther person gets immune during the PersonReinfectionImmunityPeriod \r\nDefault 9 days (iterations)")]
    public int PersonInfectiousPeriod
    {
        get => personInfectiousPeriod;
        set => personInfectiousPeriod = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("Immunity period during which a person is immune after recovering from an infection, but a certain percentage can be re-infected." +
        "\r\nStandard 5 months = 155 days (iterations)" +
        "\r\nDuring this period, a certain percentage of people who are in the immunity period can potentially be re-infected.This percentage is determined by the PersonReinfectionRate." +
        "\r\nIf a person has not been re-infected during the immunity period, they will again receive the PersonReinfectionImmunityPeriod health status and there immunity cycle will repeat itself." +
        "\r\nSpecial cases:" +
        "\r\n0 = everyone can potentially be reinfected" +
        "\r\n100 = no one in the immunity period can be re-infected")]

    public int PersonReinfectionImmunityPeriod
    {
        get => personReinfectionImmunityPeriod;
        set => personReinfectionImmunityPeriod = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("Percentage of all persons in the immunity period after an illness who could potentially become newly infected - decimal value between 0 and 100" +
        "\r\nDefault value = 11%" +
        "\r\n0%=no person can become infected again, the PersonReinfectionImmunityPeriod cycle for all persons starts again from the beginning." +
        "\r\n100%=every person can potentially be infected again, they receive the status HealthyRecoverd and can potentially be infected again\n" +
        "\r\nIs this realistic, the implementation does have a big impact on the results?")]
        
    public double  PersonReinfectionRate
    {
        get
        {
            return personReinfectionRate;
        }
        set 
        {
            personReinfectionRate = value;
            if (personReinfectionRate < 0) { personReinfectionRate = 0; }
            if (personReinfectionRate > 100) { personReinfectionRate = 100; }
        }  
    }
    
    [CategoryAttribute("Virus Settings")]
    [Description("Start poulation for Viruses - long")]
    public long InitialVirusPopulation
    {
        get => initialVirusPopulation;
        set => initialVirusPopulation = value;
    }

    [CategoryAttribute("Virus Settings")]
    [Description("allow virus global movement - true : movement within the distance limit only from the home coordinate. " +
        "\r\nfalse: Movement within the distance limit is possible from the new current coordinate, i.e. over the entire grid field")]
    public bool VirusMoveGlobal
    {
        get => virusMoveGlobal;
        set => virusMoveGlobal = value;
    }

    [CategoryAttribute("Virus Settings")]
    [Description("integer 0 to X % \r\n0 = don't move, 1 = move every iteration, 2 = move random average every 2nd iteration, " +
        "X = move back home random average every Xd iteration")]
    public int VirusMoveActivityRnd
    {
        get => virusMoveActivityRnd;
        set
        {
            if (value >= 0 ) virusMoveActivityRnd = value;
            else virusMoveActivityRnd = 1;
        }
    }

    [CategoryAttribute("Virus Settings")]
    [Description("integer 0 to X % \r\n0 = don't move back home, 1 = move back home every iteration, " +
        "2 = move back home random average every 2nd iteration, X = move back home random average every Xd iteration")]
    public int VirusMoveHomeActivityRnd
    {
        get => virusMoveHomeActivityRnd;
        set
        {
            if (value >= 0 ) virusMoveHomeActivityRnd = value;
            else virusMoveHomeActivityRnd = 1;
        }
    }
    
    [CategoryAttribute("Virus Settings")]
    [Description("Number of days/iterations in which a virus is infectious, i.e. has not decay" +
    "\r\n0= always intact/contagious never decays")]
    public int VirusInfectionDurationDays
    {
        get => virusInfectionDurationDays;
        set => virusInfectionDurationDays = value;
    }

    [CategoryAttribute("Virus Settings")]
    [Description("integer 0 to X Nuber of Viruses reproduced per day/iteration during the infectious phase of a person" +
       "Default=1")]
    public int VirusNumberReproducedPerIteration
    {
        get => virusNumberReproducedPerIteration;
        set
        {
            if (value >= 0) virusNumberReproducedPerIteration = value;
            else virusNumberReproducedPerIteration = 1;
        }
    }

    [CategoryAttribute("Virus Settings")]
    [Description("Virus reproduction rate during the infectious phase of a person" +
        "\r\nDefault value = 100%" +
        "\r\n0%= no virus reproduction " +
        "\r\n100%= during infection phase of a person, one new virus is generated per day/iteration at the person's current coordinates" +
        "\r\n50%= during a person's infection phase, a new virus is reproduced at the person's current coordinates, approximately every other day/iteration")]
    public double VirusReproductionRate
    {
        get
        {
            return virusReproductionRate;
        }
        set
        {
            virusReproductionRate = value;
            if (virusReproductionRate < 0) { virusReproductionRate = 0; }
            if (virusReproductionRate > 100) { virusReproductionRate = 100; }
        }
    }

    [CategoryAttribute("App Settings")]
    [Description("Number of maximal iterations for the Simulation - long")]
    public long MaxIterations
    {
        get => maxIterations;
        set => maxIterations = value;
    }

    [CategoryAttribute("Move Rate"), ReadOnlyAttribute(false)]
    [DescriptionAttribute()]  
    [ExcludeFromSerialization]
    [Browsable(false)]
    public DoubleSeriesClass PersonMoveRate
    {
        get => personMoveRate;
        set => personMoveRate = value;
    }

    [CategoryAttribute("Move Rate Person"), ReadOnlyAttribute(false)]
    [DescriptionAttribute("motion profile - see below:\r\n\r\nused to simulate frequent short and rare long distance moves of people" +
        "\r\nrandom chooses one range from the distance range serie\r\n"+
        "then get a random distance within the selected distance range\r\nget a random direction 365° \r\nreturn the NewGridCoordinate to move to" +
        "\r\n\r\nPersonMoveRateFrom \r\nholds a series of the upper maximum ranges a person can move \r\nif the person moves a random range from the range serie is choosen\r\n"+
        "this can be used to simulate tavel behavior for example rear far and frequent low distance movment of persons")]
    public string PersonMoveRateFrom
    {
        get
        {
            personMoveRateFrom = personMoveRate.DoubleSeriesFrom.ToString()!;
            if (personMoveRateFrom is null) 
            { 
                return ""; 
            }
            else return personMoveRateFrom;
        }
        set
        {
            personMoveRateFrom = value;
            personMoveRate.DoubleSeriesFrom = DoubleSeries.Parse(value);
        }
    }
    
    [CategoryAttribute("Move Rate Person"), ReadOnlyAttribute(false)]
    [DescriptionAttribute("series for the lower minimum range a person can move")]
    public string? PersonMoveRateTo
    {
        get
        {
            personMoveRateTo = personMoveRate.DoubleSeriesTo.ToString()!;
            return personMoveRateTo;
        }
        set 
        {
            personMoveRateTo = value!;
            personMoveRate.DoubleSeriesTo = DoubleSeries.Parse(value!); 
        }
        
    }

    [CategoryAttribute("Move Rate"), ReadOnlyAttribute(false)]
    [ExcludeFromSerialization]
    [Browsable(false)]
    public DoubleSeriesClass VirusMoveRate
    {
        get => virusMoveRate;
        set => virusMoveRate = value;
    }

    [CategoryAttribute("Move Rate Virus"), ReadOnlyAttribute(false)]
    [DescriptionAttribute("motion profile - see below:\r\n\r\nused to simulate moving behavior\r\nrandom chooses one range from the distance range serie\r\n"+
                         "then get a random distance within the selected choosed range\r\nget a random direction 365° \r\nreturn the NewGridCoordinate for to move to" +
                         "\r\n\r\nVirusMoveRateFrom \r\nholds a series of the lower limit ranges a virus can move \r\nrandom a range from the range serie will be chosen\r\n" +
                          "this can be used to simulate spread behavior of a virus for example in a airborn scenario\r\n")]
    public string VirusMoveRateFrom
    {
        get
        {
            virusMoveRateFrom = virusMoveRate.DoubleSeriesFrom.ToString()!;
            if (virusMoveRateFrom is null)
            {
                return "";
            }
            else return virusMoveRateFrom;
        }
        set
        {
            virusMoveRateFrom = value;
            virusMoveRate.DoubleSeriesFrom = DoubleSeries.Parse(value);
        }
    }

    
    [CategoryAttribute("Move Rate Virus"), ReadOnlyAttribute(false)]
    [DescriptionAttribute("series of the uper limit maximum ranges a person can move")]
    public string? VirusMoveRateTo
    {
        get
        {
            virusMoveRateTo = virusMoveRate.DoubleSeriesTo.ToString()!;
            return virusMoveRateTo;
        }
        set
        {
            virusMoveRateTo = value!;
            virusMoveRate.DoubleSeriesTo = DoubleSeries.Parse(value!);
        }

    }

    [CategoryAttribute("Global Settings")]
    [Editor(typeof(UIFilenameEditor), typeof(UITypeEditor))]
    [UIFilenameEditor.SaveFileAttribute] // -> default is openFile
    [DescriptionAttribute("path to the application xml configuration file, to read and store configuration settings")]
    public string ConfigFilePath
    {
        get 
        {            
            return Setting.GetLastConfigFilePath(configFilePath);
        }
        set 
        {
            Setting.SetLastConfigFilePath(value);
            configFilePath = Setting.GetLastConfigFilePath(configFilePath);
        } 
    }

    [CategoryAttribute("Global Settings")]
    [Editor(typeof(UIFilenameEditor), typeof(UITypeEditor))]
    [UIFilenameEditor.SaveFileAttribute] // -> default is openFile
    [DescriptionAttribute("path to csv file to write the plot data, data are then read by the plot app to plot the data diagram")]
    public string CsvFilePath
    {
        get
        {
            return csvFilePath;
        }
        set
        {
            csvFilePath = value;
        }
    }

    [CategoryAttribute("Grid Settings")]
    [Description("false: The simulation Grid window is not shown on start, the checkbox is unchecked\r\n" +
        "true: The simulation Grid window is shown on start, the checkbox is checked")]
    public bool ShowSimulationGridWindow
    {
        get => showSimulationGridWindow;
        set => showSimulationGridWindow = value;
    }

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: WhiteSmoke")]
    [ExcludeFromSerialization]
    public Color VirusColor
    {
        get
        {
            return (Color)virusColor;
        }
        set
        {
            virusColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("VirusColor")]
    public string XmlVirusColor
    {
        get
        {
            xmlVirusColor = Setting.ToXmlColor(virusColor);
            return xmlVirusColor;
        }
        set
        {
            xmlVirusColor = value;
            virusColor = Setting.FromXmlColor(value);
        }
    }

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: Blue")]
    [ExcludeFromSerialization]
    public Color PersonsHealthyOrRecoverdColor
    {
        get
        {
            return (Color)personsHealthyOrRecoverdColor;
        }
        set
        {
            personsHealthyOrRecoverdColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("PersonsHealthyOrRecoverdColor")]
    public string XmlPersonsHealthyOrRecoverdColor
    {
        get
        {
            xmlPersonsHealthyOrRecoverdColor = Setting.ToXmlColor(personsHealthyOrRecoverdColor);
            return xmlPersonsHealthyOrRecoverdColor;
        }
        set
        {
            xmlPersonsHealthyOrRecoverdColor = value;
            personsHealthyOrRecoverdColor = Setting.FromXmlColor(value);
        }
    }

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: DeepSkyBlue")]
    [ExcludeFromSerialization]
    public Color PersonInfectedColor
    {
        get
        {
            return (Color)personsInfectedColor;
        }
        set
        {
            personsInfectedColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("PersonsInfectedColor")]
    public string XmlPersonsInfectedColor
    {
        get
        {
            xmlPersonsInfectedColor = Setting.ToXmlColor(personsInfectedColor);
            return xmlPersonsInfectedColor;
        }
        set
        {
            xmlPersonsInfectedColor = value;
            personsInfectedColor = Setting.FromXmlColor(value);
        }
    }       

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: ")]
    [ExcludeFromSerialization]
    public Color PersonInfectiousColor
    {
        get
        {
            return (Color)personsInfectiousColor;
        }
        set
        {
            personsInfectiousColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("PersonsInfectiousColor")]
    public string XmlPersonsInfectiousColor
    {
        get
        {
            xmlPersonsInfectiousColor = Setting.ToXmlColor(personsInfectiousColor);
            return xmlPersonsInfectiousColor;
        }
        set
        {
            xmlPersonsInfectiousColor = value;
            personsInfectiousColor = Setting.FromXmlColor(value);
        }
    }

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: ")]
    [ExcludeFromSerialization]
    public Color PersonsRecoverdImmuneNotInfectiousColor
    {
        get
        {
            return (Color)personsRecoverdImmuneNotInfectiousColor;
        }
        set
        {
            personsRecoverdImmuneNotInfectiousColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("PersonsRecoverdImmuneNotInfectiousColor")]
    public string XmlPersonsRecoverdImmuneNotInfectiousColor
    {
        get
        {
            xmlPersonsRecoverdImmuneNotInfectiousColor = Setting.ToXmlColor(personsRecoverdImmuneNotInfectiousColor);
            return xmlPersonsRecoverdImmuneNotInfectiousColor;
        }
        set
        {
            xmlPersonsRecoverdImmuneNotInfectiousColor = value;
            personsRecoverdImmuneNotInfectiousColor = Setting.FromXmlColor(value);
        }
    }


    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: Black: chang does not work yet, because skglControl has transparent (black) background")]
    [ExcludeFromSerialization]
    public Color EmptyCellColor
    {
        get
        {
            return (Color)emptyCellColor;
        }
        set
        {
            emptyCellColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("EmptyCellColor")]
    public string XmlEmptyCellColor
    {
        get
        {
            xmlEmptyCellColor = Setting.ToXmlColor(emptyCellColor);
            return xmlEmptyCellColor;
        }
        set
        {
            xmlEmptyCellColor = value;
            emptyCellColor = Setting.FromXmlColor(value);
        }
    }

    [CategoryAttribute("Plot Settings")]
    [Description("saves the choosed visability, of the fourten data lines of PlotChart, in the config file")]
    public LegendVisability LegendVisability
    {
        get => legendVisability;
        set => legendVisability = value;
    }

    [CategoryAttribute("Plot Settings")]
    [Description("Index of the selected Listbox Item used for the X Value in the phase chart, to store the selcted entry")]
    public int PhaseChartXSelectedIndex
    {
        get => phaseChartXSelectedIndex;
        set 
        {
            phaseChartXSelectedIndex = value;
            if (phaseChartXSelectedIndex < 0) { phaseChartXSelectedIndex = 0; }
            if (phaseChartXSelectedIndex > 13) { phaseChartXSelectedIndex = 13; }
        } 
    }

    [CategoryAttribute("Plot Settings")]
    [Description("Index of the selected Listbox Item used for the Y Value in the phase chart, to store the selcted entry")]
    public int PhaseChartYSelectedIndex
    {
        get => phaseChartYSelectedIndex;
        set
        {
            phaseChartYSelectedIndex = value;
            if (phaseChartYSelectedIndex < 0) { phaseChartYSelectedIndex = 0; }
            if (phaseChartYSelectedIndex > 13) { phaseChartYSelectedIndex = 13; }
        }
    }

} // end APP Settings 


// useed by PlotForm to save choosed visability
// of the fourteen plot lines in the config
public class LegendVisability 
{
    private bool[] legendVisabilityStatus = [true];
    public LegendVisability()
    {
        legendVisabilityStatus = new bool[14];
        for (int i = 0; i < 14; i++)
        {
            legendVisabilityStatus[i] = true;
        }

        // unchecked first five plotlines
        // better zoom for the small plot values
        for (int i = 0; i < 5; i++)
        {
            legendVisabilityStatus[i] = false;
        }
        //unchecked two last plotlines
        //-> plot influences the rendering performance of the simulation BUG?
        for (int i = 12; i < 14; i++)
        {
            legendVisabilityStatus[i] = false;
        }
    }
    public bool[] LegendVisabilitySatus
    {
        get => legendVisabilityStatus;
        set => legendVisabilityStatus = value;
    }
    public bool this[int index] // <- indexer declaration
    {
        get => legendVisabilityStatus[index];
        set => legendVisabilityStatus[index] = value;
    }
}

#endregion  properties settings







// for debug tests -> serialize and deserializes only this class 
//
// run it from a Form like this:
//
// GenericDictionary Case = new GenericDictionary();
// Setting.SerializeTheClass(Case);          // <- serialize to xml and deserialize from xml file
// PropertyGrid1.SelectedObject = Case;      // <- assing the obj to the grid
public class GenDictionary : ClassSerializer
{
    //private GenDictionary genDictionary;
    private Dictionary<int, string> genDictionary = [];
    public GenDictionary()
    {
        genDictionary.Add(5, "five");
        genDictionary.Add(10, "ten");
        genDictionary.Add(20, "twenty");
        this.Source = genDictionary;
    }

    public Dictionary<int, string> GetSetGenDictionary
    {
        get => genDictionary;
        set => genDictionary = value;
    }

    //check if result and source are identical
    public override void ResultReview(object result)
    {

        var s = (Dictionary<int, string>?)Source;
        var r = (Dictionary<int, string>)result;
        if (s != null)
        {
            AreEqual(s[5], r[5]);
            AreEqual(s[10], r[10]);
            AreEqual(s[20], r[20]);
        }
    }

}

// This would have ben probably a better wayto store complex Objects to the config xml
// would probably have save a lot of workarounds for color or MovmentRates above

//// Create a new serializer
//var serializer = new SharpSerializer();
//// Set the append mode to property
//serializer.AppendMode = AppendMode.Property;
//// Serialize the window state to the existing file
//serializer.Serialize(this.WindowState, "config.xml");
//// This is the save button
//XmlSerializer serializer = new XmlSerializer(typeof(FormWindowState));
//using (StreamWriter writer = new StreamWriter("windowstate.xml"))
//{
//    serializer.Serialize(writer, this.WindowState);
//}

//// This is the retrieve (use it in the form load event for example)
//XmlSerializer serializer = new XmlSerializer(typeof(FormWindowState));
//using (StreamReader reader = new StreamReader("windowstate.xml"))
//{
//    this.WindowState = (FormWindowState)serializer.Deserialize(reader);
//}


