using System.ComponentModel;
using System.Text;

namespace VirusSpreadLibrary.AppProperties.PropertyGridExt; 

[TypeConverter(typeof(DoubleSeriesConverter))]
public class DoubleSeries
{

    private readonly double[] doubleArr = [0];
    public DoubleSeries()
    {
      // for xml serialize 
    }

    public DoubleSeries(double[] DoubleArray)
    {
      doubleArr = DoubleArray;
    }

    public double[] DoubleArray
    {
      get { return doubleArr; }
    }

    public static DoubleSeries Parse(string PropertyGridString)
    {
      if (string.IsNullOrEmpty(PropertyGridString))
      {
        throw new ArgumentNullException(PropertyGridString);
      }
  
      string[] parts = PropertyGridString.Split(';');
      double[] dblArray = new double[parts.Length] ;
  
      if (parts.Length != 4)
      {
        //throw new ArgumentException("Value is not a doubleSeries.", nameof(s));
      }
  
      for (int i = 0 ; i < parts.Length; i++)
      {
         dblArray[i] = double.Parse(parts[i]); 
      }

      return new DoubleSeries(dblArray);
    }

    public override string? ToString()
    {
       if (doubleArr is null)
       {
           throw new ArgumentException("doubleArr is null");
       }
       else 
       {
            StringBuilder sb = new();
            for (int i = 0; i < doubleArr.Length  ; i++)
            {
                sb.Append(doubleArr[i].ToString() + ';');
            }
            return sb.ToString().TrimEnd(';');
       }      
    }
}


// changed source from here:

// Creating a custom type converter part 3: Types to string
// https://www.cyotek.com/blog/creating-a-custom-type-converter-part-3-types-to-string

// This work is licensed under the Creative Commons Attribution 4.0 International License.
// http://creativecommons.org/licenses/by/4.0/.

