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
            var render = new ReactRenderer(null);
            var element = new ReactElement($@"


<b class='test1'>


    <div>eeee</div>
    <div>eeee</div>
    <div>eeee {222}</div>

</b>




");
            var dom = new ReactRendererDOMJson();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100; i++)
            {
                render.Render(element, null, dom);
            }

            Console.WriteLine(sw.Elapsed);
            Console.WriteLine(dom.StringWriter.ToString().Length);
            Console.WriteLine(dom.StringWriter.ToString());
        }



        protected void TestClick()
        {
            
        }
        
        
        [Test]
        public void TestParser()
        {
            var template = ReactElementTemplateParser.Get($@"
<div>


<div class='{123}' click='{new Action(()=>{TestClick();})}'>

{123} 



- {234}

 <span>My</span> <span>My2</span>

</div>

</div>");

            Console.WriteLine(template);
        }
    }
}