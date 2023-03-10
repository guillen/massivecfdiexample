using Cfdi.Domain.Models;
using Cfdi.Domain.Services;
using Cfdi.Domain.Repository;

namespace Cfdi.Domain.Common
{
    public class QueueService : Service<Queue>, IQueueService
    {
        public QueueService(IQueueRepository repository) : base(repository)
        {
        }

        public async virtual Task<Queue> AddQueue(Queue queue)
        {
            return await ((IQueueRepository)_repository).AddQueue(queue);
        }

        public async virtual Task<Queue> GetNext()
        {
            return await ((IQueueRepository)_repository).GetNext();
        }

        public async virtual Task<bool> HasNext()
        {
            return await ((IQueueRepository)_repository).HasNext();
        }

        public async virtual Task<int> PopQueue(Queue queue)
        {
            return await ((IQueueRepository)_repository).PopQueue(queue);
        }
    }
}
