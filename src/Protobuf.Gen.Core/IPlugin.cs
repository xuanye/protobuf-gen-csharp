
using Google.Protobuf.Compiler;

namespace Protobuf.Gen.Core
{
    public interface IPlugin
    {
        void Process(CodeGeneratorRequest request, CodeGeneratorResponse response);
    }
}
