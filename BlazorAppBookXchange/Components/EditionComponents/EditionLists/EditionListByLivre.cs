using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.EditionComponents.EditionLists
{
    public partial class EditionListByLivre
    {
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }

        public RenderFragment ChildContent { get; set; }
        public string Text { get; set; }

        [Parameter] public LivreModel SelectedBook { get; set; }
        [Parameter] public int IdSelectedBook { get; set; }
        [Parameter] public string? TitreSelectedBook { get; set; }
        [Parameter] public EditionModel SelectedEdition { get; set; }
        [Parameter] public int IdSelectedEdition { get; set; }

        //public List<EditionModel> editionsList { get; set; } = new List<EditionModel>();
        public List<EditionModel> editionsByLivre { get; set; } = new List<EditionModel>();
        public List<ExemplaireModel> exemplairesByEdition { get; set; } = new List<ExemplaireModel>();
        [Parameter] public bool ShowExemplaires { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await _accountManager.checkIfTokenStored();
            if (!_accountManager.IsConnected && !isTokenPresent)
            {
                _navigationManager.NavigateTo("/login");
                return;
            }

            IdSelectedBook = SelectedBook.IdLivre;
            StateHasChanged();
            
            await LoadData();

            //Console.WriteLine($"OnInitialized => Title: {TitreSelectedBook}, Selected book: {IdSelectedBook}");

            //GetEditionList AVEC [Autorize("isConnected")] dans Edition.API:

            //bool isTokenPresent = await accountManager.checkIfTokenStored();
            //if (!accountManager.IsConnected && !isTokenPresent)
            //{
            //    navigationManager.NavigateTo("/login");
            //    return;
            //}
            //string token = await accountManager.GetToken("token");
            ////editionList = await Http.GetFromJsonAsync<List<EditionModel>>("Edition/GetEditionList");
            //editionList = await _requester.Get<List<EditionModel>>("Edition/GetEditionList", token);

            //GetEditionList SANS [Autorize("isConnected")], AVEC [AllowAnonymous] dans Edition.API:
            //editionsList = await _requester.Get<List<EditionModel>>("Edition/GetEditionList");
            //editionsByLivre = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");
            await _accountManager.checkIfTokenStored();
            _accountManager.OnChange += StateHasChanged;
        }

        private async Task LoadData()
        {
            await GetEditionsByLivre();
            StateHasChanged();
        }

        public async Task<List<EditionModel>> GetEditionsByLivre()
        {
            await _accountManager.checkIfTokenStored();
            string token = await _accountManager.GetToken("token");
            editionsByLivre = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}", token); 
            return editionsByLivre;
        }

        protected override async Task OnParametersSetAsync()
        {
            IdSelectedBook = SelectedBook.IdLivre;
            await GetEditionsByLivre();
        }

        // Appel via boutton si refresh not working
        //private async Task<List<EditionModel>> ShowEditionsByLivre()
        //{
        //    return editionsByLivre = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");

            //await OnSelectedBook.InvokeAsync();
            //StateHasChanged();
        //}

        //public async Task<IEnumerable<EditionModel>> GetSelectedBookEditions(LivreModel book)
        //{

        //    editionsByBookList = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");
        //    return editionsByBookList;
        //}

        // Pour selection d'une edition
        public void SetSelected(EditionModel edition)
        {
            IdSelectedEdition = edition.IdEdition;
            edition.IsRowExpanded = !edition.IsRowExpanded;
            StateHasChanged();
        }
        public async Task<List<ExemplaireModel>> ShowExemplairesByEdition(EditionModel edition)
        {
            string token = await _accountManager.GetToken("token");
            exemplairesByEdition = await _requester.Get<List<ExemplaireModel>>($"Exemplaire/GetExemplaireListByLivre/{IdSelectedEdition}", token);
            return exemplairesByEdition;
        }

        public void Dispose()
        {
            _accountManager.OnChange -= StateHasChanged;
        }
    }
}
