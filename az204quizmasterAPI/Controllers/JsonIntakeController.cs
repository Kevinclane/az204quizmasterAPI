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
        public string Post([FromBody] string jsonString)
        {
            try
            {
                string? error = _jsonIntakeService.ingestJson(jsonString);
                if (error == null)
                {
                    return "Sucessfully parsed JSON";
                }
                else
                {
                    //Add bad request header
                    return error;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}

