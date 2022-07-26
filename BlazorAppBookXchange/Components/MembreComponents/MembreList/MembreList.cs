using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Components.MembreComponents.MembreList
{
    public partial class MembreList
    {
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }

        public List<MembreModel> membresList = new List<MembreModel>();
        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await _accountManager.checkIfTokenStored();
            if (!_accountManager.IsConnected && !isTokenPresent)
            {
                _navigationManager.NavigateTo("/login");
                return;
            }
            string Token = await _accountManager.GetToken("token");
            membresList = await _requester.Get<List<MembreModel>>("membre/GetMembreList", Token);
        }
    }
}
