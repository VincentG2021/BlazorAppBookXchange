using BlazorAnimate;
using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Services;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.ExemplaireComponents.ExemplaireCard
{
    public partial class ExemplaireCard
    {
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager _accountManager { get; set; }
        [Inject] private XchangeService _xchangeService { get; set; }

        private Animate? animateFlip;

        public List<ExemplaireModel> exemplairesList = new List<ExemplaireModel>();
        //public List<EditionModel> EditionsByBookList { get; set; }
        public ExemplaireModel? SelectedCard { get; set; }
        public int IdSelectedExemplaire { get; set; }
        public string TitreSelectedExemplaire { get; set; }

        public MembreModel MembreDetails { get; set; } = new MembreModel();

        protected override async Task OnInitializedAsync()
        {
            //cardFlipClass = "hideBackCard";

            exemplairesList = await _requester.Get<List<ExemplaireModel>>("Exemplaire/GetExemplaireList");
            await _accountManager.checkIfTokenStored();
            _accountManager.OnChange += StateHasChanged;
        }

        //public void SetSelected(int idExemplaire, string nameExemplaire)
        //{
        //    IdSelectedExemplaire = idExemplaire;
        //    TitreSelectedExemplaire = nameExemplaire;
        //}


        //public async Task<IEnumerable<EditionModel>> GetSelectedBookEditions(LivreModel book)
        //{
        //    if (IdSelectedBook == 0)
        //    {
        //        return new List<EditionModel>();
        //    }
        //    EditionsByBookList = await _requester.Get<List<EditionModel>>($"Edition/GetEditionByLivre/{IdSelectedBook}");
        //    return EditionsByBookList;

        //}
        private void AnimationFlip()
        {
            animateFlip?.Run();
        }

        public void SetSelectedCard(ExemplaireModel exemplaire)
        {
            SelectedCard = exemplaire;
            exemplaire.IsCardFlipped = !exemplaire.IsCardFlipped;
            AnimationFlip();
        }
        public async Task<MembreModel> AskXchange(ExemplaireModel exemplaire)
        {
            Console.WriteLine(exemplaire.IdMembre);
            IdSelectedExemplaire = exemplaire.IdExemplaire;
            TitreSelectedExemplaire = exemplaire.Titre;
            MembreDetails = await /*_xchangeService.*/GetMembreDetails(exemplaire.IdMembre);
            Console.WriteLine(MembreDetails.Email);
            return MembreDetails;
        }

        public async Task<MembreModel> GetMembreDetails(int id)
        {
            //await CheckIfTokenStored();
            string Token = await _accountManager.GetToken("token");
            int IdConnectedMembre = await _accountManager.GetMemberId("idMembre");
            MembreDetails = await _requester.Get<MembreModel>($"Membre/GetMembreById/{id}", Token);
            return MembreDetails;
        }


        public void GoToBookDetails(int id)
        {
            _navigationManager.NavigateTo("/");
        }

        public void GoToBookEdit()
        {
            _navigationManager.NavigateTo("/counter");
        }
        public void Dispose()
        {
            _accountManager.OnChange -= StateHasChanged;
        }

    }
}
