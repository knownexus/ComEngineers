﻿using ComEngineers.API.Commands;
using ComEngineers.API.Data;
using System.Reflection;
using ComEngineers.API.Models;

namespace ComEngineers.DataProcessing
{
    internal static class DataDisplayProcessing
    {
        private static List<double> CleanHeartRateData(int sessionId, ComEngineersContext context)
        {
            var firstValue = HeartRateData.GetDataBySessionId(context, sessionId).First().Value;
            var data = HeartRateData.GetDataBySessionId(context, sessionId);
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

        public static (List<double> dataX, List<double> dataY) GetBpmData(int sessionId, ComEngineersContext context)
        {
            var dataY = new List<double>();
            var dataX = new List<double>();
            var samples = new List<double>();
            var bpmData = new List<double>();
            double bpmCounter = 0;
            double count = 0;
            var cleanedHeartRateData = DataDisplayProcessing.CleanHeartRateData(sessionId, context);

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

        public static (List<double> dataForX, List<double> dataForY) TemperatureData(int sessionId, ComEngineersContext context)
        {
            int firstId;
            float firstValue;
            firstId = API.Commands.TemperatureData.GetDataBySessionId(context, sessionId).First().Id;
            firstValue = API.Commands.TemperatureData.GetDataBySessionId(context, sessionId).First().Value;
            // Use these to normalise to 0 to see fluctuations

            var count = 0;
            var temperatureList = new List<double>();
            List<double> dataForX = [];
            List<double> dataForY = [];

            foreach (var entry in API.Commands.TemperatureData.GetDataBySessionId(context, sessionId))
            {
                if (temperatureList.Count < 10)
                {
                    temperatureList.Add(entry.Value);
                    continue;
                }

                var avg = temperatureList.Average();
                dataForX.Add(++count);
                dataForY.Add(avg);
                temperatureList.Clear();
            }

            return (dataForX, dataForY);
        }

        public static (List<double> xValues, List<double> yValues, List<double> zValues, List<double> timeCode) Get3dData<T>(this List<T?> data)
        {
            List<double> xValues = [];
            List<double> yValues = [];
            List<double> zValues = [];
            List<double> timeCode = [];
            double counter = 0;
            var target = typeof(T);
                
            foreach (var entry in data)
            {
                if(target.Name == "Accelerometer")
                {
                    var castEntry = entry.Cast<Accelerometer>();
                    xValues.Add(Math.Round(castEntry.XValue,1));
                    yValues.Add(Math.Round(castEntry.YValue,1));
                    zValues.Add(Math.Round(castEntry.ZValue,1));
                }
                else
                {
                    var castEntry = entry.Cast<Gyroscope>();
                    xValues.Add(Math.Round(castEntry.XAxis, 1));
                    yValues.Add(Math.Round(castEntry.YAxis, 1));
                    zValues.Add(Math.Round(castEntry.ZAxis, 1));
                }

                timeCode.Add(counter++);
            }
            return (xValues,yValues,zValues,timeCode);
        }
        public static T Cast<T>(this object? myObj)
        {
            var target = typeof(T);
            var objInstance = Activator.CreateInstance(target, false);
            var memberInfoEnumerable = (from source in target.GetMembers().ToList()
                where source.MemberType == MemberTypes.Property
                select source);
            var memberInfos = memberInfoEnumerable as MemberInfo[] ?? memberInfoEnumerable.ToArray();
            var members = memberInfos.Where(memberInfo => memberInfos.Select(c => c.Name)
                .ToList().Contains(memberInfo.Name)).ToList();
            foreach (var memberInfo in members)
            {
                var propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                var value = myObj?.GetType().GetProperty(memberInfo.Name)?.GetValue(myObj, null);

                propertyInfo?.SetValue(objInstance, value, null);
            }
            return (T)objInstance!;
        }
    }
}
