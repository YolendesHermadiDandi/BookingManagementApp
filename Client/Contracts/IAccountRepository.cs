using API.DTOs.Account;
using API.DTOs.Employee;
using API.Models;
using API.Utilities.Handler;
using Client.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Client.Contracts
{
    public interface IAccountRepository : IRepository<AccountDto, Guid>
    {

        Task<ResponseOkHandler<TokenDto>> Login(LoginAccountDto entity);
    }
}
