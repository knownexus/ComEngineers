using EntityFrame.API.Data;
using EntityFrame.API.Models;

namespace EntityFrame.API.Commands
{
    public static class GyroscopeData
    {
        public static void AddData(EntityFrameContext context, List<Gyroscope> data)
        {
            foreach (var entry in data)
            {
                context.Add(entry);
            }

            context.SaveChanges();
        }

        public static List<Gyroscope?> GetDataBySessionId(EntityFrameContext con, int sessionId)
        {
            List<Gyroscope?> gyroscopes = con.Gyroscope.Where(gy => gy.Session.Id == sessionId)
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
