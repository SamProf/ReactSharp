using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ReactSharp.Blazor
{
    public class BaseReactSharpBlazor : ComponentBase, IDisposable
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        protected ElementReference Ref { get; set; }
        protected ElementReference PrerenderRef { get; set; }

        [Parameter]
        public ReactElement Element { get; set; }
        
        
        [Parameter]
        public bool Prerender { get; set; }


        protected ReactRuntime runtime;
        private DotNetObjectReference<BaseReactSharpBlazor> dotNetObjectReference;

        [JSInvokableAttribute]
        public void HandleEvent(long id, string eventName)
        {
            runtime.HandleEvent(id, eventName);
        }
        
        
        ReactRendererDOMJson dom = new ReactRendererDOMJson();

        public BaseReactSharpBlazor()
        {
            runtime = new ReactRuntime()
            {
                Step = () =>
                {
                    InvokeAsync(async () =>
                    {
                        try
                        {
                            //var dom = new ReactRendererDOMJson();
                            var renderer = new ReactRenderer(runtime);
                            runtime.Root = renderer.Render(Element, runtime.Root, dom);
                            await JsRuntime.InvokeAsync<object>("reactSharp.renderJsonString", Ref,
                                dom.StringWriter.ToString());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    });
                }
            };
        }


        protected string prerenderData;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (Prerender)
            {
                prerenderData = DoPrerender();
            }
        }


        protected string DoPrerender()
        {
            try
            {
                var xml = new XElement("Template");
                var dom = new ReactRendererXElement(xml);
                var renderer = new ReactRenderer(null);
                renderer.Render(Element, null, dom);
                var sb = new StringBuilder();
                foreach (var xmlNode in xml.Nodes())
                {
                    sb.Append(xmlNode.ToString());
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                this.dotNetObjectReference = DotNetObjectReference.Create(this);
                await JsRuntime.InvokeAsync<object>("reactSharp.init", Ref, dotNetObjectReference, PrerenderRef);
                runtime.Start();
            }
        }


        public void Dispose()
        {
            dotNetObjectReference?.Dispose();
        }
    }
}