using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScratchPayModels;
using ScratchPayRepository.IRepository;

namespace ScratchPayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //we can enable Authorize if we add Azure AD authentication
    //[Authorize]
    public class ClinicsController : ControllerBase
    {

        private readonly ILogger<ClinicsController> _logger;
        private readonly IClinicsRepository _clincsRepo;

        public ClinicsController(ILogger<ClinicsController> logger, IClinicsRepository clinicsRepository)
        {
            _logger = logger;
            _clincsRepo = clinicsRepository;
        }

        /// <summary>
        /// Search clinics Data based on input parameters
        /// </summary>
        /// <param name="clinicName"></param>
        /// <param name="state"></param>
        /// <param name="fromtime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet(Name = "SearchClinics")]
        public async Task<List<Clinics>> SearchClinics(string clinicName, string state, string fromtime, string toTime)
        {
            List<Clinics> lstAllClinics = new List<Clinics>();
            try
            {
                _logger.LogInformation("SearchClinics Started");
                if (string.IsNullOrEmpty(clinicName) && string.IsNullOrEmpty(state) && string.IsNullOrEmpty(fromtime) && string.IsNullOrEmpty(toTime))
                {
                    return lstAllClinics;
                }
                else
                {
                    lstAllClinics = await _clincsRepo.SearchClinics(clinicName, state, fromtime, toTime);
                    _logger.LogInformation("SearchClinics Ended");
                    return lstAllClinics;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("SearchClinics thrown exception " + ex.Message);
                lstAllClinics = new List<Clinics>();
            }
            return lstAllClinics;
        }
    }
}