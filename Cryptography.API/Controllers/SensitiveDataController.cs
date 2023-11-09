using Microsoft.AspNetCore.Mvc;
using Cryptography.API.Models;
using Cryptography.API.Services.Interfaces;
using Cryptography.API.DTOs;

namespace Cryptography.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensitiveDataController : ControllerBase
    {
        private readonly ISensitiveDataService _sensitiveDataService;

        public SensitiveDataController(ISensitiveDataService sensitiveDataService)
        {
            _sensitiveDataService = sensitiveDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _sensitiveDataService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var reponse = new 
                {
                    message = ex.Message
                };
                return BadRequest(reponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var result = await _sensitiveDataService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var reponse = new 
                {
                    message = ex.Message
                };
                return BadRequest(reponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SensitiveDataDTO model)
        {
            try
            {
                var result = await _sensitiveDataService.Create(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var reponse = new 
                {
                    message = ex.Message
                };
                return BadRequest(reponse);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(SensitiveDataDTO model)
        {
            try
            {
                var result = await _sensitiveDataService.Update(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var reponse = new 
                {
                    message = ex.Message
                };
                return BadRequest(reponse);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await _sensitiveDataService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var reponse = new 
                {
                    message = ex.Message
                };
                return BadRequest(reponse);
            }
        }
    }
}
