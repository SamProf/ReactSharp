using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ReactSharp.Demo.ServerApp.Pages
{
    public class BaseIndex : ComponentBase
    {
        protected int Counter;


        public void CreateTemplate()
        {
        }


        string[] s = Enumerable.Range(0, 10).Select(i => i.ToString()).ToArray();


        protected override void OnInitialized()
        {
            base.OnInitialized();
            CreateTemplate();
        }
    }
}