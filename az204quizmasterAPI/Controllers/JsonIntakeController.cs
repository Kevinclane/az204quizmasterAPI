using az204quizmasterAPI.Models.RequestModels;
using az204quizmasterAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace az204quizmasterAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class JsonIntakeController : ControllerBase
    {

        private readonly JsonIntakeService _jsonIntakeService;
        public JsonIntakeController(JsonIntakeService jsonIntakeService)
        {
            _jsonIntakeService = jsonIntakeService;
        }

        [HttpPost]
        public List<string> Post([FromBody] JsonIntake[] jsonIntakes)
        {
            try
            {
                List<string> errors = _jsonIntakeService.IngestJson(jsonIntakes);
                if (errors.Count == 0)
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

