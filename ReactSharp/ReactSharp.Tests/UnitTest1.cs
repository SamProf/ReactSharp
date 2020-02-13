using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using NUnit.Framework;

namespace ReactSharp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestRenderer()
        {
            var render = new ReactRenderer();
            var element = new ReactElement($@"


<b class='test1'>


    <div>eeee</div>
    <div>eeee</div>
    <div>eeee {222}</div>

</b>




");
            var writer = new StringWriter();
            var dom = new ReactRendererDOMJson(writer);
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100; i++)
            {
                render.Render(element, dom);
            }
            Console.WriteLine(sw.Elapsed);
            var c = writer.ToString();
            Console.WriteLine(c.Length);
            Console.WriteLine(c);
            
            
        }
        
        [Test]
        
        public void TestParser()
        {
            var template = ReactElementTemplateParser.Get($@"
<Component1>

<div class='{123}'>

{123} 



- {234}

 <span>My</span> <span>My2</span>

</div>

</Component1>");

            Console.WriteLine(template);
        }
    }
}