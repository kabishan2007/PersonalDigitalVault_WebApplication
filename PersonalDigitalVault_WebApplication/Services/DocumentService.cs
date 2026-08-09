using PersonalDigitalVault_WebApplication.DTOs.Document;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<List<DocumentDto>> GetAllAsync()
        {
            var documents = await _documentRepository.GetAllAsync();

            return documents.Select(document => new DocumentDto
            {
                FolderId = document.FolderId,
                UserId = document.UserId
            }).ToList();
        }

        // GET BY ID
        public async Task<DocumentDto?> GetByIdAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null)
            {
                return null;
            }

            return new DocumentDto
            {
                FolderId = document.FolderId,
                UserId = document.UserId
            };
        }

        // ADD
        public async Task<string> AddAsync(DocumentDto dto)
        {
            if (dto.File == null)
            {
                return "File is required.";
            }

            var document = new Document
            {
                FileName = dto.File.FileName,
                FilePath = "",
                FileHash = "",
                FolderId = dto.FolderId,
                UserId = dto.UserId
            };

            await _documentRepository.AddAsync(document);

            return "Document added successfully.";
        }

        // DELETE
        public async Task<string> DeleteAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null)
            {
                return "Document not found.";
            }

            await _documentRepository.DeleteAsync(document);

            return "Document deleted successfully.";
        }
    }
}
