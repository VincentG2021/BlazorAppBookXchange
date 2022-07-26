using BlazorAnimate;
using BlazorAppBookXchange.Models;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.ExemplaireComponents.ExemplaireCard
{
    public partial class ExemplaireCard
    {
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ApiRequester _requester { get; set; }
        [Inject] private AccountManager accountManager { get; set; }

        private Animate? animateFlip;

        public List<ExemplaireModel> exemplairesList = new List<ExemplaireModel>();
        //public List<EditionModel> EditionsByBookList { get; set; }
        public ExemplaireModel? SelectedCard { get; set; }
        public int IdSelectedExemplaire { get; set; }
        public string TitreSelectedExemplaire { get; set; }

        //public bool voirEditionsClicked;

        protected override async Task OnInitializedAsync()
        {
            //cardFlipClass = "hideBackCard";

            exemplairesList = await _requester.Get<List<ExemplaireModel>>("Exemplaire/GetExemplaireList");
            await accountManager.checkIfTokenStored();
            accountManager.OnChange += StateHasChanged;
        }

        public void SetSelected(int idExemplaire, string nameExemplaire)
        {
            IdSelectedExemplaire = idExemplaire;
            TitreSelectedExemplaire = nameExemplaire;
        }

        public void VoirEditions(int idBook, string nameBook)
        {
            IdSelectedExemplaire = idBook;
            TitreSelectedExemplaire = nameBook;
            //voirEditionsClicked = true;
        }

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

        public void GoToBookDetails(int id)
        {
            navigationManager.NavigateTo("/");
        }

        public void GoToBookEdit()
        {
            navigationManager.NavigateTo("/counter");
        }
        public void Dispose()
        {
            accountManager.OnChange -= StateHasChanged;
        }

    }
}
