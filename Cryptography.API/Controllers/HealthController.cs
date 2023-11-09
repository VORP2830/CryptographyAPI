using Microsoft.AspNetCore.Mvc;

namespace Cryptography.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public ActionResult<object> Get()
        {
            try
            {
                var response = new
                {
                    DataAcesso = DateTime.Now.ToLongDateString()
                };
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}