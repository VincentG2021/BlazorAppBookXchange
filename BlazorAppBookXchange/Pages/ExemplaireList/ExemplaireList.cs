using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.ExemplaireList
{
    public partial class ExemplaireList
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private ApiRequester _requester { get; set; }

        [Inject]
        private AccountManager accountManager { get; set; }


        public List<ExemplaireModel> exemplaireList = new List<ExemplaireModel>();

        public ExemplaireModel Selected { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await accountManager.checkIfTokenStored();
            if (!accountManager.IsConnected && !isTokenPresent)
            {
                navigationManager.NavigateTo("/login");
                return;
            }
            string Token = await accountManager.GetToken("token");
            int IdMembre = await accountManager.GetMemberId("idMembre");
            exemplaireList = await Http.GetFromJsonAsync<List<ExemplaireModel>>($"https://localhost:7144/BookXchangeAPI/GetMemberExemplaireList?id={IdMembre}");
            //exemplaireList = await _requester.Get<List<ExemplaireModel>>($"https://localhost:7144/BookXchangeAPI/GetMemberExemplaireList/?id={IdMembre}", Token);

        }

        public async Task<List<ExemplaireModel>> GetExemplaires()
        {
            string Token = await accountManager.GetToken("token");
            int IdMembre = await accountManager.GetMemberId("idMembre");

            exemplaireList = await _requester.Get<List<ExemplaireModel>>($"https://localhost:7144/BookXchangeAPI/GetMemberExemplaireList/?id={IdMembre}", Token);
            return exemplaireList;
        }

        public void SetSelected(ExemplaireModel exemplaire)
        {
            Selected = exemplaire;
        }
    }
}
