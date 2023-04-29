using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchPayModels
{
    public class Availability
    {
        public string from { get; set; }
        public string to { get; set; }
    }

    public class DentalClinics
    {
        public string name { get; set; }
        public string stateName { get; set; }
        public Availability availability { get; set; }
    }
}
