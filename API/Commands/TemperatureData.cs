using ComEngineers.API.Data;
using ComEngineers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComEngineers.API.Commands
{
    public static class TemperatureData
    {
        public static void AddData(ComEngineersContext context, List<Temperature> data)
        {
            foreach (var entry in data)
            {
                context.Add(entry);
            }

            context.SaveChanges();
        }
        public static List<Temperature> GetDataBySessionId(ComEngineersContext con, int sessionId)
        {
            var temperatures = con.Temperature.Where(tmp => tmp.Session.Id == sessionId)
                .Select(tmp => new Temperature
                {
                    Id = tmp.Id,
                    TimeCode = tmp.TimeCode,
                    Session = tmp.Session,
                    Value = tmp.Value
                }).ToList();
            return temperatures;
        }
    }
}
