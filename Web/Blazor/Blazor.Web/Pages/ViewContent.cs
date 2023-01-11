using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazor.Web.Pages
{
    public class ViewContent : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            //builder.OpenElement(0, "nav");
            //builder.AddAttribute(1, "class", "menu");
        }
    }
}
