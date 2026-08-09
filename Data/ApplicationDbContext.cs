using Microsoft.EntityFrameworkCore;
using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Data
{
    public class ApplicationDbContext :DbContext
    {

        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Folder> Folders { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<CredentialRecord> CredentialRecords { get; set; }
    }
}
