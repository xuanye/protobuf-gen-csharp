using System;
using System.Linq;
using Google.Protobuf.Reflection;

namespace Protobuf.Gen.Core
{
    public static class Utils
    {
        public static string GetFileNamespace(FileDescriptorProto protofile)
        {
            string ns = protofile.Options.CsharpNamespace;
            if (string.IsNullOrEmpty(ns))
            {
                throw new Exception("" + protofile.Name + ".proto did not set csharp_namespace");
            }
            return ConvertCamelCase(ns);
        }

        public static string GetFileName(string fileProto)
        {
            string nomalName = fileProto.Split('.')[0];

            string[] ArrPath = nomalName.Split(new char[]{'/','\\'});

            return ConvertCamelCase(ArrPath[ArrPath.Length-1]);
        }

        public static string ConvertCamelCase(string nomalName)
        {
            return String.Join("", nomalName.Split('_').Select(_ => _.Substring(0, 1).ToUpper() + _.Substring(1)));
        }

        public static string GetTypeName(string typeFullName)
        {
            return ConvertCamelCase(typeFullName.Split('.').Last());
        }
    }
}
