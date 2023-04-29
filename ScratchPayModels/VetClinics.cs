using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchPayModels
{

    public class Opening
    {
        public string from { get; set; }
        public string to { get; set; }
    }

    public class VetClinics
    {
        public string clinicName { get; set; }
        public string stateCode { get; set; }
        public Opening opening { get; set; }
    }
}
