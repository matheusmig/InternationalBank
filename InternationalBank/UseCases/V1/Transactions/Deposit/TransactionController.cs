using Application.Services;
using Application.UseCases.DepositMoney;
using Domain.Accounts;
using Domain.Accounts.Credits;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApi.ViewModels;

namespace WebApi.UseCases.V1.Transactions.Deposit
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class TransactionController : ControllerBase, IOutputPort
    {
        private IActionResult? _viewModel;

        void IOutputPort.Invalid(ApplicationResult result)
        {
            var validationProblemDetails = new ValidationProblemDetails(result.ModelState);
            _viewModel = BadRequest(validationProblemDetails);
        }

        void IOutputPort.Ok(Credit credit, Account account)
        {
            _viewModel = Ok(new DepositResponse(new CreditViewModel(credit)));
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        [HttpPatch("{accountId:guid}/Deposit")]
        public async Task<IActionResult> Deposit(
           [FromServices] IDepositMoneyUseCase useCase,
           [FromRoute][Required] Guid accountId,
           [FromBody][Required] DepositRequest request)
        {
            useCase.SetOutputPort(this);

            await useCase.ExecuteAsync(accountId, request.Amount, request.Currency);

            return this._viewModel!;
        }
    }
}
