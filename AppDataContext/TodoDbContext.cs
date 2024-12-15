// AppDataContext.TodoDbContext.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace TodoAPI.AppDataContext {
    public class TodoDbContext : DbContext {
        private readonly Utils _dbSettings;

        public TodoDbContext(IOptions<Utils> dbSettings) {
            _dbSettings = dbSettings.Value;
        }

        [Required]
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
                optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Todo>()
                .ToTable("TodoAPI")
                .HasKey(x => x.Id);
        }
    }
}