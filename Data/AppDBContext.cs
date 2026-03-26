using Microsoft.EntityFrameworkCore;
using notes_api_app.app.Models;

namespace notes_api_app.app.Data;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Note> Notes { get; set; }
    public DbSet<User> Users { get; set; }
}