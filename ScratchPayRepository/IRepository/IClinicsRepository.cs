using ScratchPayModels;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace ScratchPayRepository.IRepository
{
    public interface IClinicsRepository
    {

        Task<List<Clinics>> SearchClinics(string clinicName, string state, string fromTime, string toTime);
    }
}
