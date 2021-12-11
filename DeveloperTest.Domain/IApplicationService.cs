using DeveloperTest.Domain;
using System.Threading.Tasks;

namespace DeveloperTest.Application
{
    public interface IApplicationService<TResult, TRequest> where TResult : class
    {
        Task<ApplicationResult<TResult>> ExecuteAsync(TRequest request);
    }
}
