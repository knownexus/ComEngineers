using EntityFrame.API.Commands;
using EntityFrame.API.Data;
using EntityFrame.API.Models;
using ComEngineers.DataProcessing;
using Color = ScottPlot.Color;
using TypesOfData = EntityFrame.API.Commands.TypesOfData;

namespace ComEngineers
{
    public partial class DataDisplayForm : Form
    {
        private readonly DataInputForm _priorForm;
        private readonly string[] _dataTypes;
        private readonly EntityFrameContext _context;

        private string _labelX = "";
        private string _labelY = "";
        private string _labelName = "";
        private string? _previousMetric1 = "";
        private string? _previousMetric2 = "";
        private int _previousSession1 = -1;
        private int _previousSession2 = -1;

        public DataDisplayForm(DataInputForm priorForm, Session? selectedSession)
        {
            InitializeComponent();
            _priorForm = priorForm;
            _context = priorForm.Context;
            //SessionBox1.SelectedItem = selectedSession!.Id;
            foreach (var id in SessionData.GetSessionIds(_context))
            {
                SessionBox1.Items.Add(id);
                SessionBox2.Items.Add(id);
            }

            _dataTypes = TypesOfData.GetDataTypes(this._priorForm.Context);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _priorForm.Show();
        }

        private void ReturnToDataInputButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateChart_Click(object sender, EventArgs e)
        {
            var sessionId1 = Convert.ToInt32(SessionBox1.SelectedItem?.ToString());
            var sessionId2 = Convert.ToInt32(SessionBox2.SelectedItem?.ToString());
            var metric1Value = MetricBox1.SelectedItem?.ToString();
            var metric2Value = MetricBox2.SelectedItem?.ToString();
            UpdateGraphSizes(metric1Value);

            if (IsUnchanged(sessionId1, sessionId2, metric1Value, metric2Value))
            {
                RefreshPlots();
                return;
            }
            ResetPlots();
            RefreshPlots();

            _labelX = "";
            _labelY = "";
            _previousMetric1 = metric1Value;
            _previousMetric2 = metric2Value;
            _previousSession1 = sessionId1;
            _previousSession2 = sessionId2;

            (List<double> dataY1, List<double> dataY2, List<double> dataY3, List<double> dataX) data3D;
            (List<double> dataX, List<double> dataY) data;

            if (metric1Value is "Accelerometer" or "Gyroscope")
            {
                formsPlot2.Visible = true;
                formsPlot3.Visible = true;


                data3D = Get3dDataForMetric(MetricBox1.SelectedItem?.ToString(), sessionId1);
                formsPlot1.Plot.Add.Scatter(data3D.dataX, data3D.dataY1, Color.FromHex("#111fff"));
                formsPlot2.Plot.Add.Scatter(data3D.dataX, data3D.dataY2, Color.FromHex("#119fff"));
                formsPlot3.Plot.Add.Scatter(data3D.dataX, data3D.dataY3, Color.FromHex("#11ffff"));

                _labelY += metric1Value is "Accelerometer" ? "(Gs)" : "(degrees)";

                formsPlot1.Plot.YLabel("X " + _labelY);
                formsPlot2.Plot.YLabel("Y " + _labelY);
                formsPlot3.Plot.YLabel("Z " + _labelY);
            }
            else
            {
                formsPlot2.Visible = false;
                formsPlot3.Visible = false;
                data = GetDataForMetric(MetricBox1.SelectedItem?.ToString(), sessionId1);
                formsPlot1.Plot.Add.Scatter(data.dataX, data.dataY);
                formsPlot1.Plot.YLabel(_labelY);
            }

            if (SessionBox2.SelectedItem != null)
            {
                sessionId1 = Convert.ToInt32(SessionBox2.SelectedItem?.ToString());
                if(metric2Value is "Accelerometer" or "Gyroscope")
                {
                    formsPlot2.Visible = true;
                    formsPlot3.Visible = true;
                    data3D = Get3dDataForMetric(MetricBox1.SelectedItem?.ToString(), sessionId1);
                    formsPlot1.Plot.Add.Scatter(data3D.dataX, data3D.dataY1, Color.FromHex("#ff1111"));
                    formsPlot2.Plot.Add.Scatter(data3D.dataX, data3D.dataY2, Color.FromHex("#ff7111"));
                    formsPlot3.Plot.Add.Scatter(data3D.dataX, data3D.dataY3, Color.FromHex("#ff6699"));
                }
                else
                {
                    formsPlot2.Visible = false;
                    formsPlot3.Visible = false;
                    data = GetDataForMetric(MetricBox1.SelectedItem?.ToString(), sessionId1);
                    formsPlot1.Plot.Add.Scatter(data.dataX, data.dataY);
                }
            }

            formsPlot1.Plot.XLabel(_labelX);
            formsPlot2.Plot.XLabel(_labelX);
            formsPlot3.Plot.XLabel(_labelX);

            AutoScalePlots();
            RefreshPlots();
        }

        private bool IsUnchanged(int sessionId1, int sessionId2, string? metric1Value, string? metric2Value)
        {
            return metric1Value == _previousMetric1 &&
                   metric2Value == _previousMetric2 && 
                   sessionId1 == _previousSession1 && 
                   sessionId2 == _previousSession2;
        }

        private void ResetPlots()
        {
            formsPlot1.Plot.Clear();
            formsPlot2.Plot.Clear();
            formsPlot3.Plot.Clear();
            RefreshPlots();
        }
        private void RefreshPlots()
        {
            formsPlot1.Refresh();
            formsPlot2.Refresh();
            formsPlot3.Refresh();
        }
        private void AutoScalePlots()
        {
            formsPlot1.Plot.Axes.AutoScale();
            formsPlot2.Plot.Axes.AutoScale();
            formsPlot3.Plot.Axes.AutoScale();
        }
        private (List<double> dataY1, List<double> dataY2, List<double> dataY3, List<double> dataX) Get3dDataForMetric(
            string? item, int sessionId)
        {
            List<double> dataY1 = [];
            List<double> dataY2 = [];
            List<double> dataY3 = [];
            List<double> dataX = [];
            (List<double> xValues, List<double> yValues, List<double> zValues, List<double> timeCode) data;

            switch (item)
            {
                case "Accelerometer":
                    var aData = AccelerometerData.GetDataBySessionId(_context, sessionId);
                    data = aData.Get3dData();

                    dataY1 = data.xValues;
                    dataY2 = data.yValues;
                    dataY3 = data.zValues;
                    dataX = data.timeCode;
                    _labelX = "Time";
                    //generate3DData(aData, sessionId);
                    break;
                case "Gyroscope":
                    var gData = GyroscopeData.GetDataBySessionId(_context, sessionId);

                    data = gData.Get3dData();

                    dataY1 = data.xValues;
                    dataY2 = data.yValues;
                    dataY3 = data.zValues;
                    dataX = data.timeCode;
                    _labelX = "Time";
                    //generate3DData(gData, sessionId);
                    break;
            }

            return (dataY1, dataY2, dataY3, dataX);
        }

        private void UpdateGraphSizes(string? metric)
        {
            if (metric is "Accelerometer" or "Gyroscope")
            {
                formsPlot1.Size = formsPlot1.Size with { Height = (int)(this.Size.Height * 0.3) };
                formsPlot2.Size = formsPlot1.Size;
                formsPlot2.Location = formsPlot1.Location with
                {
                    Y = formsPlot1.Location.Y + (formsPlot1.Size.Height * 1)
                };
                formsPlot3.Size = formsPlot1.Size;
                formsPlot3.Location = formsPlot1.Location with
                {
                    Y = formsPlot1.Location.Y + (formsPlot1.Size.Height * 2)
                };
            }
            else
                formsPlot1.Size = formsPlot1.Size with { Height = (int)(this.Size.Height * 0.9) };
        }
        private (List<double> dataX, List<double> dataY) GetDataForMetric(string? item, int sessionId)
        {
            var dataX = new List<double>();
            var dataY = new List<double>();
            switch (item)
            {
                case "HeartRate":
                    _labelName = "Heart Rate (BPM)";
                    var bpmData = DataDisplayProcessing.GetBpmData(sessionId, _context);
                    dataX = bpmData.dataX;
                    dataY = bpmData.dataY;
                    _labelX = "Time (s)";
                    if (_labelY != "" && _labelY != _labelName)
                    {
                        _labelY += " and ";
                    }
                    else
                    {
                        _labelY = "";
                    }

                    _labelY += _labelName;

                    break;
                case "Temperature":
                    _labelName = "Temperature (c)";
                    var (dataForX, dataForY) = DataDisplayProcessing.ProcessTemperatureData(sessionId, _context);
                    dataX.AddRange(dataForX);
                    dataY.AddRange(dataForY);

                    _labelX = "Time (s)";
                    if (_labelY != "" && _labelY != _labelName)
                    {
                        _labelY += " and ";
                    }
                    else
                    {
                        _labelY = "";
                    }

                    _labelY += _labelName;
                    break;
            }

            return (dataX, dataY);
        }

        private void DataDisplayForm_Load(object sender, EventArgs e)
        {
        }

        private void SessionBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MetricBox1.Items.Count == 0)
                foreach (var type in _dataTypes)
                {
                    MetricBox1.Items.Add(type);
                }

            MetricBox1.Visible = true;
        }

        private void SessionBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MetricBox2.Items.Count == 0)
                foreach (var type in _dataTypes)
                {
                    MetricBox2.Items.Add(type);
                }

            MetricBox2.Visible = true;
        }

        private void MetricBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            MetricBox2.SelectedItem = MetricBox1.SelectedItem;
        }

        private void MetricBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            SessionBox1.SelectedItem = null;
            SessionBox1.Text = @"Session ID";
            MetricBox1.SelectedItem = null;
            MetricBox1.Text = @"Metric";
        }

        private void ClearButton2_Click(object sender, EventArgs e)
        {
            SessionBox2.SelectedItem = null;
            SessionBox2.Text = @"Session ID";
            MetricBox2.SelectedItem = null;
            MetricBox2.Text = @"Metric";
        }
    }
}