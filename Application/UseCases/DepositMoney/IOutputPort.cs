using Application.Services;
using Domain.Accounts;
using Domain.Accounts.Credits;

namespace Application.UseCases.DepositMoney
{
    public interface IOutputPort
    {
        void Invalid(ApplicationResult result);
        void Ok(Credit credit, Account account);
        void NotFound();
    }
}
