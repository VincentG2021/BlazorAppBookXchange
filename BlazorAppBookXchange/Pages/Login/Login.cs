using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.Login
{
    public partial class Login : ComponentBase
    {
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }

        public LoginMembreModel membreLogin { get; set; } = new LoginMembreModel();
        public string Token { get; set; }

        public async Task SubmitLoginForm()
        {
            //using var response = await Http.PostAsJsonAsync<LoginMembreModel>("Membre/Login", membreLogin);

            ConnectedMember mm = await _requester.Post<LoginMembreModel, ConnectedMember>("membre/login", membreLogin);


            //if (response.IsSuccessStatusCode)
            if (mm != null)
            {
                //ConnectedMember mm = await response.Content.ReadFromJsonAsync<ConnectedMember>();

                if (mm.Token is null)
                {
                    _navigationManager.NavigateTo("/");
                    return;
                }

                await _accountManager.SetToken("token", mm.Token);
                await _accountManager.SetIdMembre("idMembre", mm.IdMembre);
                await _accountManager.SetPseudo("pseudo", mm.Pseudo);
                
                await _accountManager.checkIfTokenStored();

                //SetToken() FONCTIONNEL (set bien le Token au Login, ok avant de mep AccountManager et LocalStorageService)
                //SetToken(mm.Token);
                //navigationManager.NavigateTo("/booklist");

                int idConnectedMember = mm.IdMembre;

                _navigationManager.NavigateTo($"/profilmembre");
                //_navigationManager.NavigateTo($"/profilmembre/{idConnectedMember}");
            }
            else
            {
                _navigationManager.NavigateTo("/login");
            }


        }


    }
}

