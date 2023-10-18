using API.Utilities.Handler;

namespace Client.Contracts
{
    public interface IRepository<T, X>
        where T : class
    {
            Task<ResponseOkHandler<IEnumerable<T>>> Get();
            Task<ResponseOkHandler<T>> Get(X id);
            Task<ResponseOkHandler<T>> Post(T entity);
            Task<ResponseOkHandler<T>> Put(X id, T entity);
            Task<ResponseOkHandler<T>> Delete(X id);
        
    }
}
