using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComEngineers.API.Commands;
using ComEngineers.API.Data;
using ComEngineers.API.Models;


namespace ComEngineers
{
    public partial class Form1 : Form
    {
        ComEngineersContext _context;

        public Form1(ComEngineersContext context)
        {
            InitializeComponent();
            _context = context;
        }


        private void Upload_Button_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "CSV Files | *.csv"; // file types, that will be allowed to upload
            dialog.Multiselect = false;
            if (dialog.ShowDialog() != DialogResult.OK) return;

            var path = dialog.FileName;
            using var reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding());

            var content = reader.ReadToEnd();
            ParseData(content);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ParseData(string content)
        {
            var lines = content.Split(",,,,,,\n", StringSplitOptions.None);
            lines = lines.Skip(1).SkipLast(1).ToArray();
            var SessionEntries = new List<Session>();
            var accelerometerEntries = new List<Accelerometer>();
            var gyroscopeEntries = new List<Gyroscope>();
            var heartRateEntries = new List<HeartRate>();
            var temperatureEntries = new List<Temperature>();
            var session = new Session()
            {
                TimeCode = DateTime.Now,
            };
            SessionEntries.Add(session);
            foreach (var line in lines)
            {
                // Data  = PulseData*100, ax, ay, az, yaw, pitch, roll, temperature
                var values = line.Split(",");

                accelerometerEntries.Add(new Accelerometer()
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    XValue = float.Parse(values[1]),
                    YValue = float.Parse(values[2]),
                    ZValue = float.Parse(values[3])
                });
                gyroscopeEntries.Add(new Gyroscope()
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    YAxis = float.Parse(values[4]),
                    XAxis = float.Parse(values[5]),
                    ZAxis = float.Parse(values[6])
                });
                heartRateEntries.Add(new HeartRate()
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    Value = float.Parse(values[0])
                });
                temperatureEntries.Add(new Temperature()
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    Value = float.Parse(values[7])
                });
            }

            var confirmResult = MessageBox.Show("Upload Complete!\nDo you wish you store this data?",
                "Data Storage Confirmation",
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                SessionData.AddData(_context,SessionEntries);
                AccelerometerData.AddData(_context, accelerometerEntries);
                GyroscopeData.AddData(_context, gyroscopeEntries);
                HeartRateData.AddData(_context,heartRateEntries);
                TemperatureData.AddData(_context,temperatureEntries);
            }
            else
            {
            }
        }
    }
}
