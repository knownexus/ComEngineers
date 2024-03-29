using System.ComponentModel.DataAnnotations;

namespace EntityFrame.API.Models
{
    public class Session
    {
        public int Id { get; set; }
        [Required]
        public DateTime TimeCode { get; set; }
        [Required]
        public virtual ICollection<HeartRate> HeartRates { get; set; }
        [Required]
        public virtual ICollection<Gyroscope> Gyroscopes { get; set; }
        [Required]
        public virtual ICollection<Accelerometer> Accelerometers { get; set; }
        [Required]
        public virtual ICollection<Temperature> Temperatures { get; set; }

    }
}
