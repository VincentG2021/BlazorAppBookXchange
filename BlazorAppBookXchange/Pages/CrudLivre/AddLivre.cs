﻿using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Pages.CrudLivre
{
    public partial class AddLivre : ComponentBase
    {
        //[Inject] HttpClient Http { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        public AddLivreModel addLivre { get; set; }
        public string Token { get; set; }

        public AddLivre()
        {
            addLivre = new AddLivreModel();
        }

        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await _accountManager.checkIfTokenStored();
            if (!_accountManager.IsConnected && !isTokenPresent)
            {
                _navigationManager.NavigateTo("/login");
                return;
            }
        }

        public async Task SubmitAddLivreForm()
        {
            string Token = await _accountManager.GetToken("token");

            LivreModel lm = await _requester.Post<AddLivreModel, LivreModel>("Livre/CreateLivre", addLivre, Token);

            if (lm != null)
            {
                if (Token is null)
                {
                    _navigationManager.NavigateTo("/login");
                    return;
                }
                _navigationManager.NavigateTo("/booklist");
            }
            else
            {
                _navigationManager.NavigateTo("/");
            }


        }
    }
}
