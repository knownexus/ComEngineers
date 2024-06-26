﻿using EntityFrame.API.Data;
using EntityFrame.API.Models;

namespace EntityFrame.API.Commands
{
    public static class AccelerometerData
    {
        public static void AddData(EntityFrameContext con, List<Accelerometer> data)
        {
            foreach (var entry in data)
            {
                con.Add(entry);
            }
            con.SaveChanges();
        }
        public static List<Accelerometer?> GetDataBySessionId(EntityFrameContext con, int sessionId)
        {
            List<Accelerometer?> accelerometers = con.Accelerometer.Where(acc => acc.Session.Id == sessionId)
                .Select(acc => new Accelerometer
                {
                    TimeCode = acc.TimeCode,
                    XValue = acc.XValue,
                    YValue = acc.YValue,
                    ZValue = acc.ZValue,
                    Session = acc.Session
                }).ToList();
            return accelerometers;
        }
    }
}
