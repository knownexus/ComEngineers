using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComEngineers.API.Data;
using ComEngineers.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ComEngineers.API.Commands
{
    public static class AccelerometerData
    {
        public static void AddData(ComEngineersContext con, List<Accelerometer> data)
        {
            foreach (var entry in data)
            {
                con.Add(entry);
            }
            con.SaveChanges();
        }
        public static void GetDataBySessionId(ComEngineersContext con, int sessionId)
        {
            var queryable = con.Accelerometer.Where(acc => acc.Session.Id == sessionId)
                .Select(acc => new
                {
                    timecode = acc.TimeCode,
                    x = acc.XValue,
                    y = acc.YValue,
                    z = acc.ZValue
                } );
           
        }
    }
}
