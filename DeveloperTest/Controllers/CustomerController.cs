using DeveloperTest.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DeveloperTest.Controllers
{
    [ApiController, Route("[controller]")]
    public class CustomerController : BaseController
    {
        private readonly ICreateCustomerApplicationService _createCustomerApplicationService;
        private readonly IGetCustomerApplicationService _getCustomerApplicationService;
        private readonly IGetAllCustomersApplicationService _getAllCustomersApplicationService;

        public CustomerController(ICreateCustomerApplicationService createCustomerApplicationService,
            IGetCustomerApplicationService getCustomerApplicationService,
            IGetAllCustomersApplicationService getAllCustomersApplicationService)
        {
            _createCustomerApplicationService = createCustomerApplicationService ?? throw new ArgumentNullException(nameof(createCustomerApplicationService));
            _getCustomerApplicationService = getCustomerApplicationService ?? throw new ArgumentNullException(nameof(getCustomerApplicationService));
            _getAllCustomersApplicationService = getAllCustomersApplicationService ?? throw new ArgumentNullException(nameof(getAllCustomersApplicationService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return MapResponse(await _getAllCustomersApplicationService.ExecuteAsync(new GetAllCustomersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return MapResponse(await _getCustomerApplicationService.ExecuteAsync(new GetCustomerQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerCommand customer)
        {
            return MapResponse(await _createCustomerApplicationService.ExecuteAsync(customer));
        }
    }
}