using az204quizmasterAPI.Services;
using az204quizmasterAPI.Models.RequestModels;
using az204quizmasterAPI.Data;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;


namespace Test.Services
{
    [TestClass]
    public class JsonIntakeServiceTest
    {
        private JsonIntakeService service;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            var context = new DataContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            service = new JsonIntakeService(context);
        }

        [TestMethod]
        public void IngestJson_ShouldReturnNull_WhenMultipleChoiceSingleIsValid()
        {

            ICollection<OptionIntake> options = [
                new("LeftDisplay", null, false),
                new("LD1", null, true)
                ];

            JsonIntake request = new(
                "TestQuestion",
                "MultipleChoiceSingle",
                null,
                null,
                null,
                options
                );

            string? actual = service.IngestJson(request);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void IngestJson_ShouldReturnError_WhenMultipleChoiceSingleIsInvalid()
        {
            ICollection<OptionIntake> options = [
                new("LeftDisplay", null, false),
                new("LD1", null, false)
                ];

            JsonIntake request= new(
                "TestQuestion",
                "MultipleChoiceSingle",
                null,
                null,
                null,
                options
                );

            string? actual = service.IngestJson(request);

            Assert.AreEqual("Question [TestQuestion] is missing a correct answer.", actual);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void IngestJson_ShouldReturnNull_WhenMultipleChoiceMultipleIsValid(int correctOptionCount)
        {
            ICollection<OptionIntake> options = [
                new("LeftDisplay", null, false)
            ];

            for (int i = 0; i < correctOptionCount; i++)
            {
                options.Add(new("LD" + i, null, true));
            }

            JsonIntake request = new(
                "TestQuestion",
                "MultipleChoiceMultiple",
                null,
                null,
                null,
                options
                );

            string? actual = service.IngestJson(request);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void IntakeJson_ShouldReturnError_WhenMultipleChoiceMultipleIsInvalid()
        {
            ICollection<OptionIntake> options = [
                new("LeftDisplay", null, false),
                new("LD1", null, false)
            ];

            JsonIntake request = new(
                "TestQuestion",
                "MultipleChoiceMultiple",
                null,
                null,
                null,
                options
            );

            string? actual = service.IngestJson(request);

            Assert.AreEqual("Question [TestQuestion] must have at least one answer.", actual);
        }

        [TestMethod]
        public void IntakeJson_ShouldReturnError_WhenLessThan2OptionsProvided()
        {
            JsonIntake request = new(
                "TestQuestion",
                "MultipleChoiceMultiple",
                null,
                null,
                null,
                []
            );

            string? actual = service.IngestJson(request);

            Assert.AreEqual("Question [TestQuestion] must have at least 2 options.", actual);
        }

        [TestMethod]
        public void IntakeJson_ShouldReturnError_WhenMatchIsInvalid()
        {
            ICollection<OptionIntake> options = [
                new("leftDisplay", null, false),
                new("LD1", "RD1", false)
            ];

            JsonIntake request = new(
                "TestQuestion",
                "Match",
                null,
                null,
                null,
                options
            );

            string? actual = service.IngestJson(request);

            Assert.AreEqual("Question [TestQuestion] has one or more missing display options.", actual);
        }

        [TestMethod]
        public void IntakeJson_ShouldReturnError_WhenInvalidQuestionType()
        {
            ICollection<OptionIntake> options = [
                new("leftDisplay", null, false),
                new("LD1", "RD1", false)
            ];

            JsonIntake request = new(
                "TestQuestion",
                "InvalidType",
                null,
                null,
                null,
                options
           );

            string? actual = service.IngestJson(request);

            Assert.AreEqual("Invalid QuestionType", actual);
        }
    }
}