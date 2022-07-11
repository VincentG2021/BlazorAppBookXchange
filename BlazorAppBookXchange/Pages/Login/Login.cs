using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.Login
{
    public partial class Login
    {
        [Inject]
        private IJSRuntime _js { get; set; }

        [Inject]
        private ApiRequester _requester { get; set; }

        [Inject]
        HttpClient Http { get; set; }
        
        [Inject]
        private NavigationManager navigationManager { get; set; }

        public LoginMembreModel membreLogin { get; set; }
        public string Token { get; set; }

        public Login()
        {
            membreLogin = new LoginMembreModel();
        }

        public async Task SubmitForm()
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

                SetToken(mm.Token);
                navigationManager.NavigateTo("/booklist");
            }
            else
            {
                navigationManager.NavigateTo("/memberlist");
            }


        }


        public async void SetToken(string Token)
        {
            //Token = membreLogin.Token;
            await _js.InvokeVoidAsync("localStorage.setItem", "token", Token);
            // return Token;
        }

        //protected override async Task OnInitializedAsync()
        //{
        //    membreLogin = await Http.GetFromJsonAsync<MembreModel>("https://localhost:7144/api/BookXchangeAPI/Login/auth");
        //}
    }
}

