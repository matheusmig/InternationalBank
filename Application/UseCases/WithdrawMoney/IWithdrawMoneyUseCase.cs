using System;
using System.Threading.Tasks;

namespace Application.UseCases.WithdrawMoney
{
    public interface IWithdrawMoneyUseCase
    {
        Task ExecuteAsync(Guid accountId, decimal amount, string currency);
        void SetOutputPort(IOutputPort outputPort);
    }
}
