using System;
using Google.Protobuf.Compiler;
using Google.Protobuf.Reflection;
using Protobuf.Gen.Core;

namespace Protobuf.Gen.Amp
{
    public abstract class AmpPluginBase : IPlugin
    {
        public void Process(CodeGeneratorRequest request, CodeGeneratorResponse response)
        {
            foreach (var protofile in request.ProtoFile)
            {
                try{
                    GenerateByEachFile(protofile, response);
                }
                catch(Exception ex){
                  
                    response.Error += "file:"+protofile.Name+":"+ ex.Message + ex.StackTrace;
                }
            }
        }

        protected abstract void GenerateByEachFile(FileDescriptorProto protofile, CodeGeneratorResponse response);

    }
}
