using BlazorAppBookXchange.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Pages.ProfilMembre
{
    public partial class ProfilMembre : ComponentBase
    {
        [Parameter] public ConnectedMember connectedMember { get; set; }
        [Parameter] public string idConnectedMember { get; set; }

        //protected override void OnParametersSet()
        //{
        //    //idConnectedMember = "test";

        //    connectedMember = new ConnectedMember()
        //    {
        //        Pseudo = "vg"
        //    };
        //}
    }
}
