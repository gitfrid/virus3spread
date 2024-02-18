using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace VirusSpreadLibrary.AppProperties;

public class EventsListBox : ListBox
{
    public EventsListBox()
    {
      base.IntegralHeight = false;
      base.HorizontalScrollbar = true;
    }

    [DefaultValue(true)]
    public new bool HorizontalScrollbar
    {
      get { return base.HorizontalScrollbar; }
      set { base.HorizontalScrollbar = value; }
    }

    [DefaultValue(false)]
    public new bool IntegralHeight
    {
      get { return base.IntegralHeight; }
      set { base.IntegralHeight = value; }
    }

    public static void AddEvent(string eventName)
    {
      AddEvent(eventName);
    }

    public static void AddEvent(string eventName, IDictionary<string, object> values)
    {
      AddEvent(eventName, values);
    }

    public void AddEvent(Control sender, string eventName)
    {
      this.AddEvent(sender, eventName, EventArgs.Empty);
    }

    public void AddEvent(Control sender, string eventName, EventArgs args)
    {
        Dictionary<string, object> dictionary = new();

      foreach (PropertyInfo pi in args.GetType().GetProperties())
      {
        object? value;

        if (pi.GetIndexParameters().Length == 0)
        {
          try
          {
            value = pi.GetValue(args, null)!;
          }
          catch
          {
            // ignore errors
            value = null;
          }
        }
        else
        {
          // Not going to try and handler indexers
          value = null;
        }
        ((IDictionary<string, object>)dictionary).Add(pi.Name, value!);
      }
      this.AddEvent(sender, eventName, dictionary);
    }

    public void AddEvent(Control sender, string eventName, IDictionary<string, object> values)
    {
      StringBuilder eventData;

      eventData = new StringBuilder();

      eventData.Append(DateTime.Now.ToLongTimeString());
      eventData.Append('\t');
      if (sender != null)
      {
        eventData.Append(sender.Name);
        eventData.Append('.');
      }
      eventData.Append(eventName);
      eventData.Append('(');

      if (values != null)
      {
        int index;

        index = 0;

        foreach (KeyValuePair<string, object> value in values)
        {
          eventData.Append(value.Key);
          eventData.Append(" = ");
          eventData.Append(value.Value);

          if (index < values.Count - 1)
          {
            eventData.Append(", ");
          }
          index++;
        }
      }
      eventData.Append(')');

      this.Items.Add(eventData.ToString());
      this.TopIndex = this.Items.Count - this.ClientSize.Height / this.ItemHeight;
    }

}


// changed source from here:

// Creating a custom type converter part 3: Types to string
// https://www.cyotek.com/blog/creating-a-custom-type-converter-part-3-types-to-string

// This work is licensed under the Creative Commons Attribution 4.0 International License.
// http://creativecommons.org/licenses/by/4.0/.

