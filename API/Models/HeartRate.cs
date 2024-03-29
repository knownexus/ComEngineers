namespace EntityFrame.API.Models
{
    public class HeartRate
    {
        public int Id { get; set; }
        public DateTime TimeCode { get; set; }
        public virtual required Session Session { get; set; }
        public float Value { get; set; }

    }
}
