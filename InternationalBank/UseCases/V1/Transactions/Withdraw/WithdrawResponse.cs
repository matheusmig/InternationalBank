using System.ComponentModel.DataAnnotations;
using WebApi.ViewModels;

namespace WebApi.UseCases.V1.Transactions.Withdraw
{
    public class WithdrawResponse
    {
        public WithdrawResponse(DebitViewModel transaction)
        {
            Transaction = transaction;
        }

        [Required]
        public DebitViewModel Transaction { get; }
    }
}
