using az204quizmasterAPI.Services;
using az204quizmasterAPI.Models.RequestModels;
using Moq;
using az204quizmasterAPI.Data;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using az204quizmasterAPI.Models.Entities;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;


namespace Test.Services
{
    [TestClass]
    public class JsonIntakeServiceTest
    {
        JsonIntakeService service;

        [TestInitialize]
        public void Setup()
        {
            var mockData = new List<QA> {
                new(
                1,
                "question",
                "questionType",
                "link1",
                "link2",
                "img",
                []
                )
            }.AsQueryable();

            var mockSet = new Mock<DbSet<QA>>();
            mockSet.As<IQueryable<QA>>().Setup(m => m.Provider).Returns(mockData.Provider);
            mockSet.As<IQueryable<QA>>().Setup(m => m.Expression).Returns(mockData.Expression);
            mockSet.As<IQueryable<QA>>().Setup(m => m.ElementType).Returns(mockData.ElementType);
            mockSet.As<IQueryable<QA>>().Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            //mockContext.SetupGet(x => x.QAs).Returns(mockSet.Object);
            service = new(mockContext.Object);
        }

        [TestMethod]
        public void ingestJson_shouldReturnNull_whenNoErrorsPresent()
        {

            ICollection<OptionIntake> options = [
                new("LeftDisplay", null, false),
                new("LD1", null, true)
                ];

            JsonIntake jsonIntake = new(
                "TestQuestion",
                "MultipleChoiceSingle",
                null,
                null,
                null,
                options
                );

            string request = JsonSerializer.Serialize(jsonIntake);

            string? actual = service.ingestJson(request);

            Assert.IsNull(actual);
        }
    }
}