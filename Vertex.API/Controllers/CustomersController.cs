using Microsoft.AspNetCore.Mvc;
using Vertex.Application.DTOs;
using Vertex.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace Vertex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            try
            {
                var result = await _customerService.CreateAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _customerService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("add-balance")]
        public async Task<IActionResult> AddBalance(AddBalanceRequest request)
        {
            var customerIdClaim = User.FindFirst("CustomerId")?.Value;

            if (customerIdClaim == null)
                return Unauthorized("CustomerId claim is missing or invalid.");

            var customerId = int.Parse(customerIdClaim);

            try
            {
                var result = await _customerService.AddBalanceAsync(customerId, request.Amount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
