using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.DTOs;
using Vertex.Application.Interfaces;
using Vertex.Domain.Entities;
using Vertex.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Vertex.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;

        public CustomerService(AppDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<CustomerResponse> CreateAsync(CreateCustomerRequest request)
        {
            var exists = await _context.Customers
                .AnyAsync(c => c.Username == request.Username);

            if (exists)
                throw new Exception("Esse usuário já existe");

            var customer = new Customer
            {
                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = _passwordService.HashPassword(request.Password),
                Balance = 0
            };
            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Username = customer.Username,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Balance = customer.Balance,
                CreatedAt = customer.CreatedAt
            };
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Username = c.Username,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Balance = c.Balance,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            return await _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Username = c.Username,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Balance = c.Balance,
                    CreatedAt = c.CreatedAt
                })
                .FirstOrDefaultAsync() ?? throw new Exception("Cliente não encontrado");
        }

        public async Task<CustomerResponse> AddBalanceAsync(int id, decimal amount)
        {
            if (amount <= 0)
                throw new Exception("O valor a ser adicionado deve ser maior que zero.");

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                throw new Exception("Cliente não encontrado");

            customer.Balance += amount;

            await _context.SaveChangesAsync();

            return new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Username = customer.Username,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Balance = customer.Balance,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}
