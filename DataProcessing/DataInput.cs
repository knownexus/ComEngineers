using ComEngineers.API.Commands;
using ComEngineers.API.Data;
using ComEngineers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ComEngineers.DataProcessing
{
    static class DataInput
    {
        public static (List<Accelerometer> accelerometerEntries, List<Gyroscope> gyroscopeEntries, List<HeartRate> heartRateEntries, List<Temperature> temperatureEntries) ProcessData(string content, Session session)
        {
            var lines = content.Split(",,,,,,");
            lines = lines.Skip(1).SkipLast(1).ToArray();
            //var data = new List<List<object>>();
            var accelerometerEntries = new List<Accelerometer>();
            var gyroscopeEntries = new List<Gyroscope>();
            var heartRateEntries = new List<HeartRate>();
            var temperatureEntries = new List<Temperature>();

            foreach (var line in lines) // 50ms
            {
                // Data  = PulseData*100, ax, ay, az, yaw, pitch, roll, temperature
                var values = line.Split(",");

                accelerometerEntries.Add(new Accelerometer
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    XValue = float.Parse(values[1]),
                    YValue = float.Parse(values[2]),
                    ZValue = float.Parse(values[3])
                });
                gyroscopeEntries.Add(new Gyroscope
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    YAxis = float.Parse(values[4]),
                    XAxis = float.Parse(values[5]),
                    ZAxis = float.Parse(values[6])
                });
                heartRateEntries.Add(new HeartRate
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    Value = float.Parse(values[0])
                });
                temperatureEntries.Add(new Temperature
                {
                    Session = session,
                    TimeCode = DateTime.Now,
                    Value = float.Parse(values[7])
                });
            }

            return (accelerometerEntries, gyroscopeEntries, heartRateEntries, temperatureEntries);
        }
        public static void AddProcessedData(ComEngineersContext context,List<Session> sessionEntries, (List<Accelerometer> accelerometerEntries, List<Gyroscope> gyroscopeEntries, List<HeartRate> heartRateEntries, List<Temperature> temperatureEntries) data)
        {
            SessionData.AddData(context, sessionEntries);
            AccelerometerData.AddData(context, data.accelerometerEntries);
            GyroscopeData.AddData(context, data.gyroscopeEntries);
            HeartRateData.AddData(context, data.heartRateEntries);
            TemperatureData.AddData(context, data.temperatureEntries);
        }
    }
}
