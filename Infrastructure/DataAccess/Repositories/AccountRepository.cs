using Domain.Accounts;
using Domain.Accounts.Credits;
using Domain.Accounts.Debits;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositories
{
    public sealed class AccountRepository : IAccountRepository
    {
        private readonly InternationalBankContext _dbContext;

        public AccountRepository(InternationalBankContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(Account account, Credit credit)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.Credits.AddAsync(credit);
        }

        public async Task Delete(AccountId accountId)
        {
            var account = await _dbContext.Accounts.SingleOrDefaultAsync(x => x.AccountId == accountId);
            if (account is null)
                return;

            var creditsToRemove = _dbContext.Credits.Where(x => x.AccountId == accountId).Select(e => e);
            _dbContext.Credits.RemoveRange(creditsToRemove);

            var debitsToRemove = _dbContext.Debits.Where(x => x.AccountId == accountId).Select(e => e);
            _dbContext.Debits.RemoveRange(debitsToRemove);

            _dbContext.Accounts.Remove(account);
        }

        public async Task<IAccount> Find(AccountId accountId, CustomerId customerId)
        {
            var account = await _dbContext.Accounts.SingleOrDefaultAsync(x => x.AccountId == accountId && x.CustomerId == customerId);
            if (account is Account findAccount)
            {
                await LoadTransactions(findAccount);

                return account;
            }

            return null;
        }

        public async Task<IAccount> Get(AccountId accountId)
        {
            var account = await _dbContext.Accounts.SingleOrDefaultAsync(x => x.AccountId == accountId);
            if (account is Account findAccount)
            {
                await LoadTransactions(findAccount);

                return account;
            }

            return null;
        }

        public async Task<IEnumerable<IAccount>> GetAllFromCustomer(CustomerId customerId)
        {
            var accounts = await _dbContext.Accounts.Where(e => e.CustomerId == customerId).ToListAsync();
            foreach (Account findAccount in accounts)
            {
                await LoadTransactions(findAccount);
            }

            return accounts;
        }

        public async Task Update(Account account, Credit credit)
        {
            await _dbContext.Credits.AddAsync(credit);
        }

        public async Task Update(Account account, Debit debit)
        {
            await _dbContext.Debits.AddAsync(debit);
        }

        private async Task LoadTransactions(Account account)
        {
            await _dbContext.Credits.Where(e => e.AccountId.Equals(account.AccountId)).ToListAsync();
            await _dbContext.Debits.Where(e => e.AccountId.Equals(account.AccountId)).ToListAsync();
        }
    }
}
