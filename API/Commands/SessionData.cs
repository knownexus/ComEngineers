﻿using EntityFrame.API.Data;
using EntityFrame.API.Models;

namespace EntityFrame.API.Commands
{
    public static class SessionData
    {
        public static void AddData(EntityFrameContext context, List<Session> data)
        {
            foreach (var entry in data)
            {
                context.Add(entry);
            }

            context.SaveChanges();
        }

        public static int GetSessionId(EntityFrameContext con, int sessionId)
        {
            var session = con.Session.Where(x => x.Id == sessionId)
                .Select(x => x.Id).FirstOrDefault();

            return sessionId;
        }

        public static int[] GetSessionIds(EntityFrameContext con)
        {
            var sessionIdList = con.Session.Select(x => x.Id).ToArray();

            return sessionIdList;
        }

        public static Session GetLatestSession(EntityFrameContext con)
        {
            var sessionId = con.Session.OrderBy(x => x.Id).Select(x => x.Id).LastOrDefault();
            var sessionTimeCode = con.Session.OrderBy(x => x.Id).Select(x => x.TimeCode).LastOrDefault();


            var accelerometers = con.Accelerometer
                .Where(x => x.Session.Id == sessionId)
                .Select(x => new Accelerometer
                {
                    TimeCode = x.TimeCode,
                    XValue = x.XValue,
                    YValue = x.YValue,
                    ZValue = x.ZValue,
                    Session = x.Session,
                    Id = x.Id
                }).ToList();
            var gyroscopes = con.Gyroscope.Where(x => x.Session.Id == sessionId)
                .Select(x => new Gyroscope
                {
                    TimeCode = x.TimeCode,
                    XAxis = x.XAxis,
                    YAxis = x.YAxis,
                    ZAxis = x.ZAxis,
                    Session = x.Session,
                    Id = x.Id
                }).ToList();
            var heartRates = con.HeartRate.Where(x => x.Session.Id == sessionId)
                .Select(x => new HeartRate
                {
                    TimeCode = x.TimeCode,
                    Value = x.Value,
                    Session = x.Session,
                    Id = x.Id
                }).ToList();
            var temperatures = con.Temperature.Where(x => x.Session.Id == sessionId)
                .Select(x => new Temperature
                {
                    TimeCode = x.TimeCode,
                    Value = x.Value,
                    Session = x.Session,
                    Id = x.Id
                }).ToList();

            var session = new Session()
            {
                Id = sessionId,
                Accelerometers = accelerometers,
                HeartRates = heartRates,
                Gyroscopes = gyroscopes,
                Temperatures = temperatures,
                TimeCode = sessionTimeCode
            };

            //   var session = con.Session.Where(x => x.Id == sessionId)
            //       .Select(x => x).OrderBy(x => x.TimeCode).Last();

            return session;
        }

        public static Session? GetSessionById(EntityFrameContext con, int sessionId)
        {
            var session = con.Session.FirstOrDefault(x => x.Id == sessionId);

            return session;
        }
    }
}
