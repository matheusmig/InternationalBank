using Domain.Accounts;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    public sealed class AccountViewModel
    {
        public AccountViewModel(Account account)
        {
            AccountId = account.AccountId.Id;
            CurrentBalance = account.GetCurrentBalance().Amount;
            Currency = account.Currency.Code;
        }

        [Required]
        public Guid AccountId { get; }

        [Required]
        public decimal CurrentBalance { get; }

        [Required]
        public string Currency { get; }
    }
}
