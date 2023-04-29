using ScratchPayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchPayAPI.Test
{
    public class MockData
    {
        public static List<Clinics> getClinicsMockData()
        {
            List<Clinics> clinics = new List<Clinics>();
            Clinics c = new Clinics() { ClinicName = "Test", StateName = "CA", ClinicType = "Dental", FromTime = "01:00", ToTime = "10:00" };
            clinics.Add(c);

            return clinics;
        }

        public static List<Clinics> getEmptyClinicsMockData()
        {
            List<Clinics> clinics = new List<Clinics>();
            return clinics;
        }
    }
}
