using Domain.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApi.ViewModels
{
    public sealed class AccountDetailsViewModel
    {
        public AccountDetailsViewModel(Account account)
        {
            AccountId = account.AccountId.Id;
            CurrentBalance = account.GetCurrentBalance().Amount;
            Credits = account.CreditsCollection.Select(e => new CreditViewModel(e)).ToList();
            Debits = account.DebitsCollection.Select(e => new DebitViewModel(e)).ToList();
        }

        [Required]
        public Guid AccountId { get; }

        [Required]
        public decimal CurrentBalance { get; }

        [Required]
        public List<CreditViewModel> Credits { get; }

        [Required]
        public List<DebitViewModel> Debits { get; }
    }
}
