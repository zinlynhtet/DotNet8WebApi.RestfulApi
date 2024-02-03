using DotNet8WebApi.Features.Blog;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.EFDbContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<BlogDataModel> Data { get; set; }
}
