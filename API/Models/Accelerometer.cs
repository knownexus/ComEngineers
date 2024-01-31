using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComEngineers.API.Models
{
    public class Accelerometer
    {
        public int Id { get; set; }
        public DateTime TimeCode { get; set; }
        public required Session Session { get; set; }
        public int XValue { get; set; }
        public int YValue { get; set; }
        public int ZValue { get; set; }
    }
}
