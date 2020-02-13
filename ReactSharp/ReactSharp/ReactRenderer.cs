using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ReactSharp
{
    public class ReactRenderer
    {
        public ReactNode Root { get; set; }
        private object synObj = new object();

        public void Render(ReactElement element, IReactRendererDOM dom)
        {
            lock (synObj)
            {
                dom.Start();
                long domId;
                Root = RenderElement(element, Root, dom, 0, 0);
                dom.End();
            }
        }


        protected void EnumerateChildren(object children, Action<object> action)
        {
            if (children != null)
            {
                if (children.GetType() == typeof(string))
                {
                    action(children);
                }
                else if (children.GetType().IsArray)
                {
                    var arrayChildren = (object[]) children;
                    foreach (var child in arrayChildren)
                    {
                        EnumerateChildren(child, action);
                    }
                }
                else if (children is IEnumerable enumerableChildren)
                {
                    foreach (var child in enumerableChildren)
                    {
                        EnumerateChildren(child, action);
                    }
                }
                else
                {
                    action(children);
                }
            }
        }

        protected List<ReactNode> RenderChildren(object childrenObj, List<ReactNode> childrenOld, IReactRendererDOM dom,
            long parentDomId)
        {
            var childrenNew = new List<ReactNode>();
            var index = 0;
            EnumerateChildren(childrenObj, childData =>
            {
                var childOld = index < childrenOld.Count ? childrenOld[index] : null;
                if (childOld != null)
                {
                    childrenOld[index] = null;
                }

                if (childData is ReactElement childElement)
                {
                    childrenNew.Add(RenderElement(childElement, childOld, dom, parentDomId, 0));
                }
                else
                {
                    childrenNew.Add(RenderText(childData, childOld, dom, parentDomId));
                }

                index++;
            });
            return childrenNew;
        }

        protected ReactNode RenderText(object value, ReactNode nodeOld, IReactRendererDOM dom, long parentDomId)
        {
            if (nodeOld != null)
            {
                if (nodeOld.Data is ReactElement)
                {
                    RemoveNode(nodeOld, dom);
                    nodeOld = null;
                }
            }

            var node = new ReactNode()
            {
                Data = value,
            };
            if (nodeOld == null)
            {
                node.DomId = dom.CreateText(value?.ToString(), parentDomId);
            }
            else
            {
                node.DomId = nodeOld.DomId;
                if (!string.Equals(nodeOld.Data?.ToString() , node.Data?.ToString()))
                {
                    dom.SetText(node.DomId, value?.ToString());
                }
            }

            return node;
        }


        protected void RemoveNode(ReactNode node, IReactRendererDOM dom)
        {
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    RemoveNode(child, dom);
                }
            }

            if (node.Data is ReactElement element)
            {
                var isDomElement = (element.Type is string);
                if (isDomElement)
                {
                    dom.Remove(node.DomId);
                }
            }
        }


        protected Dictionary<string, object> DiffProps(IDictionary<string, object> propsNew,
            IDictionary<string, object> propsOld)
        {
            var propsChanges = new Dictionary<string, object>();
            foreach (var propNew in propsNew)
            {
                object propOldValue;
                if (!propsOld.TryGetValue(propNew.Key, out propOldValue) || propOldValue != propNew.Value)
                {
                    propsChanges[propNew.Key] = propNew.Value;
                }
            }

            foreach (var propOld in propsOld)
            {
                if (!propsNew.ContainsKey(propOld.Key))
                {
                    propsChanges[propOld.Key] = null;
                }
            }

            return propsChanges;
        }

        protected ReactNode RenderElement(ReactElement element, ReactNode nodeOld, IReactRendererDOM dom,
            long parentDomId, long beforeDomId)
        {
            ReactNode node;
            long domId = 0;
            if (nodeOld != null &&
                ((!(nodeOld.Data is ReactElement)) || ((ReactElement) nodeOld.Data).Type != element.Type))
            {
                RemoveNode(nodeOld, dom);
                nodeOld = null;
            }

            var isDomElement = (element.Type is string);
            if (isDomElement)
            {
                var propsNew = element.Props.Where(i => !string.Equals(i.Key, "Children"))
                    .ToDictionary(i => i.Key, i => i.Value);

                if (nodeOld == null)
                {
                    domId = dom.CreateElement(element.Type.ToString(), propsNew, parentDomId, beforeDomId);
                }
                else
                {
                    domId = nodeOld.DomId;
                    var propsChanges = DiffProps(propsNew, nodeOld.Props);

                    if (propsChanges.Any())
                    {
                        dom.SetProps(domId, propsChanges);
                    }
                }

                node = new ReactNode()
                {
                    DomId = domId,
                    Data = element,
                    Props = propsNew
                };
            }
            else if (element.Type is Type componentType)
            {
                var propsNew = element.Props;
                if (nodeOld == null)
                {
                    node = new ReactNode()
                    {
                        Data = element,
                        Component = (ReactComponent) Activator.CreateInstance(componentType),
                        Props = propsNew
                    };
                    node.Component.SetProps(propsNew);
                    node.Component.ComponentWillMount();
                }
                else
                {
                    node = new ReactNode()
                    {
                        Data = element,
                        Component = nodeOld.Component,
                        Props = element.Props,
                    };

                    var propsChanhes = DiffProps(propsNew, nodeOld.Props);
                    if (propsChanhes.Any())
                    {
                        node.Component.SetProps(propsNew);
                    }
                }
            }
            else
            {
                throw new Exception();
            }

            if (isDomElement)
            {
                if (element.Props.TryGetValue("Children", out var childrenObj))
                {
                    node.Children = RenderChildren(childrenObj, nodeOld?.Children ?? new List<ReactNode>(), dom, domId);
                }
            }
            else
            {
                node.Children = RenderChildren(node.Component.Render(), nodeOld?.Children ?? new List<ReactNode>(), dom,
                    parentDomId);
            }

            return node;
        }
    }
}