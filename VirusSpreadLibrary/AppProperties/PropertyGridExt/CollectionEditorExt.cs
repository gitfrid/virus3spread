using System.ComponentModel.Design;

namespace VirusSpreadLibrary.AppProperties.PropertyGridExt;


public class CollectionEditorExt(Type type) : CollectionEditor(type)
{
    public delegate void EditorFormClosedEventHandler(object sender,FormClosedEventArgs e);

    public static event EditorFormClosedEventHandler? EditorFormClosed;

    protected override CollectionForm CreateCollectionForm()
    {
        CollectionForm collectionForm = base.CreateCollectionForm();
        collectionForm.FormClosed += new FormClosedEventHandler(Collection_FormClosed);
        return collectionForm;
    }

    void Collection_FormClosed(object? sender, FormClosedEventArgs e)
    {
        EditorFormClosed?.Invoke(this, e);
    }
}
