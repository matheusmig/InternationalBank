using System.ComponentModel.DataAnnotations;

namespace WebApi.UseCases.V1.Transactions.Deposit
{
    public class DepositRequest
    {
        [Required]
        public decimal Amount{ get; set;  }

        [Required]
        public string Currency { get; set;  }
    }
}
