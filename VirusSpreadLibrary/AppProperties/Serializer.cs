
using Polenter.Serialization;

namespace VirusSpreadLibrary.AppProperties;

public class Setting
{
    private readonly SaveFileDialog saveFileDialog = new();
    private readonly OpenFileDialog openFileDialog = new();
    private static readonly char[] separator = [':'];

    public Setting()
    {
        //
    }
    private static SharpSerializerXmlSettings SerializerXmlSettings()
    {
        // for more options see: -> SharpSerializer library -> HelloWorldApp.csproj -> Form1
        // or here: -> "C:\AppPropertiesSharpSerializer\AppProperties\Doku\SharpSerializer_Settings.pdf"

        var settings = new SharpSerializerXmlSettings();
        //settings.IncludeAssemblyVersionInTypeName = true;
        //settings.IncludeCultureInTypeName = true;
        //settings.IncludePublicKeyTokenInTypeName = true;
        //settings.Culture = System.Globalization.CultureInfo.CurrentCulture;
        //remove default ExcludeFromSerializationAttribute for performance gain
        return settings;
    }

    public static void SerializeT<T>(T Obj, Stream stream)
    {
        var serializer = new SharpSerializer();
        serializer.Serialize(Obj, stream);
    }

    public static T DeserializeT<T>(Stream stream)
    {
        var serializer = new SharpSerializer();
        return (T)serializer.Deserialize(stream);
    }

    private void Deserialize(bool openFromFile = false)
    {
        var serializer = new SharpSerializer(SerializerXmlSettings());
        string fileName = string.Empty;
                
        if (openFromFile)
        {
            string ConfigFilePath = AppSettings.Config.ConfigFilePath.ToString();
            if (File.Exists(ConfigFilePath))
            {
                openFileDialog.FileName = Path.GetFileName(ConfigFilePath);
                openFileDialog.InitialDirectory = Path.GetDirectoryName(ConfigFilePath);
                openFileDialog.DefaultExt = Path.GetExtension(openFileDialog.FileName.ToString());
                openFileDialog.Filter = openFileDialog.DefaultExt + "|*"
                    + Path.GetExtension(openFileDialog.FileName.ToString());
            }
            if (DialogResult.OK != openFileDialog.ShowDialog()) return;
            fileName = openFileDialog.FileName;
            AppSettings.Config.ConfigFilePath = fileName;
        }

        // remove default ExcludeFromSerializationAttribute for performance
        try
        {
            if (openFromFile)
            {
                AppSettings.Config = (AppSettings)serializer.Deserialize(fileName);
            }
            else
            {
                string ConfigFile = AppSettings.Config.ConfigFilePath.ToString();
                if (File.Exists(ConfigFile))
                {
                    AppSettings Conf = (AppSettings)serializer.Deserialize(ConfigFile);
                    AppSettings.Config = Conf;
                }
                else 
                {
                    MessageBox.Show(string.Format("Config file not found: {0}", ConfigFile));
                }                
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Something went wrong." + ex);
            if (ex.InnerException != null)
                MessageBox.Show("Inner Exception:" + ex.InnerException.ToString());
        }
    }

    private void Serialize(bool saveToFile = false)
    {
        var serializer = new SharpSerializer(SerializerXmlSettings());
        string fileName = string.Empty;

        if (saveToFile)
        {
            string ConfigFilePath = AppSettings.Config.ConfigFilePath.ToString();
            if (File.Exists(ConfigFilePath))
            {
                saveFileDialog.FileName = Path.GetFileName(ConfigFilePath);
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(ConfigFilePath);
                saveFileDialog.DefaultExt = Path.GetExtension(saveFileDialog.FileName.ToString());
                saveFileDialog.Filter = saveFileDialog.DefaultExt + "|*" 
                    + Path.GetExtension(saveFileDialog.FileName.ToString());
            }
            if (DialogResult.OK != saveFileDialog.ShowDialog())
                return;
            fileName = saveFileDialog.FileName;
            AppSettings.Config.ConfigFilePath = fileName;
        }

        try
        {
            if (saveToFile)
                serializer.Serialize(AppSettings.Config, fileName);
            else 
            {
                string ConfigFile = AppSettings.Config.ConfigFilePath.ToString();
                serializer.Serialize(AppSettings.Config, ConfigFile);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Something went wrong." + ex);
            if (ex.InnerException != null)
                MessageBox.Show("Inner Exception:" + ex.InnerException.ToString());
        }
    }

    // get file in %appdata%AppName\LastConfigFileLocation.XML
    // which stores the path to the last used Application Config xmlFile
    private static string LastConfigPathXmlFile()
    {
        string appName = Path.GetFileNameWithoutExtension(System.AppDomain.CurrentDomain.FriendlyName);
        return new(string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"\", appName, @"\LastConfigFileLocation.XML"));
    }
    public static void SetLastConfigFilePath(string ConfigFilePath)
    {
        GetSetLastConfigFilePath objLastConfigFilePath = new();
        string lastConfigPathXmlFile = LastConfigPathXmlFile();

        if (File.Exists(lastConfigPathXmlFile) == false)
        {
            //create directory
            string lastConfigPathXmlFilepath = Path.GetDirectoryName(lastConfigPathXmlFile) ?? "";
            Directory.CreateDirectory(lastConfigPathXmlFilepath);
        }
        objLastConfigFilePath.ConfigFilePath = ConfigFilePath;
        new SharpSerializer().Serialize(objLastConfigFilePath, lastConfigPathXmlFile);
    }

    public static string GetLastConfigFilePath(string DefaultConfigFilePath)
    {
        string lastConfigPathXmlFile = LastConfigPathXmlFile();
        
        if (File.Exists(lastConfigPathXmlFile) == false)
        {
            // return default ConfigFilePath location
            SetLastConfigFilePath(DefaultConfigFilePath);            
            return DefaultConfigFilePath; 
        }
        else
        {
            // return last ConfigFilePath from %appdata%AppName\LastConfigFileLocation.XML 
            GetSetLastConfigFilePath objLastConfigFilePath = (GetSetLastConfigFilePath)(new SharpSerializer().Deserialize(lastConfigPathXmlFile));
            return objLastConfigFilePath.ConfigFilePath;             
        }
    }

    // load appconfig form xml
    public void Load(bool openFromFile = false)
    {
        Deserialize(openFromFile);
    }

    // save appconfig to xml
    public void Save(bool saveToFile = false)
    {
        Serialize(saveToFile);
    }

    // reloads appconfig settings
    public void Reload()
    {
        Save();
        Load();
    }

    private static void SerializeClass(ClassSerializer ObjCase, SharpSerializer serializer, string XmlFileName = "")
    {

        if (XmlFileName == "") 
        {
            XmlFileName = string.Concat(Path.GetTempPath(), @"\", 
                            Path.GetFileNameWithoutExtension(System.AppDomain.CurrentDomain.FriendlyName), "_Class.XML");
        }

        // using var stream = new MemoryStream();
        // serializer.Serialize(testCase.Source, stream);
        serializer.Serialize(ObjCase.Source, XmlFileName);

        // reset stream
        // stream.Position = 0;

        // deserialize
        //var result = serializer.Deserialize(stream);
        var result = serializer.Deserialize(XmlFileName);

        // reset stream to test if it is not closed 
        // the stream will be closed by the user
        // stream.Position = 0;

        // fix assertions
        if (ObjCase.Source is not null)
        {
            ClassSerializer.AreEqual(ObjCase.Source.GetType(), result.GetType());
        }
        // custom assertions
        ObjCase.ResultReview(result);
    }
    
    public static void SerializeTheClass(ClassSerializer ObjCase) 
    {
        SerializeClass(ObjCase, new SharpSerializer());
    }

    #region Serialization Helpers
    public enum ColorFormat
    {
        NamedColor,
        ARGBColor
    }

    public static string ToXmlColor(Color color)
    {
        if (color.IsNamedColor)
            return string.Format("{0}:{1}", ColorFormat.NamedColor, color.Name);
        else
            return string.Format("{0}:{1}:{2}:{3}:{4}", ColorFormat.ARGBColor, color.A, color.R, color.G, color.B);
    }

    public static System.Drawing.Color FromXmlColor(string color)
    {
        byte a, r, g, b;

        string[] pieces = color.Split(separator);

        ColorFormat colorType = (ColorFormat)System.Enum.Parse(typeof(ColorFormat), pieces[0], true);

        switch (colorType)
        {
            case ColorFormat.NamedColor:
                return Color.FromName(pieces[1]);

            case ColorFormat.ARGBColor:
                a = byte.Parse(pieces[1]);
                r = byte.Parse(pieces[2]);
                g = byte.Parse(pieces[3]);
                b = byte.Parse(pieces[4]);

                return Color.FromArgb(a, r, g, b);
        }
        return Color.Empty;
    }
 
    #endregion

} //end setting


#region Helper classes/structs


//Class to writ or read the ConfigFile location
//from %appdata%AppName\LastConfigFileLocation.XML 
public class GetSetLastConfigFilePath 
{
    private string configFilePath = "";
    public string ConfigFilePath
    {
        get => configFilePath;
        set => configFilePath = value;
    }
}


//public class XmlFont
//{
//    public System.Drawing.Font font;
//    public string fontFamily = "Arial";
//    public GraphicsUnit graphicsUnit;
//    public float size;
//    public FontStyle style;

//    public XmlFont()
//    {
//        font = new("Arial", 8, FontStyle.Regular);
//    }

//    public string FontFamily
//    {
//        get => fontFamily;
//        set => fontFamily = value;
//    }

//    public GraphicsUnit GraphicsUnit
//    {
//        get => graphicsUnit;
//        set => graphicsUnit = value;
//    }
//    public float Size
//    {
//        get => size;
//        set => size = value;
//    }
//    public FontStyle Style
//    {
//        get => style;
//        set => style = value;
//    }
//    private Font ToFont()
//    {
//        return new Font(fontFamily, size, style, graphicsUnit);
//    }
//    public XmlFont To(Font SourceFont)
//    {
//        font = SourceFont;
//        fontFamily = font.FontFamily.Name;
//        graphicsUnit = font.Unit;
//        size = font.Size;
//        style = font.Style;
//        return this;
//    }
//    public Font FromXmlFont(XmlFont XmlFont)
//    {
//        return XmlFont.ToFont();
//    }
//}

#endregion

public abstract class ClassSerializer
{
    public ClassSerializer()
    {            
    }
    public ClassSerializer(object source)
    {
        Source = source;
    }
    public object? Source { get; set; }
    public abstract void ResultReview(object result);


    // Verifies that two specified generic type data are equal by using the equality
    public static void AreEqual(Object expected, Object actual)
    {
        AreEqual(expected, actual);
    }

    public static void AreEqual<T>(T expected, T actual)
    {
        if (!object.Equals(expected, actual))
        {
            if (actual == null || expected == null || actual.GetType().Equals(expected.GetType()))
            {
                MessageBox.Show("AreEqual check result: objects are different");
            }
        }
    }
}

// Settings uses nuget package sharpserializer
//
//
// home: https://www.sharpserializer.net/en/index.html
// git: https://github.com/polenter/SharpSerializer
// example: 
//
//


