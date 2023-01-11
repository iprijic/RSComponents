using Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Blazor.Web.Pages
{
    public class Layout : ViewElement
    {
        private Page pageParent;
        IEnumerable<ViewContent> contents;
    }
}
