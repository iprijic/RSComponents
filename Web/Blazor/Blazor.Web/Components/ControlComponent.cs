using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;

namespace Blazor.Web.Components
{
    public class ControlComponent : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            //builder.OpenElement(0, "nav");
            //builder.AddAttribute(1, "class", "menu");


        }
    }
}
