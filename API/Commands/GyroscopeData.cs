using ComEngineers.API.Data;
using ComEngineers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComEngineers.API.Commands
{
    public static class GyroscopeData
    {
        public static void AddData(ComEngineersContext context, List<Gyroscope> data)
        {
            foreach (var entry in data)
            {
                context.Add(entry);
            }

            context.SaveChanges();
        }

        public static List<Gyroscope> GetDataBySessionId(ComEngineersContext con, int sessionId)
        {
            var gyroscopes = con.Gyroscope.Where(gy => gy.Session.Id == sessionId)
                .Select(gy => new Gyroscope
                {
                    Id = gy.Id,
                    TimeCode = gy.TimeCode,
                    Session = null,
                    XAxis = gy.XAxis,
                    YAxis = gy.YAxis,
                    ZAxis = gy.ZAxis
                }).ToList();
            return gyroscopes;
        }
    }
}
