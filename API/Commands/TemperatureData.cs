using EntityFrame.API.Data;
using EntityFrame.API.Models;

namespace EntityFrame.API.Commands
{
    public static class TemperatureData
    {
        public static void AddData(EntityFrameContext context, List<Temperature> data)
        {
            foreach (var entry in data)
            {
                context.Add(entry);
            }

            context.SaveChanges();
        }
        public static List<Temperature> GetDataBySessionId(EntityFrameContext con, int sessionId)
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
