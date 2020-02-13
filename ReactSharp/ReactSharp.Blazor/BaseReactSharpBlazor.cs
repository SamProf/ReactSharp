using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ReactSharp.Blazor
{
    public class BaseReactSharpBlazor : ComponentBase
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        protected ElementReference Ref { get; set; }

        [Parameter]
        public ReactElement Element { get; set; }


        protected ReactRenderer _renderer;
        private Timer timer;

        public BaseReactSharpBlazor()
        {
            _renderer = new ReactRenderer();
        }


        public async Task RenderAsync()
        {
            var textWriter = new StringWriter();
            var dom = new ReactRendererDOMJson(textWriter);
            _renderer.Render(Element, dom);
            await JsRuntime.InvokeAsync<object>("reactSharp.renderJsonString", Ref, textWriter.ToString());
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await JsRuntime.InvokeAsync<object>("reactSharp.init", Ref);
                await RenderAsync();
                this.timer = new Timer((_) => { this.InvokeAsync(async () => { await this.RenderAsync(); }); }, null,
                    TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            }
        }
    }
}