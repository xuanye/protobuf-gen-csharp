using System;
using System.Collections.Generic;
using Google.Protobuf.Compiler;
using Google.Protobuf.Reflection;
using Protobuf.Gen.Core;

namespace Protobuf.Gen.Amp
{
    public class CheckPlugin : IPlugin
    {
        private readonly HashSet<int> _services = new HashSet<int>();
        private readonly HashSet<string> _methods = new HashSet<string>();
        public void Process(CodeGeneratorRequest request, CodeGeneratorResponse response)
        {
            foreach (var protofile in request.ProtoFile)
            {
                try{
                    CheckByEachFile(protofile);
                }
                catch(Exception ex){
                    response.Error += ex.Message;
                }
            }
        }

        private void CheckByEachFile(FileDescriptorProto protofile)
        {
            foreach (ServiceDescriptorProto service in protofile.Service)
            {
                CheckEachService(service);
            }
        }

        private void CheckEachService(ServiceDescriptorProto service)
        {
            int serviceId;
            bool hasServiceId = service.Options.CustomOptions.TryGetInt32(DotBPEOptions.SERVICE_ID, out serviceId);
            if (!hasServiceId || serviceId <= 0)
            {
                throw new Exception("Service=" + service.Name + " ServiceId NOT_FOUND");
            }
            if (serviceId >= ushort.MaxValue)
            {
                throw new Exception("Service=" + service.Name + "ServiceId too large");
            }

            if(_services.Contains(serviceId)){
                throw new Exception($" serviceid={serviceId} conflict!");
            }

            _services.Add(serviceId);

            foreach (var method in service.Method)
            {
                int msgId ;
                bool hasMsgId= method.Options.CustomOptions.TryGetInt32(DotBPEOptions.MESSAGE_ID,out msgId);
                if (!hasMsgId || msgId <= 0)
                {
                    throw new Exception("Service" + service.Name + "." + method.Name + " ' MessageId NOT_FINDOUT ");
                }
                if (msgId >= ushort.MaxValue)
                {
                    throw new Exception("Service" + service.Name + "." + method.Name + " is too large");
                }

                string checkKey = $"{serviceId}|{msgId}";
                if(_methods.Contains(checkKey)){
                    throw new Exception($" serviceid={serviceId},messageid={msgId} conflict!");
                }
                _methods.Add(checkKey);
            }
        }
    }
}
