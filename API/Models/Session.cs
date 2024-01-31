using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComEngineers.API.Models
{
    public class Session
    {
        public int Id { get; set; }
        [Required]
        public DateTime TimeCode { get; set; }
        [Required]
        public  ICollection<HeartRate> HeartRates { get; set; }
        [Required]
        public  ICollection<Gyroscope> Gyroscopes { get; set; }
        [Required]
        public  ICollection<Accelerometer> Accelerometers { get; set; }
        [Required]
        public  ICollection<Temperature> Temperatures { get; set; }

    }
}
