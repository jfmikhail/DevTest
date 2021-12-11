using DeveloperTest.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeveloperTest.Controllers
{
    [ApiController, Route("[controller]")]
    public class JobController : BaseController
    {
        private readonly IGetAllJobsApplicationService _getAllJobsApplicationService;
        private readonly IGetJobApplicationService _getJobApplicationService;
        private readonly ICreateJobApplicationService _createJobApplicationService;

        public JobController(IGetAllJobsApplicationService getAllJobsApplicationService,
            IGetJobApplicationService getJobApplicationService,
            ICreateJobApplicationService createJobApplicationService)
        {
            _getAllJobsApplicationService = getAllJobsApplicationService;
            _getJobApplicationService = getJobApplicationService;
            _createJobApplicationService = createJobApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return MapResponse(await _getAllJobsApplicationService.ExecuteAsync(new GetAllJobsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return MapResponse(await _getJobApplicationService.ExecuteAsync(new GetJobQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateJobCommand job)
        {
            return MapResponse(await _createJobApplicationService.ExecuteAsync(job));
        }
    }
}