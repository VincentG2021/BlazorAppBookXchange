using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorAppBookXchange.Tools
{
    public class AccountManager
    {
        [Inject]
        private ILocalStorageService _storageService { get; set; }


        private bool _isConnected;
        public event Action OnChange;

        public bool IsConnected
        {
            get { return _isConnected; }
            set {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    NotifyStateChanged();
                }
            }
        }
        private void NotifyStateChanged() => OnChange?.Invoke();

        public AccountManager(ILocalStorageService localStorageService)
        {
            _storageService = localStorageService;
        }

        public async Task SetToken(string Key, string Token)
        {
            await _storageService.SetItem<string>(Key, Token);
        }

        public async Task<bool> checkIfTokenStored()
        {
            string storedToken = await _storageService.GetItem<string>("token");
            bool isTokenPresent = (storedToken is null) ? false : true;
            if (isTokenPresent == true)
            {
                IsConnected = true;
            }
            return isTokenPresent;
        }

        public async Task<string> GetToken(string Key)
        {
            string Token = await _storageService.GetItem<string>(Key);
            return Token;
        }

        public async Task RemoveToken(string Key)
        {
            await _storageService.removeItem(Key);
            IsConnected = false;

        }
    }
}
