﻿using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.ExemplaireCardPage
{
    public partial class ExemplaireCardPage
    {
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager accountManager { get; set; }

        public List<ExemplaireModel> exemplairesList = new List<ExemplaireModel>();

        public List<ExemplaireModel> exemplairesByEdition = new List<ExemplaireModel>();

        public ExemplaireModel? SelectedCard { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public int IdSelectedEdition { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //AVEC [Autorize("isConnected")] dans ExemplaireApi:

            //bool isTokenPresent = await accountManager.checkIfTokenStored();
            //if (!accountManager.IsConnected && !isTokenPresent)
            //{
            //    navigationManager.NavigateTo("/login");
            //    return;
            //}
            //string Token = await accountManager.GetToken("token");
            ////exemplairesList = await Http.GetFromJsonAsync<List<ExemplaireModel>>("Exemplaire/GetAllExemplairesList");
            //exemplairesList = await _requester.Get<List<ExemplaireModel>>("Exemplaire/GetAllExemplairesList", Token);


            //SANS[Autorize("isConnected")], AVEC[AllowAnonymous] dans BookXchangeBE.API:
            //exemplairesList = await _requester.Get<List<ExemplaireModel>>("Exemplaire/GetAllExemplairesList");
            //pidSelectedEdition = IdSelectedEdition;

            if (IdSelectedEdition != 0)
            {
                exemplairesByEdition = await _requester.Get<List<ExemplaireModel>>($"Exemplaire/GetExemplaireListbyEdition/{IdSelectedEdition}");
            }

            await accountManager.checkIfTokenStored();

            accountManager.OnChange += StateHasChanged;
        }

        public void SetSelectedCard(ExemplaireModel exemplaire)
        {
            SelectedCard = exemplaire;
            exemplaire.IsCardFlipped = !exemplaire.IsCardFlipped;
        }


    }
}
