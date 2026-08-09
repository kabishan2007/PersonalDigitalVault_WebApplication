namespace PersonalDigitalVault_WebApplication.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileHash { get; set; }

        public int UserId { get; set; }

        public int FolderId { get; set; }
    }
}
