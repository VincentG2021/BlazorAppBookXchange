using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.EditionComponents.EditionList
{
    public partial class EditionList
    {
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager accountManager { get; set; }

        public List<EditionModel> editionsList = new List<EditionModel>();

        public List<EditionModel> editionsByBookList = new List<EditionModel>();

        [Parameter] public int IdSelectedBook { get; set; }
        [Parameter] public string TitreSelectedBook { get; set; }

        public int IdSelectedEdition { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //AVEC [Autorize("isConnected")] dans EditionApi.API:

            //bool isTokenPresent = await accountManager.checkIfTokenStored();
            //if (!accountManager.IsConnected && !isTokenPresent)
            //{
            //    navigationManager.NavigateTo("/login");
            //    return;
            //}
            //string Token = await accountManager.GetToken("token");
            ////editionList = await Http.GetFromJsonAsync<List<EditionModel>>("Edition/GetEditionList");
            //editionList = await _requester.Get<List<EditionModel>>("Edition/GetEditionList", Token);


            //SANS [Autorize("isConnected")], AVEC [AllowAnonymous] dans BookXchangeBE.API:
            //editionsList = await _requester.Get<List<EditionModel>>("Edition/GetEditionList");


            //if(IdSelectedBook != null)
            //{
            //    editionsByBookList = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");
            //}

            await accountManager.checkIfTokenStored();
            accountManager.OnChange += StateHasChanged;        
        }

        private async Task<List<EditionModel>> ShowEditionsByBook()
        {
                return editionsByBookList = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");

                //await OnSelectedBook.InvokeAsync(currentCount);
                //StateHasChanged();
        }

        public void SetSelected(int IdEdition)
        {
            IdSelectedEdition = IdEdition;
            StateHasChanged();
        }

        public void Dispose()
        {
            accountManager.OnChange -= StateHasChanged;
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
