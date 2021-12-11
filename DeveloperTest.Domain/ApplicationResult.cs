using System.Collections.Generic;
using System.Linq;

namespace DeveloperTest.Domain
{
    public class ApplicationResult<T> where T : class
    {
        private readonly StatusCode _statusCode = StatusCode.Success;

        protected ApplicationResult(StatusCode statusCode, T data = null, IEnumerable<string> errors = null)
        {
            (_statusCode, Data, Errors) = (statusCode, data, errors ?? Enumerable.Empty<string>());
        }

        public bool Succeeded => _statusCode == StatusCode.Success || _statusCode == StatusCode.Created || _statusCode == StatusCode.NoContent;

        public IEnumerable<string> Errors { get; }

        public T Data { get; }

        public StatusCode GetStatusCode()
        {
            return _statusCode;
        }

        public static ApplicationResult<T> Success(T data = null)
        {
            return new(StatusCode.Success, data: data);
        }

        public static ApplicationResult<T> Fail(params string[] errors)
        {
            return new(StatusCode.Error, errors: errors);
        }

        public static ApplicationResult<T> Fail(IEnumerable<string> errors)
        {
            return new(StatusCode.Error, errors: errors);
        }

        public static ApplicationResult<T> BadRequest(params string[] errors)
        {
            return new(StatusCode.BadRequest, errors: errors);
        }

        public static ApplicationResult<T> BadRequest(IEnumerable<string> errors)
        {
            return new(StatusCode.BadRequest, errors: errors);
        }

        public static ApplicationResult<T> NotFound(params string[] errors)
        {
            return new(StatusCode.NotFound, errors: errors);
        }

        public static ApplicationResult<T> Created(T data = null)
        {
            return new(StatusCode.Created, data: data);
        }
    }
}
