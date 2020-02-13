using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReactSharp
{
    public static class ReactComponentTypeResolver
    {
        private static Dictionary<string, Type> components;
        private static object syncObj = new object();


        public static Type ResolveComponent(string tagName)
        {
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


        static ReactComponentTypeResolver()
        {
            lock (syncObj)
            {
                components = new Dictionary<string, Type>();
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    RegisterAssembly(assembly);
                }
            }
        }


        public static void RegisterAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes()
                .Where(i => i.IsSubclassOf(typeof(ReactComponent))))
            {
                components[type.Name] = type;
                components[type.FullName] = type;
            }
        }
    }
}