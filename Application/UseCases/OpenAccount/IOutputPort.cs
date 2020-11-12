using Application.Services;
using Domain.Accounts;
using Domain.Accounts.Debits;

namespace Application.UseCases.OpenAccount
{
    public interface IOutputPort
    {
        void Invalid(ApplicationResult result);
        void NotFound();
        void Ok(Account account);
    }
}
