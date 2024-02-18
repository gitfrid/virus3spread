using System.ComponentModel;
using System.Drawing.Design;

namespace VirusSpreadLibrary.AppProperties.PropertyGridExt;

public class UIFilenameEditor : System.Drawing.Design.UITypeEditor
{				
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
	{
		if (context is not null && context.Instance is not null 
			&& context.PropertyDescriptor is not null)
		{
			if (!context.PropertyDescriptor.IsReadOnly)
			{
				return UITypeEditorEditStyle.Modal;
			}
		}
		return UITypeEditorEditStyle.None;
	}
	
	[RefreshProperties(RefreshProperties.All)]
    public override object? EditValue(ITypeDescriptorContext? context, System.IServiceProvider provider, object? value)
	{
		if (context is null || provider is null || context.Instance is null)
		{
			if ( provider is not null) 
			{
                return base.EditValue(provider, value);
            }			
		}
		
		FileDialog fileDlg;

		if (context is not null && context.PropertyDescriptor is not null && context.PropertyDescriptor.Attributes[typeof(SaveFileAttribute)] is null)
		{
			fileDlg = new OpenFileDialog();
		}
		else
		{
			fileDlg = new SaveFileDialog();
		}
        if (context is not null && context.PropertyDescriptor is not null)
        {
			fileDlg.Title = "Select " + context.PropertyDescriptor.DisplayName;
		}
        
		if (value is not null) 
		{
            fileDlg.InitialDirectory = Path.GetDirectoryName((string)value);
            fileDlg.FileName = Path.GetFileName((string)value);
        }
        
        fileDlg.DefaultExt = Path.GetExtension(fileDlg.FileName.ToString());
        fileDlg.Filter = fileDlg.DefaultExt + "|*" + Path.GetExtension(fileDlg.FileName.ToString());
        
		FileDialogFilterAttribute filterAtt = new("*.*");
        if (context is not null && context.PropertyDescriptor is not null && context.PropertyDescriptor.Attributes is not null)
		{
            filterAtt = (FileDialogFilterAttribute)context.PropertyDescriptor.Attributes[typeof(FileDialogFilterAttribute)]!;
        }		
		if (filterAtt is not null)
		{
			fileDlg.Filter = filterAtt.Filter;
		}
		if (fileDlg.ShowDialog() == DialogResult.OK)
		{
			value = fileDlg.FileName;
		}
		fileDlg.Dispose();
		return value;
	}
	
	[AttributeUsage(AttributeTargets.Property)]
    public class FileDialogFilterAttribute(string filter) : Attribute
	{			
		private readonly string filter = filter;
		
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SaveFileAttribute : Attribute
	{
		
	}	
	public enum FileDialogType
	{
		LoadFileDialog,
		SaveFileDialog
	}
}
