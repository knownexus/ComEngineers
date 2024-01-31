using ComEngineers.API.Data;
using ComEngineers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComEngineers.API.Commands
{
    public static class SessionData
    {
        public static void AddData(ComEngineersContext context, List<Session> data)
        {
            foreach (var entry in data)
            {
                context.Add(entry);
            }

            context.SaveChanges();
        }

        public static Session GetSession(ComEngineersContext con, int sessionId)
        {
            // var queryable = con.Session.Where(x => x.Id == sessionId)
            //                            .Select(x => new
            //                                                 {
            //                                                     accelerometers = x.Accelerometers,
            //                                                     gyros = x.Gyroscopes,
            //                                                     hrs = x.HeartRates,
            //                                                     temps = x.Temperatures
            //                                                 });

            var session = con.Session.Where(x => x.Id == sessionId)
                .Select(x => x).First();

            return session;
        }
    }
}
