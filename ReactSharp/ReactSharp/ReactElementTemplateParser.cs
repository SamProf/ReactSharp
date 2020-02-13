using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Resolvers;

namespace ReactSharp
{
    public static class ReactElementTemplateParser
    {
        private static Dictionary<string, ReactElementTemplate> dic = new Dictionary<string, ReactElementTemplate>();


        static Regex exactAttributeRegex = new Regex("^{(?<index>[0-9]+)}$", RegexOptions.Compiled);
        static Regex containsAttributeRegex = new Regex("{(?<index>[0-9]+)}", RegexOptions.Compiled);


        private static Dictionary<string, Type> components;
        private static object syncObj = new object();

        private static Type ResolveComponent(string tagName)
        {
            if (components == null)
            {
                lock (syncObj)
                {
                    if (components == null)
                    {
                        components = new Dictionary<string, Type>();
                        foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(i => i.GetTypes())
                            .Where(i => i.IsSubclassOf(typeof(ReactComponent))))
                        {
                            components[type.Name] = type;
                            components[type.FullName] = type;
                        }
                    }
                }
            }

            try
            {
                Type t;
                if (!components.TryGetValue(tagName, out t))
                {
                    t = Type.GetType(tagName);
                }

                if (t == null)
                {
                    throw new Exception($"Component {tagName} not found");
                }

                return t;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static ReactElementTemplate ParseElement(XmlReader xmlReader)
        {
            if (xmlReader.NodeType != XmlNodeType.Element)
            {
                throw new Exception();
            }

            var template = new ReactElementTemplate()
            {
            };

            var tagName = xmlReader.Name;
            template.Type = new ReactElementTemplateType()
            {
                Value = tagName,
            };
            if (tagName.Length == 0)
            {
                throw new Exception();
            }

            if (char.IsLower(tagName[0]))
            {
                template.Type.Evaluator = ReactValueEvaluatorFactory.CreateValue(tagName);
            }
            else
            {
                template.Type.Evaluator = ReactValueEvaluatorFactory.CreateValue(ResolveComponent(tagName));
            }

            template.Children = new List<ReactElementTemplateChild>();
            template.Props = new List<ReactElementTemplateProp>();

            if (xmlReader.HasAttributes)
            {
                while (xmlReader.MoveToNextAttribute())
                {
                    var propValue = xmlReader.Value;
                    var propName = xmlReader.Name;

                    var propTemplate = new ReactElementTemplateProp()
                    {
                        Name = propName,
                        Value = propValue,
                    };

                    var m = exactAttributeRegex.Match(propValue);

                    if (m.Success)
                    {
                        var index = int.Parse(m.Groups["index"].Value);
                        propTemplate.Evaluator = ReactValueEvaluatorFactory.CreateByIndex(index);
                    }
                    else if (containsAttributeRegex.IsMatch(propValue))
                    {
                        propTemplate.Evaluator = ReactValueEvaluatorFactory.CreateByFormat(propValue);
                    }
                    else
                    {
                        propTemplate.Evaluator = ReactValueEvaluatorFactory.CreateValue(propValue);
                    }

                    template.Props.Add(propTemplate);
                }
            }

            if (!xmlReader.IsEmptyElement)
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        var childTemplate = ParseElement(xmlReader);
                        template.Children.Add(new ReactElementTemplateChild()
                        {
                            Value = childTemplate,
                            Evaluator = ReactValueEvaluatorFactory.CreateFromTemplate(childTemplate)
                        });
                    }
                    else if (xmlReader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Whitespace)
                    {
                        template.Children.Add(new ReactElementTemplateChild()
                        {
                            Value = xmlReader.Value,
                            Evaluator = ReactValueEvaluatorFactory.CreateValue(xmlReader.Value)
                        });
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Text)
                    {
                        var matches = containsAttributeRegex.Matches(xmlReader.Value);
                        if (matches.Count > 0)
                        {
                            var previousIndex = 0;

                            foreach (Match match in matches)
                            {
                                if (previousIndex < match.Index)
                                {
                                    var contentValue =
                                        xmlReader.Value.Substring(previousIndex,
                                            match.Index - previousIndex);

                                    template.Children.Add(new ReactElementTemplateChild()
                                    {
                                        Value = contentValue,
                                        Evaluator = ReactValueEvaluatorFactory.CreateValue(contentValue)
                                    });
                                }

                                var contentIndex = int.Parse(match.Groups["index"].Value);

                                template.Children.Add(new ReactElementTemplateChild()
                                {
                                    Value = match.Value,
                                    Evaluator = ReactValueEvaluatorFactory.CreateByIndex(contentIndex)
                                });

                                previousIndex = match.Index + match.Length;
                            }


                            if (previousIndex < xmlReader.Value.Length)
                            {
                                var contentValue =
                                    xmlReader.Value.Remove(0, previousIndex);

                                template.Children.Add(new ReactElementTemplateChild()
                                {
                                    Value = contentValue,
                                    Evaluator = ReactValueEvaluatorFactory.CreateValue(contentValue)
                                });
                            }
                        }
                        else
                        {
                            var value = xmlReader.Value;

                            template.Children.Add(new ReactElementTemplateChild()
                            {
                                Value = value,
                                Evaluator = ReactValueEvaluatorFactory.CreateValue(value)
                            });
                        }
                    }
                }
            }

            return template;
        }


        private static ReactElementTemplate Parse(string formatString)
        {
            using (var stringReader = new StringReader(formatString))
            {
                var xmlReaderSettings = new XmlReaderSettings()
                {
                };
                using (var xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
                {
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element)
                        {
                            return ParseElement(xmlReader);
                        }

                        if (xmlReader.NodeType == XmlNodeType.Whitespace)
                        {
                            continue;
                        }
                    }

                    throw new Exception();
                }
            }
        }

        private static ReactElementTemplate Get(string formatString)
        {
            ReactElementTemplate elementTemplate;
            if (!dic.TryGetValue(formatString, out elementTemplate))
            {
                lock (dic)
                {
                    if (!dic.TryGetValue(formatString, out elementTemplate))
                    {
                        elementTemplate = Parse(formatString);
                        dic.Add(formatString, elementTemplate);
                    }
                }
            }

            return elementTemplate;
        }


        public static ReactElementTemplate Get(FormattableString formatString)
        {
            return Get(formatString.Format);
        }
    }
}