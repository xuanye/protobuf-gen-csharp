using Google.Protobuf;
using Google.Protobuf.Compiler;
using System;
using System.Reflection;
using System.Linq;
using Protobuf.Gen.Core;
using Google.Protobuf.Compatibility;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Loader;

namespace DotBPE.ProtobufPlugin
{
    class Program
    {
        static bool ConsoleLog = false;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            if (args.Length > 0)
            {
                ConsoleLog = true;
                var list = FindAllPlugin();
                LogIt("all plugin loaded,total:"+ list.Count);
                return;
            }

            
            var response = new CodeGeneratorResponse();
            try
            {
                CodeGeneratorRequest request;
                using (var inStream = Console.OpenStandardInput())
                {
                    request = CodeGeneratorRequest.Parser.ParseFrom(inStream);
                }
                ParseCode(request, response);
            }
            catch (Exception e)
            {
                response.Error += e.ToString();
            }

            using (var output = Console.OpenStandardOutput())
            {
                response.WriteTo(output);
                output.Flush();

            }
        }
        private static void LogIt(string logtxt)
        {
            if (ConsoleLog)
            {
                Console.WriteLine(logtxt);
            }
        }
        private static void ParseCode(CodeGeneratorRequest request, CodeGeneratorResponse response)
        {
            var pluginList = FindAllPlugin();
            foreach(var plugin in pluginList)
            {
                plugin.Process(request, response);
            }
        }

        private static List<IPlugin> FindAllPlugin()
        {
            List<IPlugin> list = new List<IPlugin>();
            var allTypes = FindAllAssemblyNames();
            foreach (var typeInfo in allTypes)
            {
                var plugin = (IPlugin)Activator.CreateInstance(typeInfo.AsType());
                list.Add(plugin);
            }
            return list;
        }
        private static List<TypeInfo> FindAllAssemblyNames()
        {
            string root = AppContext.BaseDirectory;
            LogIt("RootPath=" + root);
            var files = Directory.GetFiles(Path.Combine(root, "plugins"));
            var dlls =  files.TakeWhile( fileName => fileName.EndsWith(".dll",StringComparison.OrdinalIgnoreCase));
            List<TypeInfo> allTypes = new List<TypeInfo>();
            LogIt("All DLL Count=" + dlls.Count());
            foreach (var file in dlls)
            {
                LogIt("Load:" + file);
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(root, file));
                List<TypeInfo> listTypes= FindIPluginTypeInfos(assembly);

                if(listTypes.Count>0)
                    allTypes.AddRange(listTypes);
            }
            return allTypes;
        }
        private static List<TypeInfo> FindIPluginTypeInfos(Assembly assembly)
        {
            
            if (assembly == null)
            {
                throw new InvalidOperationException("The assembly  failed to load.");
            }


            var pluginType = typeof(IPlugin).GetTypeInfo();

            // Full scan
            var definedTypes = assembly.DefinedTypes.ToList().FindAll(type=> pluginType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);


            return definedTypes;
        }
    }
}
