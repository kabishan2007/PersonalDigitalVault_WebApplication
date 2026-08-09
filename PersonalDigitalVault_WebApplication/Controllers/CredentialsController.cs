using Microsoft.AspNetCore.Mvc;
using PersonalDigitalVault_WebApplication.DTOs.Credential;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialsController : ControllerBase
    {
        private readonly ICredentialService _service;
        public CredentialsController(ICredentialService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();

            return Ok(result);
        }

        // GET: api/Credential/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);

            if (result == null)
            {
                return NotFound("Credential Not Found");
            }

            return Ok(result);
        }

        // POST: api/Credential
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CredentialDto dto)
        {
            var result = await _service.Create(dto);

            return Ok(result);
        }

        // PUT: api/Credential/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CredentialDto dto)
        {
            var result = await _service.Update(id, dto);

            if (result == "Credential Not Found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // DELETE: api/Credential/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);

            if (result == "Credential Not Found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

    }
}
