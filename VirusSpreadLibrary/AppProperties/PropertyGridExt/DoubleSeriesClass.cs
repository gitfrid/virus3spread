using System.ComponentModel;

namespace VirusSpreadLibrary.AppProperties.PropertyGridExt;

public sealed class DoubleSeriesClass : INotifyPropertyChanged
{

private DoubleSeries doubleSeriesFrom = new([0]);

private DoubleSeries doubleSeriesTo = new([0]);

public event PropertyChangedEventHandler? PropertyChanged;

public DoubleSeriesClass()
{
    // for xml serialize
}

public DoubleSeries DoubleSeriesFrom
{
  get 
  { 
    return doubleSeriesFrom; 
  }
  set
  {
    doubleSeriesFrom = value;

    this.OnPropertyChanged(nameof(DoubleSeriesFrom));
  }
}

public DoubleSeries DoubleSeriesTo
{
  get { return doubleSeriesTo; }
  set
  {
    doubleSeriesTo = value;

    this.OnPropertyChanged(nameof(DoubleSeriesTo));
  }
}
private void OnPropertyChanged(string propertyName)
{
  PropertyChangedEventHandler handler;
  handler = PropertyChanged!;
  handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

}


// changed source from here:

// Creating a custom type converter part 3: Types to string
// https://www.cyotek.com/blog/creating-a-custom-type-converter-part-3-types-to-string

// This work is licensed under the Creative Commons Attribution 4.0 International License.
// http://creativecommons.org/licenses/by/4.0/.
