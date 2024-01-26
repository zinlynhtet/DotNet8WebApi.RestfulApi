using DotNet8WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<BlogDataModel> Data {  get; set; }
    }
}
