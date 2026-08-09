namespace PersonalDigitalVault_WebApplication.DTOs.Document
{
    public class DocumentDto
    {
        public IFormFile File { get; set; }

        public int FolderId { get; set; }

        public int UserId { get; set; }
    }
}
