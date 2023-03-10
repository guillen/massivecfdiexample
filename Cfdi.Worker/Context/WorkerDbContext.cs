using Cfdi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Worker.Context
{
    internal class WorkerDbContext : DbContext
    {
        public WorkerDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Queue> Queues { get; set; }
        public DbSet<CfdiHistory> CfdiHistories { get; set; }
    }
}
