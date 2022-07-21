using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.Register
{
    public partial class Register
    {
        //[Inject]
        //HttpClient Http { get; set; }

        [Inject]
        private ApiRequester _requester { get; set; }

        [Inject]
        private AccountManager accountManager { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        public MembreModel nouveauMembre { get; set; }

        public Register()
        {
            nouveauMembre = new MembreModel();
        }

        public async Task SubmitForm()
        {
            ConnectedMember mm = await _requester.Post<MembreModel, ConnectedMember>("membre/register", nouveauMembre);

            if (mm != null)
            {
                if (mm.Token is null)
                {
                    navigationManager.NavigateTo("/");
                    return;
                }

                await accountManager.SetToken("token", mm.Token);
                await accountManager.SetIdMembre("idMembre", mm.IdMembre);
                await accountManager.SetPseudo("pseudo", mm.Pseudo);
                await accountManager.checkIfTokenStored();
                int idConnectedMember = mm.IdMembre;

                navigationManager.NavigateTo($"/memberprofile/{idConnectedMember}");
            }
            else
            {
                navigationManager.NavigateTo("/register");
            }
        }
    }
}
