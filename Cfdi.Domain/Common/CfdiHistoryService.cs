using Cfdi.Domain.Models;
using Cfdi.Domain.Services;
using Cfdi.Domain.Repository;

namespace Cfdi.Domain.Common
{
    public class CfdiHistoryService : Service<CfdiHistory>, ICfdiHistoryService
    {
        public CfdiHistoryService(ICfdiHistoryRepository repository) : base(repository)
        {
        }
    }
}
