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
        public virtual required Session Session { get; set; }
        public float XValue { get; set; }
        public float YValue { get; set; }
        public float ZValue { get; set; }
    }
}
