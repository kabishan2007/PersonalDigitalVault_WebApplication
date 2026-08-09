using Microsoft.AspNetCore.Mvc;
using PersonalDigitalVault_WebApplication.DTOs.Document;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // GET: api/documents
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var documents = await _documentService.GetAllAsync();

            return Ok(documents);
        }

        // GET: api/documents/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var document = await _documentService.GetByIdAsync(id);

            if (document == null)
            {
                return NotFound(new
                {
                    message = "Document not found."
                });
            }

            return Ok(document);
        }

        // POST: api/documents
        [HttpPost]
        public async Task<IActionResult> Add(
            [FromForm] DocumentDto dto)
        {
            var result = await _documentService.AddAsync(dto);

            if (result == "File is required.")
            {
                return BadRequest(new
                {
                    message = result
                });
            }

            return Ok(new
            {
                message = result
            });
        }

        // DELETE: api/documents/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _documentService.DeleteAsync(id);
            return Ok(new
            {
                message = result
            });


        }


    }
}
