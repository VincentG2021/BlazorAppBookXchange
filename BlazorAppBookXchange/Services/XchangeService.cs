using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Services
{
    public class XchangeService
    {
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        public MembreModel MembreDetails { get; set; } = new MembreModel();

        //public async Task<bool> CheckIfTokenStored()
        //{
        //    bool isTokenPresent = await _accountManager.checkIfTokenStored();
        //    if (!_accountManager.IsConnected && !isTokenPresent)
        //    {
        //        _navigationManager.NavigateTo("/login");
        //        return isTokenPresent;
        //    }
        //    return isTokenPresent;
        //}

        //public async Task<MembreModel> GetMembreDetails(int id)
        //{
        //    //await CheckIfTokenStored();
        //    string Token = await _accountManager.GetToken("token");
        //    int IdConnectedMembre = await _accountManager.GetMemberId("idMembre");
        //    MembreDetails = await _requester.Get<MembreModel>($"Membre/GetMembreById/{id}", Token);
        //    return MembreDetails;
        //}

    }
}
