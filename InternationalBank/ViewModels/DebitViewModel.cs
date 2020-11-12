using Domain.Accounts.Debits;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    public class DebitViewModel
    {
        public DebitViewModel(Debit Debit)
        {
            TransactionId = Debit.DebitId.Id;
            Amount = Debit.Amount.Amount;
            Currency = Debit.Amount.Currency.Code;
            Description = "Debit";
            TransactionDate = Debit.TransactionDate;
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
