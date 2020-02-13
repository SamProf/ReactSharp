using System;
using System.Collections.Generic;
using System.Text;

namespace ReactSharp.Demo
{
    public class App : ReactComponent
    {
        private const string code1 = @"


  public class Counter : ReactComponent
    {
        private int value = 0;

        void Increment()
        {
            SetState(() => { value++; });
        }

        void Decrement()
        {
            SetState(() => { value--; });
        }


        public override object Render()
        {
            return new ReactElement($@""
<div>
    <h4>Counter value: {value}</h4>
    <p>
        <button type='button' class='btn btn-primary' onclick='{new Action(Increment)}'>Increment</button>
        <button type='button' class='btn btn-primary' onclick='{new Action((Decrement))}'>Decrement</button>
    </p>
</div>
"");
        }
    }


";


        public override object Render()
        {
            return new ReactElement($@"
<Fragment>    
    <Code Code='{code1}'></Code>    

    <p style='padding-top: 20px;'> 
        <Counter />
    </p>    

 

    <hr class='featurette-divider' />

    <div class='row featurette'>
      <div class='col-md-12'>
        <h2 class='featurette-heading'>Why?</h2>
        <p class='lead'>Just for fun. But maybe this technology can be make greater value. If you like it - please <a href='https://github.com/SamProf/ReactSharp' target='_blank'>put star on GitHub</a></p>
      </div>      
    </div>


    <hr class='featurette-divider' />

    <div class='row featurette'>
      <div class='col-md-12'>
        <h2 class='featurette-heading'>More examples</h2>
        <p class='lead'>Most of this page rendered and works on BlazorSharp. You can check more  examples here: <a href='https://github.com/SamProf/ReactSharp/ReactSharp/ReactSharp.Demo' target='_blank'>ReactSharp.Demo</a></p>
      </div>      
    </div>

    <hr class='featurette-divider' />

    <div class='row featurette'>
      <div class='col-md-12'>
        <h2 class='featurette-heading'>ReactSharp inspired by React.js and PReact.js and Blazor</h2>
        <p class='lead'>Razor is really good, but a huge number of libraries go by React way.</p>
      </div>      
    </div>

    <hr class='featurette-divider' />


    <div class='row featurette'>
      <div class='col-md-12'>
        <h2 class='featurette-heading'>Power of Virtual DOM - It is NOT string interpolation</h2>
        <p class='lead'>ReactSharp uses FormattableString and compiles into high-performance templates to create Virtual DOM elements</p>
      </div>      
    </div>

  

    <hr class='featurette-divider' />

    <div class='row featurette'>
      <div class='col-md-12'>
        <h2 class='featurette-heading'>Change detection</h2>
        <p class='lead'>After calculating DOM diff - Only changes apply to DOM.</p>
        <p>
            <img src='/dom-changes.gif' style='max-width: 100%;'/>           
        </p>   
      </div>    
    </div>

    <hr class='featurette-divider' />

    <div class='row featurette'>
      <div class='col-md-12'>
        <h2 class='featurette-heading'>One engine - 4 targets</h2>
        <p class='lead'>Now - ReactSharp work inside Blazor. Blazor needs for tranfer js calls and initialization.</p>           
        <p class='lead'>But in plans - create targets for ASP.Net (.NET Framework + SignalR), ASP.Net Core (.NET Core + SignalR) and WASM without Blazor.</p>
      </div>  
    </div>

    <hr class='featurette-divider' />
    
    <div class='row featurette'>
      <div class='col-md-12'>
        <h2 class='featurette-heading'>Do you know about my other projects?</h2>
        <p class='lead'>In my spare time I also engaged in other projects</p>           
        <p class='lead'><a href='https://github.com/SamProf/MatBlazor' target='_blank'>MatBlazor</a> - Material Design components for Blazor</p>
        <p class='lead'><a href='https://blazorfiddle.com/' target='_blank'>BlazorFiddle</a> - Blazor .Net Developer Playground and Code Editor in the Browser</p>
        <p class='lead'><a href='https://www.samprof.com/' target='_blank'>Blog</a> - Sometimes, but not so often I wrote something.</p>
        <p class='lead'><a href='https://twitter.com/SamProf' target='_blank'>@SamProf</a></p>
      </div>  
    </div>

    <hr class='featurette-divider' />    

</Fragment>
");
        }
    }
}