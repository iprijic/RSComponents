using Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Blazor.Web.Pages
{
    public class Page : ViewElement
    {
        private Page _parentPage;
        private Layout _layout;
        private ViewContent content;
    }
}
