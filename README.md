# React#

## ReactSharp - A C# library for building web user interfaces
[![NuGet](https://img.shields.io/nuget/v/ReactSharp.svg)](https://www.nuget.org/packages/ReactSharp.Blazor/)
[![Gitter](https://badges.gitter.im/ReactSharp/community.svg)](https://gitter.im/MatBlazor/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
[![GitHub Stars](https://img.shields.io/github/stars/SamProf/ReactSharp.svg)](https://github.com/SamProf/ReactSharp/stargazers)
[![GitHub Issues](https://img.shields.io/github/issues/SamProf/ReactSharp.svg)](https://github.com/SamProf/ReactSharp/issues)
[![Live Demo](https://img.shields.io/badge/demo-online-green.svg)](https://reactsharp.samprof.com)
[![MIT](https://img.shields.io/github/license/SamProf/ReactSharp.svg)](LICENSE)
[![Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_SM.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=9XT68N2VKWTPE&source=url)
[![Patreon](https://img.shields.io/badge/Patreon-donation-blue)](https://www.patreon.com/SamProf)


## Demo and Documentation
- [http://reactsharp.samprof.com](http://reactsharp.samprof.com)


## Examples

### Counter.cs
```csharp
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
            return new ReactElement($@"
<div>
    <h4>Counter value: {value}</h4>
    <p>
        <button type='button' class='btn btn-primary' onclick='{new Action(Increment)}'>Increment</button>
        <button type='button' class='btn btn-primary' onclick='{new Action((Decrement))}'>Decrement</button>
    </p>
</div>
");
        }
    }
```

### App.cs
```csharp
 public class App : ReactComponent
    {
        public override object Render()
        {
            return new ReactElement($@"
<Fragment>

    <p style='padding-top: 20px;'> 
        <Counter />
    </p>

</Fragment>
");
        }
    }
```


### Index.razor

```html
<ReactSharpBlazor Prerender="true" Element="@_reactElement"></ReactSharpBlazor>

@code
{
    ReactElement _reactElement = new ReactElement($@"<App/>");
}
```


### Todo.cs [In action](http://reactsharp.samprof.com/Todo)
```csharp
    public class Todo : ReactComponent
    {
        List<string> items = Enumerable.Range(0, 10).Select(i => i.ToString()).ToList();
        

        void Add()
        {
            SetState(() => { this.items.Add(Guid.NewGuid().ToString()); });
        }


        void Remove(string item)
        {
            SetState(() => { this.items.Remove(item); });
        }


        public override object Render()
        {
            return new ReactElement($@"
<div>
    <h4>Todo: {items.Count}</h4>
    <p>
        <button type='button' class='btn btn-primary' onclick='{new Action((Add))}'>Add item</button>
    </p>
    {items.Select(i => new ReactElement($"<div>Task - {i} <button onclick='{new Action(() => Remove(i))}'>X</button></div>"))}
   
</div>
");
        }
    }
```





## News

### ReactSharp 1.0.0
- First public release


## Prerequisites

Don't know what Blazor is? Read [here](https://github.com/aspnet/Blazor)

Complete all Blazor dependencies.

- .NET Core 3.1
- Visual Studio 2019 with the ASP.NET and web development workload selected.

## Installation 

Latest version in here:  [![NuGet](https://img.shields.io/nuget/v/ReactSharp.Blazor.svg)](https://www.nuget.org/packages/ReactSharp.Blazor/)


To Install 

```
Install-Package ReactSharp
Install-Package ReactSharp.Blazor
```

For client-side and server-side Blazor - add script section to index.html or _Host.cshtml (head section) 

```html
<script src="_content/ReactSharp.Blazor/react-sharp.js"></script>
```


## Questions

For *how-to* questions and other non-issues, for now you can use issues or you can use [![Gitter](https://badges.gitter.im/MatBlazor/community.svg)](https://gitter.im/MatBlazor/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).


## Contributing
We'd greatly appreciate any contribution you make. :)

## Sponsors & Backers
ReactSharp does not run under the umbrella of any company or anything like that.
It is an independent project created in spare time.

If you think that this project helped you or your company in any way, you can consider becoming a backer/sponsor.
- [PayPal](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=9XT68N2VKWTPE&source=url)
- [Patreon](https://www.patreon.com/SamProf)



## License

This project is licensed under the terms of the [MIT license](LICENSE).

## Thank you
- [Blazor](https://blazor.net)

