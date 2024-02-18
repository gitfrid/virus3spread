using System.ComponentModel;
using System.Globalization;


namespace VirusSpreadLibrary.AppProperties.PropertyGridExt;

internal class DoubleSeriesConverter : TypeConverter
{
public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
{
  return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
}

public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
{
  return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
}

public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
{
  if (value is string s && !string.IsNullOrEmpty(s))
  {
    return DoubleSeries.Parse(s);
  }
  else
  {
     return base.ConvertFrom(context, culture, value);
  }
}

public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
{
   
    if (value is DoubleSeries doubleSeries && destinationType == typeof(string) )
    {
       // explode dynArray to string for property gird editing
       return  string.Format("{0}", doubleSeries.DoubleArray.ToString());
    }
    else
    {
       return base.ConvertTo(context, culture, value, destinationType)!;
    }        
}


}


// changed source from here:

// Creating a custom type converter part 3: Types to string
// https://www.cyotek.com/blog/creating-a-custom-type-converter-part-3-types-to-string

// This work is licensed under the Creative Commons Attribution 4.0 International License.
// http://creativecommons.org/licenses/by/4.0/.
