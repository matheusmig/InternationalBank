using System;
using System.Threading.Tasks;

namespace Application.UseCases.GetAccount
{
    public interface IGetAccountUseCase
    {
        Task ExecuteAsync(Guid accountId);
        void SetOutputPort(IOutputPort outputPort);
    }
}
