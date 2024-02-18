using ScottPlot;
using ScottPlot.Plottable;
using VirusSpreadLibrary.Plott;
using VirusSpreadLibrary.AppProperties;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Drawing;

namespace Virus3spread.Forms;

public partial class PhaseChartForm : Form
{
    private readonly PlotData plotData;

    readonly FormsPlot formsPlot;

    private readonly ScatterPlot scatterPlot;

    private readonly double[][] scatterData = new double[2][];

    private int nextDataIndex = 0;

    private readonly Crosshair crosshair;

    private readonly bool listBoxChangeByCodeValueX = false;
    private readonly bool listBoxChangeByCodeValueY = false;

    public PhaseChartForm(PlotData plotData)
    {
        InitializeComponent();
        this.plotData = plotData;

        listBoxChangeByCodeValueX = true;
        listBoxChangeByCodeValueY = true;
        XvalueListBox.SelectionMode = SelectionMode.One;
        YvalueListBox.SelectionMode = SelectionMode.One;
        XvalueListBox.Items.AddRange(plotData.Legend);
        XvalueListBox.SelectedIndex = AppSettings.Config.PhaseChartXSelectedIndex;
        YvalueListBox.Items.AddRange(plotData.Legend);
        YvalueListBox.SelectedIndex = AppSettings.Config.PhaseChartYSelectedIndex;
        listBoxChangeByCodeValueX = false;
        listBoxChangeByCodeValueY = false;


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

        formsPlot.Plot.Palette = ScottPlot.Palette.Category20;

        // create empty data x y arrays with length of MaxIterations add it to the plot 
        for (int i = 0; i < 2; i++)
        {
            scatterData[i] = new double[AppSettings.Config.MaxIterations];
        }
        scatterPlot = formsPlot.Plot.AddScatter(scatterData[0], scatterData[1], formsPlot.Plot.Palette.GetColor(0));
        BtnHoldStart.BackColor = SystemColors.ControlLightLight;
    }
    private void XvalueListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppSettings.Config.PhaseChartXSelectedIndex = XvalueListBox.SelectedIndex;
        if (listBoxChangeByCodeValueX != true) 
        {           
            Array.Clear(scatterData[0], 0, scatterData[0].Length);
            Array.Clear(scatterData[1], 0, scatterData[1].Length);
            nextDataIndex = 0;
            plotData.ClearPhaseChartQueue();            
        }
    }
    private void YvalueListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppSettings.Config.PhaseChartYSelectedIndex = YvalueListBox.SelectedIndex;
        if (listBoxChangeByCodeValueY != true)
        {
            Array.Clear(scatterData[0], 0, scatterData[0].Length);
            Array.Clear(scatterData[1], 0, scatterData[1].Length);
            nextDataIndex = 0;
            plotData.ClearPhaseChartQueue();
        }
    }
    private void DataTimer_Tick(object sender, EventArgs e)
    {
        if (nextDataIndex >= scatterData[0].Length)
        {
            DataTimer?.Stop();
            RenderTimer?.Stop();
            throw new OverflowException("PlotForm data array isn't long enough to accomodate new data");
        }

        // adjusted the de-queue n value below and the eimer in this form 
        // to make shure, all values can be dequed in time by the PhaseChartForm
        for (int n = 0; n < 2; n++)
        {
            bool success = plotData.PlotPhaseChartDataQueue.TryDequeueList(out List<double> values);
            if (success)
            {
                scatterPlot.MaxRenderIndex = nextDataIndex;
                for (int i = 0; i < scatterData.Length; i++)
                {
                    scatterData[i][nextDataIndex] = values[i];                  
                }
                nextDataIndex += 1;
            }
            else
            {
                break;
            }
        }
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

    private void PhaseChartForm_Load(object sender, EventArgs e)
    {
        RestoreWindowPosition();
    }

    private void PhaseChartForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        //plotData.StopPhaseChartQueue = true;
        SaveWindowsPosition();
        AppSettings.Config.Setting.Save();
    }

    private void BtnAutoScaleTight_Click(object sender, EventArgs e)
    {
        formsPlot.Plot.Margins(0, 0);
        formsPlot.Refresh();
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
    private void BtnExportCsv_Click(object sender, EventArgs e)
    {
        string fileName = SaveCsvToFile();
        if (fileName == "")
        {
            return;
        }

        // create a list of double arrays containing the Y values of all 14 signal plots
        List<double[]> yValues = new ();
        for (int i = 0; i < 2; i++)
        {
            yValues.Add(scatterData[i]);
        }

        // create a new CSV file and write the Y values to it
        // CultureInfo.CurrentCulture, CultureInfo.InvariantCulture, CultureInfo("de-DE")
        using StreamWriter writer = new(fileName);
        using CsvWriter csv = new(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" });
        // write header in first row
        csv.WriteField(XvalueListBox.Text);
        csv.WriteField(YvalueListBox.Text);
        csv.NextRecord();


        int maxR = 0;
        if (scatterPlot.MaxRenderIndex != 0)
        {
            maxR = (int)scatterPlot.MaxRenderIndex!;
        }

        for (int r = 0; r < maxR; r++)
        {
            // fill Array with Y-values from SignalPlot-Array
            double[] ys = yValues.Select(y => y[r]).ToArray();
            // write to CSV-file, separated by comma
            csv.WriteField($"{ys[0]}");
            csv.WriteField($"{ys[1]}");
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
            this.Location = AppSettings.Config.PhaseChartForm_WindowLocation;
            this.Size = AppSettings.Config.PhaseChartForm_WindowSize;
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
            AppSettings.Config.PhaseChartForm_WindowSize = this.Size;
            AppSettings.Config.PhaseChartForm_WindowLocation = this.Location;
        }
        else
        {
            AppSettings.Config.PhaseChartForm_WindowSize = this.RestoreBounds.Size;
            AppSettings.Config.PhaseChartForm_WindowLocation = this.RestoreBounds.Location;
        }
    }
}
