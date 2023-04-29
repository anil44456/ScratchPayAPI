using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ScratchPayAPI.Controllers;
using ScratchPayRepository.IRepository;

namespace ScratchPayAPI.Test
{
    public class ClinicsControllerTest
    {
        [Fact]
        public async void Clinics_Search_GetData()
        {
            var clinicRepo = new Mock<IClinicsRepository>();
            var mock = new Mock<ILogger<ClinicsController>>();
            ILogger<ClinicsController> logger = mock.Object;


            clinicRepo.Setup(_ => _.SearchClinics("Test", "CA", "01:00", "10:00")).ReturnsAsync(MockData.getClinicsMockData());
            var cliController = new ClinicsController(logger, clinicRepo.Object);

            var result = await cliController.SearchClinics("Test", "CA", "01:00", "10:00");

            Assert.NotNull(result);

        }

        [Fact]
        public async void Clinics_Search_GetNullData()
        {
            var clinicRepo = new Mock<IClinicsRepository>();
            var mock = new Mock<ILogger<ClinicsController>>();
            ILogger<ClinicsController> logger = mock.Object;


            clinicRepo.Setup(_ => _.SearchClinics("Test", "CA", "01:00", "10:00")).ReturnsAsync(MockData.getEmptyClinicsMockData());
            var cliController = new ClinicsController(logger, clinicRepo.Object);

            var result = await cliController.SearchClinics("Test", "CA", "01:00", "10:00");

            Assert.Empty(result);

        }
    }
}