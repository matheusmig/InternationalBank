using Application.Services;
using Application.UseCases.WithdrawMoney;
using Domain.Accounts;
using Domain.Accounts.Debits;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApi.ViewModels;

namespace WebApi.UseCases.V1.Transactions.Withdraw
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

        void IOutputPort.Ok(Debit debit, Account account)
        {
            _viewModel = Ok(new WithdrawResponse(new DebitViewModel(debit)));
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        void IOutputPort.OutOfFunds()
        {
            var messages = new Dictionary<string, string[]> { { "", new[] { "Out of funds." } } };

            var problemDetails = new ValidationProblemDetails(messages);
            _viewModel = BadRequest(problemDetails);
        }

        [HttpPatch("{accountId:guid}/Withdraw")]
        public async Task<IActionResult> Withdraw(
           [FromServices] IWithdrawMoneyUseCase useCase,
           [FromRoute][Required] Guid accountId,
           [FromBody][Required] WithdrawRequest request)
        {
            useCase.SetOutputPort(this);

            await useCase.ExecuteAsync(accountId, request.Amount, request.Currency);

            return _viewModel!;
        }
    }
}
