using Domain.Accounts;
using System.ComponentModel.DataAnnotations;
using WebApi.ViewModels;

namespace WebApi.UseCases.V1.Accounts.GetAccount
{
    public sealed class GetAccountResponse
    {
        public GetAccountResponse(Account account)
        {
            Account = new AccountDetailsViewModel(account);
        }

        [Required]
        public AccountDetailsViewModel Account { get; }
    }
}
