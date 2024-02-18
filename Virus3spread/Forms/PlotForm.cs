using ScottPlot;
using ScottPlot.Renderable;
using ScottPlot.Plottable;
using VirusSpreadLibrary.Plott;
using VirusSpreadLibrary.AppProperties;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;


namespace Virus3spread;

public partial class PlotForm : Form
{
    private readonly PlotData plotData;
    private readonly FormsPlot formsPlot;

    private readonly SignalPlot[] signalPlot = new SignalPlot[14];
    private readonly double[][] signalData = new double[14][];
    private int nextDataIndex = 0;
    
    private readonly Crosshair crosshair;
    public string Title = "Virus3Spread Diagram: Y-14 parameter, X-Number of iterations";

    public PlotForm(PlotData PlotData)
    {
        InitializeComponent();

        plotData = PlotData;

        // Add the FormsPlot
        formsPlot = new() { Dock = DockStyle.Fill };
        splitContainer1.Panel2.Controls.Add(formsPlot);

        //register the MouseMove event handler
        crosshair = formsPlot.Plot.AddCrosshair(0, 0);
        crosshair.HorizontalLine.PositionLabelFont.Size = 16;
        crosshair.VerticalLine.PositionLabelFont.Size = 16;
        formsPlot.MouseMove += FormsPlot_MouseMove;
        formsPlot.MouseEnter += FormsPlot_MouseEnter;
        FormsPlot_MouseLeave(null!, null!);

        Legend legend = formsPlot.Plot.Legend(enable: true, location: null);
        formsPlot.Plot.Palette = ScottPlot.Palette.Category20;

        // create forteen empty double data arrays with length of MaxIterations add it to the plot 
        for (int i = 0; i < 14; i++)
        {
            signalData[i] = new double[AppSettings.Config.MaxIterations];
            signalPlot[i] = formsPlot.Plot.AddSignal(signalData[i], 1, formsPlot.Plot.Palette.GetColor(i), string.Format("{0}", plotData.Legend[i].ToString()));
        }

        LegendListBox.Items.AddRange(plotData.Legend);
        LegendListBox.CheckOnClick = true;// <- change mode from double to single click

        // set visability of plot lines - legend
        for (int i = 0; i < LegendListBox.Items.Count; i++)
        {
            LegendListBox.SetItemChecked(i, AppSettings.Config.LegendVisability[i]); // -> load status from config
            signalPlot[i].IsVisible = LegendListBox.GetItemChecked(i);
        }

        // set timer intervall to enque data and refresh plot
        DataTimer.Interval = 10;
        DataTimer.Start();
        RenderTimer.Interval = 100;
        RenderTimer.Start();
        BtnHoldStart.BackColor = SystemColors.ControlLightLight;

        Closed += (sender, args) =>
        {
            DataTimer?.Stop();
            RenderTimer?.Stop();
        };
    }

    private void PlotForm_Load(object sender, EventArgs e)
    {
        RestoreWindowPosition();
    }
    private void DataTimer_Tick(object sender, EventArgs e)
    {

        if (nextDataIndex >= signalData[0].Length)
        {
            DataTimer?.Stop();
            RenderTimer?.Stop();
            throw new OverflowException("PlotForm data array isn't long enough to accomodate new data");
            //  a coding solution would be, 1. clear the plot, 2. create a new larger array,
            //  3. copy the old data into the start of the larger array, 4. plot the new (larger) array, 5. continue to update the new array
        }

        // enqueue in the simulate class and timing should be adjusted with dequeue here
        // use the n value below and the DataTimer / Refreshtimer intervalls
        for (int n = 0; n < 2; n++)
        {
            bool success = plotData.PlotDataQueue.TryDequeueList(out List<double> values);
            if (success)
            {
                for (int i = 0; i < signalData.Length; i++)
                {
                    signalData[i][nextDataIndex] = values[i];
                    signalPlot[i].MaxRenderIndex = nextDataIndex;
                }
                nextDataIndex += 1;
            }
            else
            {
                break;
            }
        }

        //formsPlot.Refresh();
        Text = $"Virus3spread Charts ({nextDataIndex:N0} points)";
    }
    private void RenderTimer_Tick(object sender, EventArgs e)
    {
        if (CbAutoAxis.Checked)
        {
            formsPlot.Plot.AxisAuto();
        }
        formsPlot.Refresh();
    }
    private void BtnHoldStart_Click(object sender, EventArgs e)
    {
        // sart stop plotting
        if (DataTimer.Enabled || RenderTimer.Enabled)
        {
            BtnHoldStart.BackColor = SystemColors.Control;
            DataTimer.Enabled = false;
            RenderTimer.Enabled = false;
        }
        else
        {
            BtnHoldStart.BackColor = SystemColors.ControlLightLight;
            DataTimer.Enabled = true;
            RenderTimer.Enabled = true;
        }
    }
    private void BtnManualScale_Click(object sender, EventArgs e)
    {
        CbAutoAxis.Checked = false;
        formsPlot.Plot.SetAxisLimits(0, 50, -20, 20, 0, 1);
        formsPlot.Plot.SetAxisLimits(0, 50, -20000, 20000, 0, 1);
        formsPlot.Refresh();
    }
    private void BtnAutoScale_Click(object sender, EventArgs e)
    {
        formsPlot.Plot.Margins();
        formsPlot.Plot.AxisAuto();
        formsPlot.Refresh();
    }
    private void BtnAutoScaleTight_Click(object sender, EventArgs e)
    {
        formsPlot.Plot.Margins(0, 0);
        formsPlot.Refresh();
    }
    private void BtnAutoScaleX_Click(object sender, EventArgs e)
    {
        formsPlot.Plot.AxisAutoX();
        formsPlot.Refresh();
    }
    private void BtnAutoScaleY_Click(object sender, EventArgs e)
    {
        formsPlot.Plot.AxisAutoY();
        formsPlot.Refresh();
    }
    private void ChkShowLegend_CheckedChanged(object sender, EventArgs e)
    {
        formsPlot.Plot.Legend(ChkShowLegend.Checked);
        formsPlot.Refresh();
    }

    private void CbAutoAxis_CheckedChanged(object sender, EventArgs e)
    {
        if (CbAutoAxis.Checked == false)
        {
            formsPlot.Plot.AxisAuto(verticalMargin: .5);
            var oldLimits = formsPlot.Plot.GetAxisLimits();
            formsPlot.Plot.SetAxisLimits(xMax: oldLimits.XMax + 1000);
        }
    }
    private void CbCorssHair_CheckedChanged(object sender, EventArgs e)
    {
        if (CbCorssHair.Checked == false)
        {
            crosshair.IsVisible = false;
        }
        else
        {
            crosshair.IsVisible = true;
        }
    }
    private void FormsPlot_MouseLeave(object sender, MouseEventArgs e)
    {
        crosshair.IsVisible = false;
        formsPlot.Refresh();
    }
    private void FormsPlot_MouseEnter(object? sender, EventArgs e)
    {
        if (CbCorssHair.Checked) { crosshair.IsVisible = true; }
    }
    private void FormsPlot_MouseMove(object? sender, MouseEventArgs e)
    {
        (double coordinateX, double coordinateY) = formsPlot.GetMouseCoordinates();
        crosshair.X = coordinateX;
        crosshair.Y = coordinateY;
        formsPlot.Refresh(lowQuality: false, skipIfCurrentlyRendering: true);
    }
    private void LegendListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (e.NewValue == CheckState.Checked)
        {
            signalPlot[e.Index].IsVisible = true;
            AppSettings.Config.LegendVisability[e.Index] = true; // <- write viability status to config
        }
        else
        {
            signalPlot[e.Index].IsVisible = false;
            AppSettings.Config.LegendVisability[e.Index] = false;
        }
        formsPlot.Refresh();
    }

    private void AllNoneCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        if (AllNoneCheckbox.Checked == false)
        {
            // set viability of plot lines / legend
            for (int i = 0; i < LegendListBox.Items.Count; i++)
            {
                LegendListBox.SetItemChecked(i, false);
                signalPlot[i].IsVisible = false;
            }
        }
        else
        {
            for (int i = 0; i < LegendListBox.Items.Count; i++)
            {
                LegendListBox.SetItemChecked(i, true);
                signalPlot[i].IsVisible = true;
            }
        }
    }

    private void BtnExportCsv_Click(object sender, EventArgs e)
    {
        string fileName = SaveCsvToFile();
        if (fileName == "") 
        {
            return;
        }

        // create a list of double arrays containing the Y values of all 14 signal plots
        List<double[]> yValues = new();
        for (int i = 0; i < 14; i++)
        {
            yValues.Add(signalData[i]);
        }

        // create a new CSV file and write the Y values to it
        // CultureInfo.CurrentCulture, CultureInfo.InvariantCulture, CultureInfo("de-DE")
        using StreamWriter writer = new(fileName);
        using CsvWriter csv = new(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" });
        // write header in first row
        for (int i = 0; i < plotData.Legend.Length; i++)
        {
            csv.WriteField(plotData.Legend[i].ToString());
        }
        csv.NextRecord();

        int maxR = signalPlot[0].MaxRenderIndex;
        for (int r = 0; r < maxR; r++)
        {
            for (int c = 0; c < yValues.Count; c++)
            {
                // fill Array with Y-values from SignalPlot-Array
                double[] ys = yValues.Select(y => y[r]).ToArray();
                // write to CSV-file, separated by comma
                csv.WriteField($"{ys[c]}");
            }
            csv.NextRecord();
        }
    }

    private static string SaveCsvToFile()
    {
        SaveFileDialog saveFileDialog = new();
        string fileName;

        string FilePath = AppSettings.Config.CsvFilePath;
        if (File.Exists(FilePath))
        {
            saveFileDialog.FileName = Path.GetFileName(FilePath);
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(FilePath);
            saveFileDialog.DefaultExt = Path.GetExtension(saveFileDialog.FileName.ToString());
            saveFileDialog.Filter = saveFileDialog.DefaultExt + "|*"
                + Path.GetExtension(saveFileDialog.FileName.ToString());
        }
        if (DialogResult.OK == saveFileDialog.ShowDialog())
        {
            fileName = saveFileDialog.FileName;
            AppSettings.Config.CsvFilePath = fileName;
            return fileName;
        }
        else 
        {
            return "";
        }        
    }

    private void PlotForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveWindowsPosition();
        AppSettings.Config.Setting.Save();
    }
    private static bool IsVisiblePosition(Point location, Size size)
    {
        Rectangle myArea = new(location, size);
        bool intersect = false;
        foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
        {
            intersect |= myArea.IntersectsWith(screen.WorkingArea);
        }
        return intersect;
    }
    private void RestoreWindowPosition()
    {
        // set window position
        if (IsVisiblePosition(AppSettings.Config.Form_Config_WindowLocation, AppSettings.Config.Form_Config_WindowSize))
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = AppSettings.Config.PlotForm_WindowLocation;
            this.Size = AppSettings.Config.PlotForm_WindowSize;
            WindowState = FormWindowState.Normal;
        }
        else
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
    private void SaveWindowsPosition()
    {
        // write window size to app config vars
        if (this.WindowState == FormWindowState.Normal)
        {
            AppSettings.Config.PlotForm_WindowSize = this.Size;
            AppSettings.Config.PlotForm_WindowLocation = this.Location;
        }
        else
        {
            AppSettings.Config.PlotForm_WindowSize = this.RestoreBounds.Size;
            AppSettings.Config.PlotForm_WindowLocation = this.RestoreBounds.Location;
        }
    }
}
