using Cfdi.API.Context;
using Cfdi.Domain.Common;
using Cfdi.Domain.Models;
using Cfdi.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cfdi.API.Repository
{
    internal class QueueRepository : Repository<Queue>, IQueueRepository
    {
        public QueueRepository(CfdiDbContext context) : base(context)
        {
        }

        public async virtual Task<Queue> AddQueue(Queue queue)
        {
            queue.Order = await ((CfdiDbContext)_context).Queues.CountAsync() + 1;
            return await AddAsync(queue);
        }

        public Task<bool> HasNext()
        {
            throw new NotImplementedException();
        }

        public Task<int> PopQueue(Queue queue)
        {
            throw new NotImplementedException();
        }

        public Task<Queue> GetNext()
        {
            throw new NotImplementedException();
        }
    }
}
