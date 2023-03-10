using Cfdi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Services
{
    public interface IQueueService : IService<Queue>
    {
        Task<Queue> AddQueue(Queue q);
        Task<Queue> GetNext();
        Task<bool> HasNext();
        Task<int> PopQueue(Queue queue);
    }
}
