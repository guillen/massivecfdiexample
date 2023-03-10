using Cfdi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Cfdi.API.Context
{
    public class CfdiDbContext : DbContext
    {
        public CfdiDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Queue> Queues { get; set; }
        public DbSet<CfdiHistory> CfdiHistories { get; set; }
    }
}
