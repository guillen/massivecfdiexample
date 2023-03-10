using Cfdi.Domain.Models;
using Cfdi.Domain.Services;

namespace Cfdi.Domain.Repository
{
    public interface IQueueRepository : IRepository<Queue>
    {
        Task<Queue> AddQueue(Queue queue);
        Task<Queue> GetNext();
        Task<bool> HasNext();
        Task<int> PopQueue(Queue queue);
    }
}
