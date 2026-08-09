namespace PersonalDigitalVault_WebApplication.DTOs.Document
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }

        public string? FileName { get; set; }

        public string? FileHash { get; set; }

        public int FolderId { get; set; }

        public int UserId { get; set; }

        public IFormFile? File { get; set; }
    }
}
