﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComEngineers.API.Models
{
    public class HeartRate
    {
        public int Id { get; set; }
        public DateTime TimeCode { get; set; }
        public required Session Session { get; set; }
        public int Value { get; set; }

    }
}