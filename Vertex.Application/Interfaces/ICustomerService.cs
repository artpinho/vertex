using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.DTOs;
using Vertex.Domain.Entities;


namespace Vertex.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateAsync(CreateCustomerRequest request);
        Task<List<CustomerResponse>> GetAllAsync();
        Task<CustomerResponse> GetByIdAsync(int id);
        Task<CustomerResponse> AddBalanceAsync(int customerId, decimal amount);
    }
}
