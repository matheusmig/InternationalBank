using System;
using System.Threading.Tasks;

namespace Application.UseCases.OpenAccount
{
    public interface IOpenAccountUseCase
    {
        Task ExecuteAsync(decimal amount, string currency);
        void SetOutputPort(IOutputPort outputPort);
    }
}
