using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.EditionList
{
    public partial class EditionList
    {

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private ApiRequester _requester { get; set; }

        [Inject]
        private AccountManager accountManager { get; set; }


        public List<EditionModel> editionsList = new List<EditionModel>();

        [Parameter]
        public LivreModel Selected { get; set; }
        public int IdLivre { get; set; }


        protected override async Task OnInitializedAsync()
        {
            //AVEC [Autorize("isConnected")] dans BookXchangeBE.API:

            //bool isTokenPresent = await accountManager.checkIfTokenStored();
            //if (!accountManager.IsConnected && !isTokenPresent)
            //{
            //    navigationManager.NavigateTo("/login");
            //    return;
            //}
            //string Token = await accountManager.GetToken("token");
            ////editionList = await Http.GetFromJsonAsync<List<EditionModel>>("https://localhost:7144/EditionApi/GetEditionList");
            //editionList = await _requester.Get<List<EditionModel>>("https://localhost:7144/EditionApi/GetEditionList", Token);


            //SANS [Autorize("isConnected")], AVEC [AllowAnonymous] dans BookXchangeBE.API:
            editionsList = await _requester.Get<List<EditionModel>>("https://localhost:7144/EditionApi/GetEditionList");


            if(Selected != null)
            {
                IdLivre = Selected.IdLivre;
                editionsList = await _requester.Get<List<EditionModel>>($"https://localhost:7144/EditionApi/GetEditionByLivre/{IdLivre}");
            }



            await accountManager.checkIfTokenStored();

            accountManager.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            accountManager.OnChange -= StateHasChanged;
        }


        public void SetSelected(LivreModel book)
        {
            Selected = book;
        }

        //public void GoToBookDetails(int id)
        //{
        //    navigationManager.NavigateTo("/");
        //}

        //public void GoToBookEdit()
        //{
        //    navigationManager.NavigateTo("/counter");
        //}


    }
}
