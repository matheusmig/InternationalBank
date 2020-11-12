using Application.Services;
using Application.UseCases.GetAccount;
using Domain.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.UseCases.V1.Accounts.GetAccount
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase, IOutputPort
    {
        private IActionResult? _viewModel;

        void IOutputPort.Invalid(ApplicationResult applicationResult)
        {
            var validationProblemDetails = new ValidationProblemDetails(applicationResult.ModelState);
            _viewModel = BadRequest(validationProblemDetails);
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        void IOutputPort.Ok(Account account)
        {
            _viewModel = Ok(new GetAccountResponse(account));
        }

        [HttpGet("{accountId:guid}", Name = "GetAccount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccountResponse))]
        public async Task<IActionResult> GetAsync(
            [FromServices] IGetAccountUseCase useCase,
            [FromRoute] [Required] Guid accountId)
        {
            useCase.SetOutputPort(this);

            await useCase.ExecuteAsync(accountId);

            return _viewModel;
        }
       
    }
}
