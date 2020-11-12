using System;
using System.Threading.Tasks;

namespace Application.UseCases.DepositMoney
{
    public interface IDepositMoneyUseCase
    {
        Task ExecuteAsync(Guid accountId, decimal amount, string currency);
        void SetOutputPort(IOutputPort outputPort);
    }
}
