using Domain.Accounts.Credits;
using Domain.Accounts.Debits;
using Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Accounts
{
    public interface IAccountRepository
    {
        Task<IAccount> Get(AccountId accountId);
        Task<IEnumerable<IAccount>> GetAllFromCustomer(CustomerId customerId);

        Task<IAccount> Find(AccountId accountId, CustomerId customerId);
        Task Add(Account account, Credit credit);
        Task Update(Account account, Credit credit);
        Task Update(Account account, Debit debit);
        Task Delete(AccountId accountId);
    }
}
