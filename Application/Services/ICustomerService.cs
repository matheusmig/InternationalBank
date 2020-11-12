using Domain.ValueObjects;

namespace Application.Services
{
    public interface ICustomerService
    {
        CustomerId GetCurrentCustomerId();
    }
}
