using Domain.Accounts.Credits;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    public class CreditViewModel
    {
        public CreditViewModel(Credit credit)
        {
            TransactionId = credit.CreditId.Id;
            Amount = credit.Amount.Amount;
            Currency = credit.Amount.Currency.Code;
            Description = "Credit";
            TransactionDate = credit.TransactionDate;
        }

        [Required]
        public Guid TransactionId { get; }

        [Required]
        public decimal Amount { get; }

        [Required]
        public string Currency { get; }

        [Required]
        public string Description { get; }

        [Required]
        public DateTime TransactionDate { get; }
    }
}
