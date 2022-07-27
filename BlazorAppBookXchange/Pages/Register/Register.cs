using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.Register
{
    public partial class Register
    {
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        public MembreModel nouveauMembre { get; set; } = new MembreModel();

        public async Task SubmitForm()
        {
            ConnectedMember mm = await _requester.Post<MembreModel, ConnectedMember>("membre/register", nouveauMembre);

            if (mm != null)
            {
                if (mm.Token is null)
                {
                    _navigationManager.NavigateTo("/");
                    return;
                }
                await _accountManager.SetToken("token", mm.Token);
                await _accountManager.SetIdMembre("idMembre", mm.IdMembre);
                await _accountManager.SetPseudo("pseudo", mm.Pseudo);
                await _accountManager.checkIfTokenStored();
                int idConnectedMember = mm.IdMembre;

                _navigationManager.NavigateTo($"/ProfilMembre");
                //_navigationManager.NavigateTo($"/memberprofile/{idConnectedMember}");

            }
            else
            {
                _navigationManager.NavigateTo("/register");
            }
        }
    }
}
