using Microsoft.Extensions.Configuration;
using ScratchPayModels;
using ScratchPayRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScratchPayRepository.Repository
{
    public class ClinicRepository : IClinicsRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ClinicRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// Search Clincs Data
        /// </summary>
        /// <param name="clinicName"></param>
        /// <param name="state"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        public async Task<List<Clinics>> SearchClinics(string clinicName, string state, string fromTime, string toTime)
        {
            List<Clinics> lstAllClinics = new List<Clinics>();
            List<Clinics> lstClincs = new List<Clinics>();
            try
            {

                if (string.IsNullOrEmpty(clinicName) && string.IsNullOrEmpty(state) && string.IsNullOrEmpty(fromTime) && string.IsNullOrEmpty(toTime))
                {
                    return lstAllClinics;
                }
                else
                {

                    //get Dental clinics Data
                    lstClincs = await GetDentalClincs();
                    lstAllClinics.AddRange(lstClincs);
                    lstClincs = new List<Clinics>();

                    //get Vet clinics Data
                    lstClincs = await GetVetClincs();
                    lstAllClinics.AddRange(lstClincs);
                    lstClincs = new List<Clinics>();


                    //filter data based on input parameters
                    if (!string.IsNullOrEmpty(clinicName))
                    {
                        lstAllClinics = lstAllClinics.Where(x => x.ClinicName == clinicName).ToList();
                    }
                    if (!string.IsNullOrEmpty(state))
                    {
                        lstAllClinics = lstAllClinics.Where(x => x.StateName == state).ToList();
                    }
                    if (!string.IsNullOrEmpty(fromTime) && !string.IsNullOrEmpty(toTime))
                    {
                        lstAllClinics = lstAllClinics.Where(x => x.FromTime == fromTime && x.ToTime == toTime).ToList();
                    }
                }
            }
            catch (Exception)
            {
            }
            return lstAllClinics;
        }


        /// <summary>
        /// Reading Dental clinics data
        /// </summary>
        /// <returns></returns>
        private async Task<List<Clinics>> GetDentalClincs()
        {
            List<Clinics> lstDentalClinics = new List<Clinics>();
            try
            {
                using HttpClient client = _httpClientFactory.CreateClient();
                string url = _configuration["DentalJsonUrl"].ToString();

                DentalClinics[]? dentalClinics = await client.GetFromJsonAsync<DentalClinics[]>(
                url,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));

                if (dentalClinics != null)
                {
                    foreach (var item in dentalClinics)
                    {
                        Clinics c = new Clinics();
                        c.ClinicName = item.name;
                        c.StateName = item.stateName;
                        c.FromTime = (item.availability.from);
                        c.ToTime = (item.availability.to);
                        lstDentalClinics.Add(c);
                    }
                }
            }
            catch (Exception)
            {
                lstDentalClinics = new List<Clinics>();
            }

            return lstDentalClinics;
        }

        /// <summary>
        /// Reading Vet Clinics Data
        /// </summary>
        /// <returns></returns>
        private async Task<List<Clinics>> GetVetClincs()
        {
            List<Clinics> lstVetClinics = new List<Clinics>();
            try
            {
                using HttpClient client = _httpClientFactory.CreateClient();
                string url = _configuration["VetJsonUrl"].ToString();

                VetClinics[]? dentalClinics = await client.GetFromJsonAsync<VetClinics[]>(
                url,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));

                if (dentalClinics != null)
                {
                    foreach (var item in dentalClinics)
                    {
                        Clinics c = new Clinics();
                        c.ClinicName = item.clinicName;
                        c.StateName = item.stateCode;
                        c.FromTime = (item.opening.from);
                        c.ToTime = (item.opening.to);
                        lstVetClinics.Add(c);
                    }
                }
            }
            catch (Exception)
            {
                lstVetClinics = new List<Clinics>();
            }

            return lstVetClinics;
        }
    }
}
