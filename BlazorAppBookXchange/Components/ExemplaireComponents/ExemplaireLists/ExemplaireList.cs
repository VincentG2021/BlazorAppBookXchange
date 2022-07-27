using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.ExemplaireComponents.ExemplaireLists
{
    public partial class ExemplaireList
    {
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }
        [Parameter] public int IdSelectedBook { get; set; }
        [Parameter] public string? TitreSelectedBook { get; set; }
        [Parameter] public int IdSelectedEdition { get; set; }
        [Parameter] public bool ShowExemplaires { get; set; }

        public List<ExemplaireModel> exemplairesList = new List<ExemplaireModel>();

        public List<ExemplaireModel> exemplairesByBookList = new List<ExemplaireModel>();
        public ExemplaireModel? Selected { get; set; }

        private ExemplaireModel _itemToXchange = new ExemplaireModel();
        public bool XchangeDialogOpen { get; set; } // Cette propriété détermine si ModalDialog doit s'acfficher


        protected override async Task OnInitializedAsync()
        {
            //GetExemplaireList AVEC[Autorize("isConnected")] dans Exemplaire.API:


            ////exemplairesList = await Http.GetFromJsonAsync<List<ExemplaireModel>>("Exemplaire/GetExemplaireList");
            exemplairesList = await _requester.Get<List<ExemplaireModel>>("Exemplaire/GetExemplaireList");

            await _accountManager.checkIfTokenStored();
            _accountManager.OnChange += StateHasChanged;

            //GetExemplaireList SANS [Autorize("isConnected")], AVEC [AllowAnonymous] dans Exemplaire.API:
            //exemplairesList = await _requester.Get<List<ExemplaireModel>>("Exemplaire/GetExemplaireList");

            if (ShowExemplaires)
            {
                exemplairesByBookList = await _requester.Get<List<ExemplaireModel>>($"Exemplaire/GetExemplaireListByLivre/{IdSelectedBook}");
            }
        }

        public void SetSelected(ExemplaireModel exemplaire)
        {
            Selected = exemplaire;
            exemplaire.IsRowExpanded = !exemplaire.IsRowExpanded;
        }

        private void OpenXchangeDialog(ExemplaireModel exemplaire)
        {
            _itemToXchange = exemplaire;
            XchangeDialogOpen = true;
        }

        private async Task OnXchangeDialogClose(bool accepted)
        {
            if (accepted)
            {
                bool isTokenPresent = await _accountManager.checkIfTokenStored();
                if (!_accountManager.IsConnected && !isTokenPresent)
                {
                    _navigationManager.NavigateTo("/login");
                    return;
                }
                string Token = await _accountManager.GetToken("token");
                //await _accountManager.checkIfTokenStored();
                //string token = await _accountManager.GetToken("token");

                Console.WriteLine($"Exemplaire demandé au membre ayant l'id {_itemToXchange.IdMembre} : {_itemToXchange.Titre} ayant l'id d'exemplaire: {_itemToXchange.IdExemplaire} ");
                //TODO changer requète pour envoi demande 
                //bool result = await _requester.Delete<bool>($"exemplaire/DeleteExemplaire/", _itemToXchange.IdExemplaire, token);
                //Console.WriteLine(result);
                //_itemToXchange = new ExemplaireModel();
                //await LoadData();
            }

            XchangeDialogOpen = false;
            StateHasChanged();
        }




        //protected override void OnParametersSet()
        //{
        //    Console.WriteLine($"OnParameterSet => IdSelectedBook: {IdSelectedBook}, TitreSelectedBook: {TitreSelectedBook}");
        //}


        // Appel via boutton si refresh not working
        //private async Task<List<ExemplaireModel>> ShowExemplairesByLivre()
        //{
        //    return exemplairesByBookList = await _requester.Get<List<ExemplaireModel>>($"Exemplaire/GetExemplaireByLivre/{IdSelectedBook}");
        //    //await OnSelectedBook.InvokeAsync(currentCount);
        //    //StateHasChanged();
        //}

        //public void SetSelected(int IdLivre)
        //{
        //    IdSelectedBook = IdLivre;
        //    StateHasChanged();
        //}

        public void Dispose()
        {
            _accountManager.OnChange -= StateHasChanged;
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
