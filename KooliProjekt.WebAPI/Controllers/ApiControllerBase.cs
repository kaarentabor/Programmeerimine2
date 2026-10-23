using KooliProjekt.Application.Infrastructure.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KooliProjekt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public abstract class ApiControllerBase : Controller
    {
        private static JsonSerializerSettings _serializerSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore                
            };

        protected IActionResult Result(OperationResult result)
        {
            var serialized = JsonConvert.SerializeObject(result, _serializerSettings);

            if (result.HasErrors)
            {
                return new ContentResult { Content = serialized, ContentType = "application/json", StatusCode = 400 };
            }

            return new ContentResult { Content = serialized, ContentType = "application/json", StatusCode = 200 };
        }

        protected IActionResult Result<T>(OperationResult<T> result)
        {
            var serialized = JsonConvert.SerializeObject(result, _serializerSettings);

            if (result.HasErrors)
            {
                return new ContentResult { Content = serialized, ContentType = "application/json", StatusCode = 400 };
            }

            if (result.Value == null)
            {
                return NotFound();
            }

            return new ContentResult { Content = serialized, ContentType = "application/json", StatusCode = 200 };
        }
    }
}
