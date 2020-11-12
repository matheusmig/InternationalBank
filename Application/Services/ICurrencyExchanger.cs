using Domain.ValueObjects;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICurrencyExchanger
    {
        Task<Money> Convert(Money from, Currency to);
    }
}
