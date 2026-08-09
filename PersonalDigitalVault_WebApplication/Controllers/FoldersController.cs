using Microsoft.AspNetCore.Mvc;
using PersonalDigitalVault_WebApplication.DTOs.Folder;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Controllers
{
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
            return Ok(await _folderService.GetAllFolders());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FolderDto dto)
        {
            return Ok(await _folderService.CreateFolder(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FolderDto dto)
        {
            return Ok(await _folderService.UpdateFolder(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _folderService.DeleteFolder(id));
        }


    }
}
