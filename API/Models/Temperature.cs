using System;
using System.ComponentModel.DataAnnotations;

namespace ComEngineers.API.Models
{
    public class Temperature
    {
        public int Id { get; set; }
        public DateTime TimeCode { get; set; }
        public required Session Session { get; set; }
        public float Value { get; set; }
    }
}