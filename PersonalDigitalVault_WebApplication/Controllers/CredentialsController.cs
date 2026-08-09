using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalDigitalVault_WebApplication.DTOs.Credential;
using PersonalDigitalVault_WebApplication.Helpers;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    [Authorize]
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
            var userId = UserHelper.GetUserId(User);
            return Ok(await _service.GetAll(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = UserHelper.GetUserId(User);
            var result = await _service.GetById(id, userId);

            if (result == null)
                return NotFound(new { message = "Credential Not Found" });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CredentialDto dto)
        {
            var userId = UserHelper.GetUserId(User);
            return Ok(new { message = await _service.Create(dto, userId) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CredentialDto dto)
        {
            var userId = UserHelper.GetUserId(User);
            var result = await _service.Update(id, dto, userId);

            if (result == "Credential Not Found")
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = UserHelper.GetUserId(User);
            var result = await _service.Delete(id, userId);

            if (result == "Credential Not Found")
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }
    }
}
