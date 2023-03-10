using Cfdi.Domain.Common;
using Cfdi.Domain.Models;
using Cfdi.Domain.Repository;
using Cfdi.Worker.Context;

namespace Cfdi.Worker.Repository
{
    internal class CfdiHistoryRepository : Repository<CfdiHistory>, ICfdiHistoryRepository
    {
        public CfdiHistoryRepository(WorkerDbContext context) : base(context)
        {
        }
    }
}
