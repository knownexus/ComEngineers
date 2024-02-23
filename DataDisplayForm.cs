using ComEngineers.API.Commands;
using ComEngineers.API.Data;
using ComEngineers.API.Models;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OpenTK.Graphics.OpenGL.GL;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Color = ScottPlot.Color;
using TypesOfData = ComEngineers.API.Commands.TypesOfData;

namespace ComEngineers
{
    public partial class DataDisplayForm : Form
    {
        private readonly DataInputForm _priorForm;
        private readonly string[] _dataTypes;
        private readonly ComEngineersContext _Context;

        string _labelX = "";
        string _labelY = "";
        string labelName = "";
        public DataDisplayForm(DataInputForm priorForm)
        {
            InitializeComponent();
            _priorForm = priorForm;
            _Context = priorForm.Context;
            foreach (var id in SessionData.GetSessionIds(_Context))
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

        private void formsPlot1_Load(object sender, EventArgs e)
        {
        }

        private void UpdateChart_Click(object sender, EventArgs e)
        {

            var sessionId = Convert.ToInt32(SessionBox1.SelectedItem?.ToString());
            formsPlot1.Plot.Clear();
            formsPlot1.Refresh();
            _labelX = "";
            _labelY = "";
            var data = GetDataForMetric(MetricBox1.SelectedItem?.ToString(), sessionId);
            formsPlot1.Plot.Add.Scatter(data.dataX, data.dataY);
            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();

            if (SessionBox2.SelectedItem != null)
            {
                sessionId = Convert.ToInt32(SessionBox2.SelectedItem?.ToString());
                data = GetDataForMetric(MetricBox2.SelectedItem?.ToString(), sessionId);
                formsPlot1.Plot.Add.Scatter(data.dataX, data.dataY);
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Refresh();
            }

            formsPlot1.Plot.XLabel(_labelX);
            formsPlot1.Plot.YLabel(_labelY);

        }

        private (List<double> dataX, List<double> dataY) GetDataForMetric(string? item, int sessionId)
        {
            List<double> dataX = new List<double>();
            List<double> dataY = new List<double>();
            int firstId;
            float firstValue;
            switch (item)
            {
                case "Accelerometer":
                    var aData = AccelerometerData.GetDataBySessionId(_Context, sessionId);
                    aData.
                    //generate3DData(aData, sessionId);
                    break;
                case "Gyroscope":
                    var gData = GyroscopeData.GetDataBySessionId(_Context, sessionId);
                    //generate3DData(gData, sessionId);
                    break;
                case "HeartRate":
                    labelName = "Heart Rate (BPM)";
                    var cleanedHeartRateData = CleanHeartRateData(sessionId);
                    var bpmData = GetBpmData(cleanedHeartRateData);
                    dataX = bpmData.dataX;
                    dataY = bpmData.dataY;
                    _labelX = "Time (s)";
                    if (_labelY != "" && _labelY != labelName)
                    {
                        _labelY += " and ";
                    }
                    else
                    {
                        _labelY = "";
                    }
                    _labelY += labelName;

                    break;
                case "Temperature":
                    labelName = "Temperature (c)";
                    firstId = TemperatureData.GetDataBySessionId(_Context, sessionId).First().Id;
                    firstValue = TemperatureData.GetDataBySessionId(_Context, sessionId).First().Value;
                    int count = 0;
                    List<double> temperatureList = new List<double>();
                    foreach (var entry in TemperatureData.GetDataBySessionId(_Context, sessionId))
                    {
                        if (temperatureList.Count < 10)
                        {
                            temperatureList.Add(entry.Value);
                            continue;
                        }

                        var avg = temperatureList.Average();
                        dataX.Add(++count);
                        dataY.Add(avg);
                        temperatureList.Clear();
                    }
                   
                    _labelX = "Time (s)";
                    if (_labelY != "" && _labelY != labelName)
                    {
                        _labelY += " and ";
                    }
                    else
                    {
                        _labelY = "";
                    }
                    _labelY += labelName;
                    break;
            }
            return (dataX, dataY);
        }

        private static (List<double> dataX, List<double> dataY) GetBpmData(List<double> cleanedHeartRateData)
        {
            var dataY = new List<double>();
            var dataX = new List<double>();
            var samples = new List<double>();
            var bpmData = new List<double>();
            double bpmCounter = 0;
            double count = 0;
            foreach (var value in cleanedHeartRateData)
            {
                const double sampleRate = 50;
                if (samples.Count < sampleRate)
                {
                    samples.Add(value);
                    continue;
                }

                foreach (var entry in samples.Where(entry => entry > 0))
                {
                    bpmCounter++;
                }

                var multiplierToMinute = 600 / (sampleRate + count++);

                var bpm = bpmCounter * multiplierToMinute;
                bpmData.Add(bpm);

                bpmCounter = 0;
                samples.Add(value);

                if (count % 10 == 0)
                {
                    var lastTen = bpmData.Skip(Math.Max(0, bpmData.Count() - 10));
                    var avgValue = lastTen.Average();
                    dataY.Add(avgValue);
                    dataX.Add(count / 10);
                }
            }

            return (dataX, dataY);
        }

        private List<double> CleanHeartRateData(int sessionId)
        {
            var firstValue = HeartRateData.GetDataBySessionId(_Context, sessionId).First().Value;
            var data = HeartRateData.GetDataBySessionId(_Context, sessionId);
            var yValues = new List<double>();
            float previousValue = 0;

            foreach (var entry in data)
            {
                // Remove magic number
                var cleanedValues = (entry.Value / 70) - 1;
                // Remove this when clean data available
                if ((firstValue / 70) > 1.5)
                    cleanedValues -= 1;

                if (cleanedValues < 0.1)
                    cleanedValues = 0;

                if (previousValue > 0)
                    yValues.Add(0);
                else
                    yValues.Add(cleanedValues);

                previousValue = cleanedValues;
            }
            return yValues;
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
            SessionBox1.Text = "Session ID";
            MetricBox1.SelectedItem = null;
            MetricBox1.Text = "Metric";
        }

        private void ClearButton2_Click(object sender, EventArgs e)
        {
            SessionBox2.SelectedItem = null;
            SessionBox2.Text = "Session ID";
            MetricBox2.SelectedItem = null;
            MetricBox2.Text = "Metric";
        }
    }
}
