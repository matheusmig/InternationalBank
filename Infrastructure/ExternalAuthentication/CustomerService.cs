using Application.Services;
using Domain.ValueObjects;
using Infrastructure.DataAccess;

namespace Infrastructure.ExternalAuthentication
{
    public sealed class CustomerService : ICustomerService
    {
        public CustomerId GetCurrentCustomerId()
        {
            return SeedData.DefaultCustomerId;
        }        
    }
}
