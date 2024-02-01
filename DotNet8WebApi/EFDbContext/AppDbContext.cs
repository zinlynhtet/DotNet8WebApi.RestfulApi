using DotNet8WebApi.Features.Blogs;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.EFDbContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<BlogDataModel> Data { get; set; }
}
