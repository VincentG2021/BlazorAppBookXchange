using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorAppBookXchange.Tools
{
    public class AccountManager
    {
        [Inject]
        private ILocalStorageService _storageService { get; set; }


        public bool isConnected;

        public AccountManager(ILocalStorageService localStorageService)
        {
            _storageService = localStorageService;
        }

        public async Task SetToken(string Token)
        {
            //Token = membreLogin.Token;
            await _storageService.SetItem<string>("token", Token);
            isConnected = true;
        }

        //public async void GetToken()
        //{
        //    //Token = membreLogin.Token;
        //    await _jsRuntime.InvokeVoidAsync("localStorage.getItem", "token", Token);
        //    // return Token;
        //}
    }
}
