using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.ExemplaireComponents.ExemplaireLists
{
    public partial class ExemplaireListByLivre
    {
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }

        //public List<ExemplaireModel> exemplairesList = new List<ExemplaireModel>();

        public List<ExemplaireModel> exemplairesByLivre = new List<ExemplaireModel>();
        [Parameter] public LivreModel SelectedBook { get; set; }
        [Parameter] public int IdSelectedBook { get; set; }
        [Parameter] public string? TitreSelectedBook { get; set; }
        protected override async Task OnInitializedAsync()
        {
            bool isTokenPresent = await _accountManager.checkIfTokenStored();
            if (!_accountManager.IsConnected && !isTokenPresent)
            {
                _navigationManager.NavigateTo("/login");
                return;
            }

            await LoadData();

            await _accountManager.checkIfTokenStored();
            _accountManager.OnChange += StateHasChanged;

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
        }

        private async Task LoadData()
        {
            await GetExemplairesByLivre();
            StateHasChanged();
        }

        public async Task<List<ExemplaireModel>> GetExemplairesByLivre()
        {
            await _accountManager.checkIfTokenStored();
            string Token = await _accountManager.GetToken("token");
            exemplairesByLivre = await _requester.Get<List<ExemplaireModel>>($"Exemplaire/GetExemplaireByLivre/{IdSelectedBook}", Token);
            return exemplairesByLivre;
        }

        protected override async Task OnParametersSetAsync()
        {
            IdSelectedBook = SelectedBook.IdLivre;
            await GetExemplairesByLivre();
        }

        public void Dispose()
        {
            _accountManager.OnChange -= StateHasChanged;
        }
    }
}
