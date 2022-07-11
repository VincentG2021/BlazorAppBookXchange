using BlazorAppBookXchange.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.Register
{
    public partial class Register
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        public MembreModel nouveauMembre { get; set; }

        public Register()
        {
            nouveauMembre = new MembreModel();
        }
        public async Task SubmitForm()
        {
            //dataset.Add(nouveauMembre);
            using var response = await Http.PostAsJsonAsync<MembreModel>("https://localhost:7144/api/BookXchangeAPI/RegisterMembre", nouveauMembre);
            MembreModel nouveauMembreOK = await response.Content.ReadFromJsonAsync<MembreModel>();

            //nouveauMembre = await Http.PostAsJsonAsync<MembreModel>("https://localhost:7144/api/BookXchangeAPI/RegisterMembre", nouveauMembre);
            navigationManager.NavigateTo("/memberlist");
        }


    }
}
