using Application.Services;
using Domain.Accounts;
using Domain.Accounts.Debits;

namespace Application.UseCases.WithdrawMoney
{
    public interface IOutputPort
    {
        void OutOfFunds();
        void Invalid(ApplicationResult result);
        void NotFound();
        void Ok(Debit debit, Account account);
    }
}
