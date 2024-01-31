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
            foreach (var line in lines)
            {
                // Data  = PulseData*100, ax, ay, az, yaw, pitch, roll, temperature
                var values = line.Split(",");
                var session = new Session()
                {
                    TimeCode = DateTime.Now,
                };
                var accelerometer = new Accelerometer()
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    XValue = int.Parse(values[1]),
                    YValue = int.Parse(values[2]),
                    ZValue = int.Parse(values[3])
                };
                var gyroscope = new Gyroscope()
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    YAxis = int.Parse(values[4]),
                    XAxis = int.Parse(values[5]),
                    ZAxis = int.Parse(values[6])
                };
                var heartRate = new HeartRate()
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    Value = int.Parse(values[0])
                };
                var temperature = new Temperature()
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    Value = int.Parse(values[7])
                };
                _context.Add(session);
                _context.Add(accelerometer);
                _context.Add(gyroscope);
                _context.Add(heartRate);
                _context.Add(temperature);
                _context.SaveChanges();
            }
        }
    }
}
