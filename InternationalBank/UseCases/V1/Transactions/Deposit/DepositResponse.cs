using System.ComponentModel.DataAnnotations;
using WebApi.ViewModels;

namespace WebApi.UseCases.V1.Transactions.Deposit
{
    public class DepositResponse
    {
        public DepositResponse(CreditViewModel transaction)
        {
            Transaction = transaction;
        }

        [Required]
        public CreditViewModel Transaction { get; }
    }
}
