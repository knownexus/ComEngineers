namespace EntityFrame.API.Models
{
    public class Gyroscope
    {
        public int Id { get; set; }
        public DateTime TimeCode { get; set; }
        public virtual required Session Session { get; set; }
        public float XAxis { get; set; } // Pitch
        public float YAxis { get; set; } // Yaw
        public float ZAxis { get; set; } // Roll
    }
}