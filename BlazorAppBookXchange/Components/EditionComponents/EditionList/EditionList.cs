using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.EditionComponents.EditionList
{
    public partial class EditionList
    {
        public List<EditionModel> editionsList { get; set; } = new List<EditionModel>();

        public List<EditionModel> editionsByBookList { get; set; } = new List<EditionModel>();

        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager accountManager { get; set; }


        [Parameter] public LivreModel SelectedBook { get; set; }
        [Parameter] public int IdSelectedBook { get; set; }
        [Parameter] public string? TitreSelectedBook { get; set; }


        //[Parameter] public bool ShowEditions { get; set; }

        public int IdSelectedEdition { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine($"OnInitialized => Title: {TitreSelectedBook}, Selected book: {IdSelectedBook}");

            //GetEditionList AVEC [Autorize("isConnected")] dans Edition.API:

            //bool isTokenPresent = await accountManager.checkIfTokenStored();
            //if (!accountManager.IsConnected && !isTokenPresent)
            //{
            //    navigationManager.NavigateTo("/login");
            //    return;
            //}
            //string Token = await accountManager.GetToken("token");
            ////editionList = await Http.GetFromJsonAsync<List<EditionModel>>("Edition/GetEditionList");
            //editionList = await _requester.Get<List<EditionModel>>("Edition/GetEditionList", Token);

            await accountManager.checkIfTokenStored();
            accountManager.OnChange += StateHasChanged;

            //GetEditionList SANS [Autorize("isConnected")], AVEC [AllowAnonymous] dans Edition.API:
            //editionsList = await _requester.Get<List<EditionModel>>("Edition/GetEditionList");
            IdSelectedBook = SelectedBook.IdLivre;
            StateHasChanged();

            editionsByBookList = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");
            //if (IdSelectedBook != 0)
            //{
            //}
        }
        protected override async Task OnParametersSetAsync()
        {
            IdSelectedBook = SelectedBook.IdLivre;
            editionsByBookList = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");

            Console.WriteLine($"OnParameterSet => Title: {TitreSelectedBook}, Selected book: {IdSelectedBook}");
        }

        //public override Task SetParametersAsync(ParameterView parameters)
        //{
        //    Console.WriteLine($"SetParametersAsync => Title: {TitreSelectedBook}, Selected book: {IdSelectedBook}");
        //    return base.SetParametersAsync(parameters);
        //}

        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine($"OnAfterRender => Title: {TitreSelectedBook}, Selected book: {IdSelectedBook}");

            base.OnAfterRender(firstRender);
        }

        protected override bool ShouldRender()
        {
            Console.WriteLine($"ShouldRender => Title: {TitreSelectedBook}, Selected book: {IdSelectedBook}");
            return true; 
        }

        // Appel via boutton si refresh not working
        private async Task<List<EditionModel>> ShowEditionsByBook()
        {
            return editionsByBookList = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");

            //await OnSelectedBook.InvokeAsync(currentCount);
            //StateHasChanged();
        }

        //public async Task<IEnumerable<EditionModel>> GetSelectedBookEditions(LivreModel book)
        //{

        //    editionsByBookList = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");
        //    return editionsByBookList;
        //}


        // Pour selection d'une edition
        public void SetSelectedEdition(int IdEdition)
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
