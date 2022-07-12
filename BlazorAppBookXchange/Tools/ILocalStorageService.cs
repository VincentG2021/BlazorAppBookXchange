namespace BlazorAppBookXchange.Tools
{
    public interface ILocalStorageService
    {
        Task<T> GetItem<T>(string key);

        Task SetItem<T>(string key, T value);

        Task removeItem(string key);
    }
}