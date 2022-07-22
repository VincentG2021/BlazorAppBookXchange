using Microsoft.AspNetCore.Components;

namespace BlazorAppBookXchange.Components.DetailsComponents
{
    public partial class DetailsLivre
    {
        [Parameter]
        public string Title { get; set; }

        //[Parameter]
        //public int CurrentCount { get; set; }


        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }
        
        [CascadingParameter]
        public string Color { get; set; }
        
        [Parameter]
        public RenderFragment VisitContent { get; set; }

        [Parameter]
        public RenderFragment Greetings { get; set; }


        protected override void OnInitialized()
        {
            Console.WriteLine($"OnInitialized => Title: {Title}");
        }
        protected override void OnParametersSet()
        {
            Console.WriteLine($"OnParameterSet => Title: {Title}");
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Console.WriteLine("This is the first render of the component");
            }
        }
        protected override bool ShouldRender()
        {
            return true;
        }
    }
}
