using API.Utilities.Handler;
using Client.Contracts;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Repositories
{
    public class GeneralRepository<Entity, TId> : IRepository<Entity, TId>
        where Entity : class
    {
        protected readonly string request;
        private readonly HttpContextAccessor contextAccessor;
        protected HttpClient httpClient;
        //constructor
        public GeneralRepository(string request)
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7100/api/")
            };
            contextAccessor = new HttpContextAccessor();
            // Ini yg bawah skip dulu
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
        }

        public async Task<ResponseOkHandler<Entity>> Delete(TId id)
        {
            ResponseOkHandler<Entity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = httpClient.DeleteAsync(request + id).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOkHandler<IEnumerable<Entity>>> Get()
        {
            ResponseOkHandler<IEnumerable<Entity>> entityVM = null;
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<IEnumerable<Entity>>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOkHandler<Entity>> Get(TId id)
        {
            ResponseOkHandler<Entity> entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }
            return entity;
        }

        public async Task<ResponseOkHandler<Entity>> Post(Entity entity)
        {
            ResponseOkHandler<Entity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOkHandler<Entity>> Put(TId id, Entity entity)
        {
            ResponseOkHandler<Entity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request + id, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }
    }
}