using BlazorAppBookXchange.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.EditionList
{
    public partial class EditionList
    {
        [Parameter]
        public LivreModel Selected { get; set; }

    }
}
