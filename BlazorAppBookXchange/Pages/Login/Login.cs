using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.Login
{
    public partial class Login : ComponentBase
    {

        [Inject]
        private ApiRequester _requester { get; set; }

        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private AccountManager accountManager { get; set; }

        public LoginMembreModel membreLogin { get; set; }
        public string Token { get; set; }

        public Login()
        {
            membreLogin = new LoginMembreModel();
        }

        public async Task SubmitLoginForm()
        {
            //using var response = await Http.PostAsJsonAsync<LoginMembreModel>("https://localhost:7144/BookXchangeAPI/Login/auth", membreLogin);

            ConnectedMember mm = await _requester.Post<LoginMembreModel, ConnectedMember>("https://localhost:7144/bookxchangeapi/login/auth", membreLogin);


            //if (response.IsSuccessStatusCode)
            if (mm != null)
            {
                //ConnectedMember mm = await response.Content.ReadFromJsonAsync<ConnectedMember>();

                if (mm.Token is null)
                {
                    navigationManager.NavigateTo("/");
                    return;
                }

                await accountManager.SetToken("token", mm.Token);
                await accountManager.SetIdMembre("idMembre", mm.IdMembre);
                await accountManager.SetPseudo("pseudo", mm.Pseudo);
                
                await accountManager.checkIfTokenStored();

                //SetToken() FONCTIONNEL (set bien le Token au Login, ok avant de mep AccountManager et LocalStorageService)
                //SetToken(mm.Token);
                //navigationManager.NavigateTo("/booklist");

                int idConnectedMember = mm.IdMembre;
                
                navigationManager.NavigateTo($"/memberprofile/{idConnectedMember}");
            }
            else
            {
                navigationManager.NavigateTo("/login");
            }


        }


    }
}

