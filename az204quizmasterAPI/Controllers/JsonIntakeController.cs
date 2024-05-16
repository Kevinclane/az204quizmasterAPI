using az204quizmasterAPI.Models.RequestModels;
using az204quizmasterAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace az204quizmasterAPI.Controllers
{
    [Route("api/json-intake")]
    [ApiController]
    public class JsonIntakeController : ControllerBase
    {

        private readonly JsonIntakeService _jsonIntakeService;
        public JsonIntakeController(JsonIntakeService jsonIntakeService)
        {
            _jsonIntakeService = jsonIntakeService;
        }

        [HttpPost]
        public string[] Post([FromBody] JsonIntake[] jsonIntakes)
        {
            try
            {
                string[] errors = _jsonIntakeService.IngestJson(jsonIntakes);
                if (errors.Length == 0)
                {
                    return ["Sucessfully ingested json"];
                }
                else
                {
                    //Add bad request header
                    return errors;
                }
            }
            catch (Exception ex)
            {
                return [ex.Message];
            }
        }

    }
}

