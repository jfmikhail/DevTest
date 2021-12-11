using DeveloperTest.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeveloperTest.Application
{
    public abstract class ApplicationService<TResult, TRequest> : IApplicationService<TResult, TRequest>
        where TResult : class
    {

        public async Task<ApplicationResult<TResult>> ExecuteAsync(TRequest request)
        {
            try
            {
                return await DoExecuteAsync(request);
            }
            catch (Exception ex)
            {
                return Fail();
            }
        }

        public abstract Task<ApplicationResult<TResult>> DoExecuteAsync(TRequest request);

        protected ApplicationResult<TResult> Success(TResult data)
        {
            return ApplicationResult<TResult>.Success(data);
        }

        protected ApplicationResult<TResult> Fail()
        {
            return ApplicationResult<TResult>.Fail("Internal error.");
        }

        protected ApplicationResult<TResult> BadRequest(params string[] errors)
        {
            return ApplicationResult<TResult>.BadRequest(errors);
        }

        protected ApplicationResult<TResult> BadRequest(IEnumerable<string> errors)
        {
            return ApplicationResult<TResult>.BadRequest(errors);
        }

        protected ApplicationResult<TResult> NotFound(params string[] errors)
        {
            return ApplicationResult<TResult>.NotFound(errors);
        }

        protected ApplicationResult<TResult> Created(TResult data)
        {
            return ApplicationResult<TResult>.Created(data);
        }
    }
}
