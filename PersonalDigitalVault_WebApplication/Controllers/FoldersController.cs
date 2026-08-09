using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalDigitalVault_WebApplication.DTOs.Folder;
using PersonalDigitalVault_WebApplication.Helpers;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FoldersController : ControllerBase
    {
        private readonly IFolderService _folderService;

        public FoldersController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = UserHelper.GetUserId(User);
            return Ok(await _folderService.GetAllFolders(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = UserHelper.GetUserId(User);
            var folder = await _folderService.GetById(id, userId);

            if (folder == null)
                return NotFound(new { message = "Folder Not Found" });

            return Ok(folder);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FolderDto dto)
        {
            var userId = UserHelper.GetUserId(User);
            return Ok(await _folderService.CreateFolder(dto, userId));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FolderDto dto)
        {
            var userId = UserHelper.GetUserId(User);
            var result = await _folderService.UpdateFolder(id, dto, userId);

            if (result == "Folder Not Found")
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = UserHelper.GetUserId(User);
            var result = await _folderService.DeleteFolder(id, userId);

            if (result == "Folder Not Found")
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }


    }
}