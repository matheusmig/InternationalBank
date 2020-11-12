using System.ComponentModel.DataAnnotations;

namespace WebApi.UseCases.V1.Transactions.Withdraw
{
    public class WithdrawRequest
    {
        [Required]
        public decimal Amount{ get; set;  }

        [Required]
        public string Currency { get; set;  }
    }
}
