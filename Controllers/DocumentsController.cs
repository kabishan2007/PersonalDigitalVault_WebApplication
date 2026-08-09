using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalDigitalVault_WebApplication.DTOs.Document;
using PersonalDigitalVault_WebApplication.Helpers;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = UserHelper.GetUserId(User);
            return Ok(await _documentService.GetAllAsync(userId));
        }

        [HttpGet("folder/{folderId}")]
        public async Task<IActionResult> GetByFolder(int folderId)
        {
            var userId = UserHelper.GetUserId(User);
            return Ok(await _documentService.GetByFolderAsync(folderId, userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = UserHelper.GetUserId(User);
            var document = await _documentService.GetByIdAsync(id, userId);

            if (document == null)
                return NotFound(new { message = "Document not found." });

            return Ok(document);
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> Download(int id)
        {
            var userId = UserHelper.GetUserId(User);
            var (fileBytes, fileName, message) =
                await _documentService.DownloadAsync(id, userId);

            if (fileBytes == null)
                return NotFound(new { message });

            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] DocumentDto dto)
        {
            var userId = UserHelper.GetUserId(User);
            var result = await _documentService.AddAsync(dto, userId);

            if (result == "File is required.")
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = UserHelper.GetUserId(User);
            var result = await _documentService.DeleteAsync(id, userId);

            if (result == "Document not found.")
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }


    }
}