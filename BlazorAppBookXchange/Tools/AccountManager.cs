using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorAppBookXchange.Tools
{
    public class AccountManager
    {
        [Inject]
        private ILocalStorageService _storageService { get; set; }


        private bool _isConnected;
        private bool _isTokenPresent;
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

        public bool IsTokenPresent
        {
            get { return _isTokenPresent; }
            set {
                if(_isTokenPresent != value)
                {
                    _isTokenPresent = value;
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
            await _storageService.SetItem(Key, Token);
        }

        public async Task SetIdMembre(string Key, int IdMembre)
        {
            await _storageService.SetItem(Key, IdMembre);
        }

        public async Task SetPseudo(string Key, string Pseudo)
        {
            await _storageService.SetItem(Key, Pseudo);
        }

        public async Task<bool> checkIfTokenStored()
        {
            string storedToken = await _storageService.GetItem<string>("token");
            IsTokenPresent = (storedToken is null) ? false : true;
            if (IsTokenPresent == true)
            {
                IsConnected = true;
            }
            return IsTokenPresent;
        }

        public async Task<string> GetToken(string Key)
        {
            string Token = await _storageService.GetItem<string>(Key);
            return Token;
        }

        public async Task<string> GetPseudo(string Key)
        {
            string Pseudo = await _storageService.GetItem<string>(Key);
            return Pseudo;
        }

        public async Task<int> GetMemberId(string Key)
        {
            int IdMembre = await _storageService.GetItem<int>(Key);
            return IdMembre;
        }

        public async Task RemoveToken(string Key)
        {
            await _storageService.removeItem(Key);
            IsTokenPresent = false;
            IsConnected = false;

        }
    }
}
