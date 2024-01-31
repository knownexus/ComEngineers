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
        public required Session Session { get; set; }
        public int XAxis { get; set; } // Pitch
        public int YAxis { get; set; } // Yaw
        public int ZAxis { get; set; } // Roll
    }
}