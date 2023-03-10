using Cfdi.Domain.Common;
using Cfdi.Domain.Models;
using Cfdi.Domain.Repository;
using Cfdi.Worker.Context;
using Microsoft.EntityFrameworkCore;

namespace Cfdi.Worker.Repository
{
    internal class QueueRepository : Repository<Queue>, IQueueRepository
    {
        public QueueRepository(WorkerDbContext context) : base(context)
        {
        }

        public async virtual Task<Queue> AddQueue(Queue queue)
        {
            throw new NotImplementedException();
        }

        public async virtual Task<bool> HasNext()
        {
            return await ((WorkerDbContext)_context).Queues.CountAsync() > 0;
        }

        public async virtual Task<int> PopQueue(Queue queue)
        {
            ((WorkerDbContext)_context).Queues.Remove(queue);
            return await ((WorkerDbContext)_context).SaveChangesAsync();
        }

        public async virtual Task<Queue> GetNext()
        {
            return await ((WorkerDbContext)_context).Queues.OrderBy(x => x.Order).FirstOrDefaultAsync();
        }
    }
}
