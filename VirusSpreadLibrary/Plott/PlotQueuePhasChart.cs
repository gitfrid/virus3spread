
using System.Collections.Concurrent;


namespace VirusSpreadLibrary.Plott;

public class PlotQueuePhaseChart
{
    // with a list of ten random Y-double values to  transfer between forms
    // create a FIFO threadsave ConcurrentQueue
    
    // to save and exchange doubles list to plot ten lines on PlotForm
    readonly private ConcurrentQueue<List<double>> queue1 = new();
    public void EnqueueList(List<double> values)
    {
        // add a doubles list to queue
        queue1.Enqueue(values);
    }
    public bool TryDequeueList(out List<double> values) =>
        // remove a doubles list from queue
        queue1.TryDequeue(result: out values!);

    public void ClearQueue()
    {
        queue1.Clear();
    }

}
