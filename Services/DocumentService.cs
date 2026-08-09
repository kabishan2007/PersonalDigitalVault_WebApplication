using PersonalDigitalVault_WebApplication.DTOs.Document;
using PersonalDigitalVault_WebApplication.Helpers;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IWebHostEnvironment _env;

        public DocumentService(
            IDocumentRepository documentRepository,
            IWebHostEnvironment env)
        {
            _documentRepository = documentRepository;
            _env = env;
        }

        public async Task<List<DocumentDto>> GetAllAsync(int userId)
        {
            var documents = await _documentRepository.GetAllByUserAsync(userId);
            return documents.Select(MapToDto).ToList();
        }

        public async Task<List<DocumentDto>> GetByFolderAsync(int folderId, int userId)
        {
            var documents = await _documentRepository.GetByFolderAsync(folderId, userId);
            return documents.Select(MapToDto).ToList();
        }

        public async Task<DocumentDto?> GetByIdAsync(int id, int userId)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null || document.UserId != userId)
                return null;

            return MapToDto(document);
        }

        public async Task<(byte[]? FileBytes, string? FileName, string? Message)> DownloadAsync(
            int id,
            int userId)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null || document.UserId != userId)
                return (null, null, "Document not found.");

            if (!File.Exists(document.FilePath))
                return (null, null, "File not found on server.");

            var bytes = await File.ReadAllBytesAsync(document.FilePath);

            // Integrity check
            var currentHash = DocumentHelper.GenerateSHA256(bytes);
            if (!string.Equals(currentHash, document.FileHash, StringComparison.OrdinalIgnoreCase))
                return (null, null, "File integrity check failed.");

            return (bytes, document.FileName, null);
        }

        public async Task<string> AddAsync(DocumentDto dto, int userId)
        {
            if (dto.File == null)
                return "File is required.";

            using var memoryStream = new MemoryStream();
            await dto.File.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            var hash = DocumentHelper.GenerateSHA256(fileBytes);

            var storageRoot = Path.Combine(_env.ContentRootPath, "Storage");
            Directory.CreateDirectory(storageRoot);

            var savedFileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
            var fullPath = Path.Combine(storageRoot, savedFileName);

            await File.WriteAllBytesAsync(fullPath, fileBytes);

            var document = new Document
            {
                FileName = dto.File.FileName,
                FilePath = fullPath,
                FileHash = hash,
                FolderId = dto.FolderId,
                UserId = userId
            };

            await _documentRepository.AddAsync(document);

            return "Document added successfully.";
        }

        public async Task<string> DeleteAsync(int id, int userId)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null || document.UserId != userId)
                return "Document not found.";

            if (File.Exists(document.FilePath))
                File.Delete(document.FilePath);

            await _documentRepository.DeleteAsync(document);

            return "Document deleted successfully.";
        }

        private static DocumentDto MapToDto(Document document)
        {
            return new DocumentDto
            {
                DocumentId = document.DocumentId,
                FileName = document.FileName,
                FileHash = document.FileHash,
                FolderId = document.FolderId,
                UserId = document.UserId
            };
        }
    }
}
