using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace BlazorAppBookXchange.Components.ExemplaireComponents.ExemplaireLists
{
    public partial class ExemplaireListByMembre
    {
        [Inject] private AccountManager _accountManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Parameter] public int IdMembre { get; set; }

        public List<ExemplaireModel> exemplaireList = new List<ExemplaireModel>();
        public ExemplaireModel? Selected { get; set; }
        public ExemplaireModel? SelectedExemplaireToEdit { get; set; }

        private ExemplaireModel _itemToDelete = new ExemplaireModel();
        public bool DeleteDialogOpen { get; set; } // Cette propriété détermine si ModalDialog doit s'acfficher

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

            //string Token = await accountManager.GetToken("token");
            //IdMembre = await accountManager.GetMemberId("idMembre");
            //exemplaireList = await Http.GetFromJsonAsync<List<ExemplaireModel>>($"Exemplaire/GetExemplaireListByMember/{IdMembre}");
            //exemplaireList = await _requester.Get<List<ExemplaireModel>>($"Exemplaire/GetExemplaireListByMember/{IdMembre}", Token);
        }

        private async Task LoadData()
        {
            await GetExemplairesByMembre();
            StateHasChanged();
        }

        public async Task<List<ExemplaireModel>> GetExemplairesByMembre()
        {
            await _accountManager.checkIfTokenStored();
            string token = await _accountManager.GetToken("token");
            IdMembre = await _accountManager.GetMemberId("idMembre");

            exemplaireList = await _requester.Get<List<ExemplaireModel>>($"Exemplaire/GetExemplaireListByMember/{IdMembre}", token);
            return exemplaireList;
        }

        public void SetSelected(ExemplaireModel exemplaire)
        {
            Selected = exemplaire;
            exemplaire.IsRowExpanded = !exemplaire.IsRowExpanded;
        }
        
        public void GoToCreateExemplaire()
        {
            _navigationManager.NavigateTo("/createExemplaire");
        }

        public void SetSelectedExemplaireToEdit(ExemplaireModel exemplaire)
        {
            SelectedExemplaireToEdit = exemplaire;
            _navigationManager.NavigateTo($"/editExemplaire/{SelectedExemplaireToEdit.IdExemplaire}");

        }

        //public void GoToExemplaireEdit(int id)
        //{
        //    //_navigationManager.NavigateTo("/editlivre");
        //}

        private void OpenDeleteDialog(ExemplaireModel exemplaire)
        {
            _itemToDelete = exemplaire;
            DeleteDialogOpen = true;
        }

        private async Task OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                await _accountManager.checkIfTokenStored();
                string token = await _accountManager.GetToken("token");

                Console.WriteLine($"Elément supprimé: {_itemToDelete.Titre} ayant l'id: {_itemToDelete.IdExemplaire}");
                bool result = await _requester.Delete<bool>($"exemplaire/DeleteExemplaire/", _itemToDelete.IdExemplaire, token);
                Console.WriteLine(result);
                _itemToDelete = new ExemplaireModel();
                await LoadData();
            }

            DeleteDialogOpen = false;
            StateHasChanged();
        }

    }
}
