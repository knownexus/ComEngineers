using ComEngineers.API.Data;
using ComEngineers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComEngineers.API.Commands
{
    public static class HeartRateData
    {
        public static void AddData(ComEngineersContext context, List<HeartRate> data)
        {
            foreach (var entry in data)
            {
                context.Add(entry);
            }

            context.SaveChanges();
        }
        public static List<HeartRate> GetDataBySessionId(ComEngineersContext con, int sessionId)
        {
            var heartRates = con.HeartRate.Where(hr => hr.Session.Id == sessionId)
                .Select(hr => new HeartRate
                {
                    Id = hr.Id,
                    TimeCode = hr.TimeCode,
                    Session = hr.Session,
                    Value = hr.Value
                }).ToList();
            return heartRates;
        }
    }
}
