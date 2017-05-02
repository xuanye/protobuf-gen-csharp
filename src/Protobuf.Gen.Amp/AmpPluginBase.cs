using System;
using System.IO;
using System.Text;
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
                    using (Stream stream = File.Create("./protobuf.gen.amp.error"))
                    {
                        byte[] err = Encoding.UTF8.GetBytes(ex.Message+ex.StackTrace);
                        stream.Write(err,0,err.Length);
                    }
                    response.Error += "file:"+protofile.Name+":"+ ex.Message;
                }
            }
        }

        protected abstract void GenerateByEachFile(FileDescriptorProto protofile, CodeGeneratorResponse response);

    }
}
