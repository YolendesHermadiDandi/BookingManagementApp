using API.DTOs.Account;
using API.Models;
using API.Utilities.Handler;
using Client.Contracts;
using Client.DTOs.Auth;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Client.Repositories
{
    public class AccountRepository : GeneralRepository<AccountDto, Guid>, IAccountRepository
    {
        public AccountRepository(string request = "account/") : base(request)
        {

        }


        public async Task<ResponseOkHandler<TokenDto>> Login(LoginAccountDto entity)
        {
            ResponseOkHandler<TokenDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request+"Login", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<TokenDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}
