using EntityFrame.API.Data;
using EntityFrame.API.Models;

namespace EntityFrame.API.Commands
{
    public static class HeartRateData
    {
        public static void AddData(EntityFrameContext context, List<HeartRate> data)
        {
            foreach (var entry in data)
            {
                context.Add(entry);
            }

            context.SaveChanges();
        }
        public static List<HeartRate> GetDataBySessionId(EntityFrameContext con, int sessionId)
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
