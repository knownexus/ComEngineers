using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComEngineers.API.Models
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