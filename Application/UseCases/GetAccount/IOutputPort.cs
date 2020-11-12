using Application.Services;
using Domain.Accounts;

namespace Application.UseCases.GetAccount
{
    public interface IOutputPort
    {
        void Invalid(ApplicationResult result);
        void NotFound();
        void Ok(Account account);
    }
}
